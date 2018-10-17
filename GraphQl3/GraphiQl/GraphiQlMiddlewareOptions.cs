namespace GraphQl3.GraphiQl
{
    public class GraphiQlMiddlewareOptions
    {
        public const string SlashGraphql = "/graphiql";

        public string GraphQlEndpoint { get; set; } = SlashGraphql;
    }
}
