using GraphQL.Validation.Complexity;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global

namespace GraphQl3.GraphQl.Middlewares
{
    public class GraphQlMiddlewareOptions
    {
        public string SchemaName { get; set; }
        public string AuthorizationPolicy { get; set; }

        public bool FormatOutput { get; set; } = true;
        public ComplexityConfiguration ComplexityConfiguration { get; set; } = new ComplexityConfiguration();
        public bool ExposeExceptions { get; set; }
    }
}
