using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace GraphQlCleant.Client
{
    /// <summary>
    /// The Options that the <see cref="GraphQlClient"/> will use
    /// </summary>
    public class GraphQlClientOptions
    {
        /// <summary>
        /// The GraphQL EndPoint to be used
        /// </summary>
        public Uri EndPoint { get; set; }

        /// <summary>
        /// The <see cref="Newtonsoft.Json.JsonSerializerSettings"/> that is going to be used
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        /// The <see cref="System.Net.Http.HttpMessageHandler"/> that is going to be used
        /// </summary>
        public HttpMessageHandler HttpMessageHandler { get; set; } = new HttpClientHandler();

        /// <summary>
        /// The <see cref="MediaTypeHeaderValue"/> that will be send on POST
        /// </summary>
        public MediaTypeHeaderValue MediaType { get; set; } = new MediaTypeHeaderValue("application/json"); // This should be "application/graphql" also "application/x-www-form-urlencoded" is Accepted
    }
}
