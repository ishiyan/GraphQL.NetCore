using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Execution;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedField.Local
// ReSharper disable once UnusedMember.Global

namespace GraphQl3.GraphQl.Middlewares
{
    public class GraphQlMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ISchemaProvider schemaProvider;
        private readonly GraphQlMiddlewareOptions options;
        private readonly DocumentExecuter executer;
        private readonly IEnumerable<IDocumentExecutionListener> executionListeners;

        public GraphQlMiddleware(
            RequestDelegate next,
            ISchemaProvider schemaProvider,
            GraphQlMiddlewareOptions options,
            DocumentExecuter executer,
            IEnumerable<IDocumentExecutionListener> executionListeners)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.schemaProvider = schemaProvider ?? throw new ArgumentNullException(nameof(schemaProvider));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.executer = executer ?? throw new ArgumentNullException(nameof(options));
            this.executionListeners = executionListeners ?? new IDocumentExecutionListener[0];
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var logger = httpContext.RequestServices.GetService<ILogger<GraphQlMiddleware>>();

            HttpRequest request = httpContext.Request;
            HttpResponse response = httpContext.Response;

            // GraphQL HTTP only supports GET and POST methods.
            if (request.Method != "GET" && request.Method != "POST")
            {
                response.Headers.Add("Allow", "GET, POST");
                response.StatusCode = 405;

                return;
            }

            // Check authorization
            if (options.AuthorizationPolicy != null)
            {
                var authorizationService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
                var authzResult = await authorizationService.AuthorizeAsync(httpContext.User, options.AuthorizationPolicy);

                if (!authzResult.Succeeded)
                {
                    await httpContext.ForbidAsync();
                    return;
                }
            }

            GraphQlParameters parameters = await GetParametersAsync(request);

            ISchema schema = schemaProvider.Create(httpContext.RequestServices);

            var result = await executer.ExecuteAsync(executionOptions =>
            {
                executionOptions.Schema = schema;
                executionOptions.Query = parameters.Query;
                executionOptions.OperationName = parameters.OperationName;
                executionOptions.Inputs = parameters.GetInputs();
                executionOptions.CancellationToken = httpContext.RequestAborted;
                executionOptions.ComplexityConfiguration = options.ComplexityConfiguration;
                executionOptions.UserContext = httpContext;
                executionOptions.ExposeExceptions = options.ExposeExceptions;
                ConfigureDocumentExecutionListeners(executionOptions, executionListeners);
            });

            if (result.Errors?.Count > 0)
                logger.LogError("GraphQL Result {Errors}", result.Errors);

            var writer = new DocumentWriter(indent: options.FormatOutput);
            var json = writer.Write(result);

            response.StatusCode = 200;
            response.ContentType = "application/json; charset=utf-8";

            await response.WriteAsync(json);
        }

        private static async Task<GraphQlParameters> GetParametersAsync(HttpRequest request)
        {
            // http://graphql.org/learn/serving-over-http/#http-methods-headers-and-body

            string body = null;
            if (request.Method == "POST")
            {
                // Read request body
                using (var sr = new StreamReader(request.Body))
                {
                    body = await sr.ReadToEndAsync();
                }
            }

            MediaTypeHeaderValue.TryParse(request.ContentType, out MediaTypeHeaderValue contentType);

            GraphQlParameters parameters;

            switch (contentType.MediaType)
            {
                case "application/json":
                    // Parse request as json
                    parameters = JsonConvert.DeserializeObject<GraphQlParameters>(body);
                    break;

                case "application/graphql":
                    // The whole body is the query
                    parameters = new GraphQlParameters { Query = body };
                    break;

                default:
                    // Don't parse anything
                    parameters = new GraphQlParameters();
                    break;
            }

            string query = request.Query["query"];

            // Query string "query" overrides a query in the body
            parameters.Query = query ?? parameters.Query;

            return parameters;
        }
        
        private static void ConfigureDocumentExecutionListeners(ExecutionOptions options, IEnumerable<IDocumentExecutionListener> listeners)
        {
            Debug.Assert(listeners != null, "listeners != null");

            var listenerSet = new HashSet<IDocumentExecutionListener>(options.Listeners);           
            listenerSet.UnionWith(listeners);
            
            options.Listeners.Clear();
            foreach (var listener in listenerSet)
            {
                options.Listeners.Add(listener);
            }
        }
    }
}
