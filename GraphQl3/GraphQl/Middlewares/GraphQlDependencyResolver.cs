using System;
using GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl3.GraphQl.Middlewares
{
    internal class GraphQlDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider services;

        public GraphQlDependencyResolver(IServiceProvider services)
        {
            this.services = services;
        }

        public T Resolve<T>()
        {
            return services.GetService<T>();
        }

        public object Resolve(Type type)
        {
            return services.GetService(type);
        }
    }
}
