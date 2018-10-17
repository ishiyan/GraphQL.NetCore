using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Conversion;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable UnusedMember.Global

namespace GraphQl3.GraphQl.Middlewares
{
    public class SchemaConfiguration : ISchemaProvider
    {
        public string Name { get; }

        private Type queryType;
        private Type mutationType;
        private Type subscriptionType;
        private Type fileNameConverterType;

        public SchemaConfiguration(string name)
        {
            Name = name;
        }

        // TODO: Add Directives

        public void SetMutationType<T>() where T : IObjectGraphType
        {
            mutationType = typeof(T);
        }

        public void SetQueryType<T>() where T : IObjectGraphType
        {
            queryType = typeof(T);
        }

        public void SetSubscriptionType<T>() where T : IObjectGraphType
        {
            subscriptionType = typeof(T);
        }

        public void SetFieldNameConverter<T>() where T : IFieldNameConverter
        {
            fileNameConverterType = typeof(T);
        }

        ISchema ISchemaProvider.Create(IServiceProvider services)
        {
            var dependencyResolver = new GraphQlDependencyResolver(services);
            var schema = new Schema(dependencyResolver);

            if (queryType != null)
                schema.Query = (IObjectGraphType)services.GetRequiredService(queryType);

            if (mutationType != null)
                schema.Mutation = (IObjectGraphType)services.GetRequiredService(mutationType);

            if (subscriptionType != null)
                schema.Subscription = (IObjectGraphType)services.GetRequiredService(subscriptionType);

            if (fileNameConverterType != null)
                schema.FieldNameConverter = (IFieldNameConverter)services.GetRequiredService(fileNameConverterType);

            return schema;
        }

        public static ISchemaProvider GetSchemaProvider(string name, IServiceProvider services)
        {
            var providers = services.GetService<IEnumerable<ISchemaProvider>>()
                .Where(x => x.Name == name)
                .ToArray();

            if (providers.Length == 0)
            {
                if (name == null)
                    throw new InvalidOperationException("No default schema registered!");

                throw new InvalidOperationException($"No schema found registered with name '{name}'");
            }

            if (providers.Length > 1)
                throw new InvalidOperationException($"Multiple schemas registered with the same name: '{name}'");

            return providers[0];
        }
    }
}
