using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

// ReSharper disable once UnusedMethodReturnValue.Global
// ReSharper disable once MemberCanBePrivate.Global

namespace GraphQl3.GraphiQl
{
    public static class GraphiQlMiddlewareExtensions
    {
        public static IApplicationBuilder UseGraphiQl(this IApplicationBuilder builder, PathString path, Action<GraphiQlMiddlewareOptions> configure)
        {
            var options = new GraphiQlMiddlewareOptions();
            configure(options);

            return builder.UseGraphiQl(path, options);
        }

        public static IApplicationBuilder UseGraphiQl(this IApplicationBuilder builder, PathString path, GraphiQlMiddlewareOptions options)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return builder.Map(path, branch => branch.UseMiddleware<GraphiQlMiddleware>(options));
        }
    }
}
