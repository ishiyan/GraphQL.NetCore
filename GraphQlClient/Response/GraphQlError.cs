using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace GraphQlClient.Response
{
    /// <summary>
    /// Represents the error of a <see cref="GraphQlResponse"/>
    /// </summary>
    public class GraphQlError
    {
        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The Location of an error
        /// </summary>
        public GraphQlLocation[] Locations { get; set; }

        /// <summary>
        /// Additional error entries
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, JToken> AdditonalEntries { get; set; }
    }
}
