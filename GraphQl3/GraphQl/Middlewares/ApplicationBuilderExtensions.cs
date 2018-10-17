using GraphQl3.GraphQl.Middlewares;
using Microsoft.AspNetCore.Http;
using System;

// ReSharper disable once UnusedMember.Global
// ReSharper disable once MemberCanBePrivate.Global
// ReSharper disable once UnusedMethodReturnValue.Global

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds a GraphQL middleware to the <see cref="IApplicationBuilder"/> request execution pipeline with default options.
        /// </summary>
        public static IApplicationBuilder UseGraphQl(this IApplicationBuilder builder,
            PathString path)
        {
            return builder.UseGraphQl(path, new GraphQlMiddlewareOptions());
        }

        /// <summary>
        /// Adds a GraphQL middleware to the <see cref="IApplicationBuilder"/> request execution pipeline with a callback to configure options.
        /// </summary>
        public static IApplicationBuilder UseGraphQl(this IApplicationBuilder builder,
            PathString path, Action<GraphQlMiddlewareOptions> configure)
        {
            var options = new GraphQlMiddlewareOptions();
            configure(options);

            return builder.UseGraphQl(path, options);
        }

        /// <summary>
        /// Adds a GraphQL middleware to the <see cref="IApplicationBuilder"/> request execution pipeline with the specified options.
        /// </summary>
        public static IApplicationBuilder UseGraphQl(this IApplicationBuilder builder,
            PathString path, GraphQlMiddlewareOptions options)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var schemaProvider = SchemaConfiguration.GetSchemaProvider(options.SchemaName, builder.ApplicationServices);

            return builder.Map(path, branch => branch.UseMiddleware<GraphQlMiddleware>(schemaProvider, options));
        }
    }
}
