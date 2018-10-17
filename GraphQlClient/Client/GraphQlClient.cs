using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GraphQlCleant.Client;
using GraphQlClient.Request;
using GraphQlClient.Exceptions;
using GraphQlClient.Response;
using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace GraphQlClient.Client
{
    /// <summary>
    /// A Client to access GraphQL EndPoints
    /// </summary>
    public class GraphQlClient : IDisposable
    {
        /// <summary>
        /// Gets the headers which should be sent with each request.
        /// </summary>
        public HttpRequestHeaders DefaultRequestHeaders => httpClient.DefaultRequestHeaders;

        /// <summary>
        /// The GraphQL EndPoint to be used
        /// </summary>
        public Uri EndPoint
        {
            get => Options.EndPoint;
            set => Options.EndPoint = value;
        }

        /// <summary>
        /// The Options	to be used
        /// </summary>
        public GraphQlClientOptions Options { get; set; }

        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="endPoint">The EndPoint to be used</param>
        public GraphQlClient(string endPoint) : this(new Uri(endPoint)) { }

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="endPoint">The EndPoint to be used</param>
        public GraphQlClient(Uri endPoint) : this(new GraphQlClientOptions { EndPoint = endPoint }) { }

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="endPoint">The EndPoint to be used</param>
        /// <param name="options">The Options to be used</param>
        public GraphQlClient(string endPoint, GraphQlClientOptions options) : this(new Uri(endPoint), options) { }

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="endPoint">The EndPoint to be used</param>
        /// <param name="options">The Options to be used</param>
        public GraphQlClient(Uri endPoint, GraphQlClientOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Options.EndPoint = endPoint ?? throw new ArgumentNullException(nameof(endPoint));

            if (Options.JsonSerializerSettings == null) { throw new ArgumentNullException(nameof(Options.JsonSerializerSettings)); }
            if (Options.HttpMessageHandler == null) { throw new ArgumentNullException(nameof(Options.HttpMessageHandler)); }
            if (Options.MediaType == null) { throw new ArgumentNullException(nameof(Options.MediaType)); }

            httpClient = new HttpClient(Options.HttpMessageHandler);
        }

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="options">The Options to be used</param>
        public GraphQlClient(GraphQlClientOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));

            if (Options.EndPoint == null) { throw new ArgumentNullException(nameof(Options.EndPoint)); }
            if (Options.JsonSerializerSettings == null) { throw new ArgumentNullException(nameof(Options.JsonSerializerSettings)); }
            if (Options.HttpMessageHandler == null) { throw new ArgumentNullException(nameof(Options.HttpMessageHandler)); }
            if (Options.MediaType == null) { throw new ArgumentNullException(nameof(Options.MediaType)); }

            httpClient = new HttpClient(Options.HttpMessageHandler);
        }

        /// <summary>
        /// Send a query via GET
        /// </summary>
        /// <param name="query">The Request</param>
        /// <returns>The Response</returns>
        public Task<GraphQlResponse> GetQueryAsync(string query) => GetQueryAsync(query, CancellationToken.None);

        /// <summary>
        /// Send a query via GET
        /// </summary>
        /// <param name="query">The Request</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The Response</returns>
        public Task<GraphQlResponse> GetQueryAsync(string query, CancellationToken cancellationToken)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }

            return GetAsync(new GraphQlRequest { Query = query }, cancellationToken);
        }

        /// <summary>
        /// Send a <see cref="GraphQlRequest"/> via GET
        /// </summary>
        /// <param name="request">The Request</param>
        /// <returns>The Response</returns>
        public Task<GraphQlResponse> GetAsync(GraphQlRequest request) => GetAsync(request, CancellationToken.None);

        /// <summary>
        /// Send a <see cref="GraphQlRequest"/> via GET
        /// </summary>
        /// <param name="request">The Request</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The Response</returns>
        public async Task<GraphQlResponse> GetAsync(GraphQlRequest request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Query == null) { throw new ArgumentNullException(nameof(request.Query)); }

            var queryParamsBuilder = new StringBuilder($"query={request.Query}", 3);
            if (request.OperationName != null) { queryParamsBuilder.Append($"&operationName={request.OperationName}"); }
            if (request.Variables != null) { queryParamsBuilder.Append($"&variables={JsonConvert.SerializeObject(request.Variables)}"); }
            using (var httpResponseMessage = await httpClient.GetAsync($"{Options.EndPoint}?{queryParamsBuilder}", cancellationToken).ConfigureAwait(false))
            {
                return await ReadHttpResponseMessageAsync(httpResponseMessage).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Send a query via POST
        /// </summary>
        /// <param name="query">The Request</param>
        /// <returns>The Response</returns>
        public Task<GraphQlResponse> PostQueryAsync(string query) => PostQueryAsync(query, CancellationToken.None);

        /// <summary>
        /// Send a query via POST
        /// </summary>
        /// <param name="query">The Request</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The Response</returns>
        public Task<GraphQlResponse> PostQueryAsync(string query, CancellationToken cancellationToken)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }

            return PostAsync(new GraphQlRequest { Query = query }, cancellationToken);
        }

        /// <summary>
        /// Send a <see cref="GraphQlRequest"/> via POST
        /// </summary>
        /// <param name="request">The Request</param>
        /// <returns>The Response</returns>
        public Task<GraphQlResponse> PostAsync(GraphQlRequest request) => PostAsync(request, CancellationToken.None);

        /// <summary>
        /// Send a <see cref="GraphQlRequest"/> via POST
        /// </summary>
        /// <param name="request">The Request</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The Response</returns>
        public async Task<GraphQlResponse> PostAsync(GraphQlRequest request, CancellationToken cancellationToken)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Query == null) { throw new ArgumentNullException(nameof(request.Query)); }

            var graphQlString = JsonConvert.SerializeObject(request, Options.JsonSerializerSettings);
            using (var httpContent = new StringContent(graphQlString, Encoding.UTF8, Options.MediaType.MediaType))
            using (var httpResponseMessage = await httpClient.PostAsync(EndPoint, httpContent, cancellationToken).ConfigureAwait(false))
            {
                return await ReadHttpResponseMessageAsync(httpResponseMessage).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Releases unmanaged resources
        /// </summary>
        public void Dispose() => httpClient.Dispose();

        /// <summary>
        /// Reads the <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="httpResponseMessage">The Response</param>
        /// <returns>The GrahQlResponse</returns>
        private async Task<GraphQlResponse> ReadHttpResponseMessageAsync(HttpResponseMessage httpResponseMessage)
        {
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var jsonSerializer = new JsonSerializer
                {
                    ContractResolver = Options.JsonSerializerSettings.ContractResolver
                };
                try
                {
                    return jsonSerializer.Deserialize<GraphQlResponse>(jsonTextReader);
                }
                catch (JsonReaderException)
                {
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw;
                    }
                    throw new GraphQlHttpException(httpResponseMessage);
                }
            }
        }

    }

}
