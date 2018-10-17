using System;
using GraphQL.DataLoader;
using GraphQL.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl3.GraphQl.Middlewares
{
    public class GraphQlBuilder : IGraphQlBuilder
    {
        private readonly IServiceCollection services;

        public GraphQlBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public IGraphQlBuilder AddSchema(string name, Action<SchemaConfiguration> configure)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var schema = new SchemaConfiguration(name);
            configure(schema);

            services.AddSingleton<ISchemaProvider>(schema);
            return this;
        }
        
        public IGraphQlBuilder AddDocumentExecutionListener<T>()
            where T: class, IDocumentExecutionListener
        {
            services.AddSingleton<IDocumentExecutionListener, T>();
            return this;
        }
        
        public IGraphQlBuilder AddDataLoader()
        {
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            AddDocumentExecutionListener<DataLoaderDocumentListener>();
            return this;
        }

        internal IGraphQlBuilder AddSchema(SchemaConfiguration schema)
        {
            services.AddSingleton<ISchemaProvider>(schema);
            return this;
        }

        public IGraphQlBuilder AddGraphTypes()
        {
            throw new NotImplementedException();
        }
    }
}
