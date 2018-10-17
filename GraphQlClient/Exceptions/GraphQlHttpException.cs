using System;
using System.Net.Http;

// ReSharper disable once UnusedAutoPropertyAccessor.Global
// ReSharper disable once MemberCanBePrivate.Global

namespace GraphQlClient.Exceptions
{
    /// <summary>
    /// An exception thrown on unexpected <see cref="System.Net.Http.HttpResponseMessage"/>
    /// </summary>
    public class GraphQlHttpException : Exception
    {
        /// <summary>
        /// The <see cref="System.Net.Http.HttpResponseMessage"/>
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Creates a new instance of <see cref="GraphQlHttpException"/>
        /// </summary>
        /// <param name="httpResponseMessage">The unexpected <see cref="System.Net.Http.HttpResponseMessage"/></param>
        public GraphQlHttpException(HttpResponseMessage httpResponseMessage)
            : base($"Unexpected {nameof(System.Net.Http.HttpResponseMessage)} with code: {httpResponseMessage?.StatusCode}")
        {
            HttpResponseMessage = httpResponseMessage ?? throw new ArgumentNullException(nameof(httpResponseMessage));
        }
    }
}
