using System;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Execution;
using GraphQl3.GraphQl.Middlewares;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable once MemberCanBePrivate.Global
// ReSharper disable once UnusedMember.Global

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add GraphQL services to the specified <see cref="IServiceCollection">IServiceCollection</see>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure">Action to configure the default schema.</param>
        /// <returns></returns>
        public static IGraphQlBuilder AddGraphQl(this IServiceCollection services, Action<SchemaConfiguration> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            // commented out because of https://github.com/graphql-dotnet/graphql-dotnet/issues/478
            // to use it, uncomment the line in Program.cs
            // services.AddScoped<DocumentExecuter>();
            services.AddSingleton<DocumentExecuter>();

            var builder = new GraphQlBuilder(services);

            var schema = new SchemaConfiguration(null);
            configure(schema);

            return builder.AddSchema(schema);
        }

        /// <summary>
        /// Adds a GraphQL <see cref="IDocumentExecutionListener"/> services to the specified <see cref="IServiceCollection">IServiceCollection</see>.
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="T">The listener to add.</typeparam>
        /// <returns></returns>
        public static IServiceCollection AddDocumentExecutionListener<T>(this IServiceCollection services)
            where T: class, IDocumentExecutionListener
        {
            services.AddSingleton<IDocumentExecutionListener, T>();
            return services;
        }
        
        /// <summary>
        /// Add GraphQL DataLoader services to the specified <see cref="IServiceCollection">IServiceCollection</see>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataLoader(this IServiceCollection services)
        {
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddDocumentExecutionListener<DataLoaderDocumentListener>();
            return services;
        }
    }
}
