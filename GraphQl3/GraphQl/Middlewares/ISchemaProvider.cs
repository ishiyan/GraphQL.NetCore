using System;
using GraphQL.Types;

namespace GraphQl3.GraphQl.Middlewares
{
    public interface ISchemaProvider
    {
        string Name { get; }

        ISchema Create(IServiceProvider services);
    }
}