using System;
using GraphQlClient.Response;

namespace GraphQlClient.Exceptions
{
    /// <summary>
    /// An exception that contains a <see cref="Response.GraphQlError"/>
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public class GraphQlException : Exception
    {
        /// <summary>
        /// The GraphQLError
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public GraphQlError GraphQlError { get; }

        /// <summary>
        /// Constructor for a GraphQlException
        /// </summary>
        /// <param name="graphQlError">The GraphQL Error</param>
        public GraphQlException(GraphQlError graphQlError) : base(graphQlError.Message)
        {
            GraphQlError = graphQlError;
        }
    }
}
