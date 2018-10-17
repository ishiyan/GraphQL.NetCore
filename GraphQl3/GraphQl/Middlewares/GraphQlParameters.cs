using System.Collections.Generic;
using System.Linq;
using GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GraphQl3.GraphQl.Middlewares
{
    internal class GraphQlParameters
    {
        public string Query { get; set; }
        public string OperationName { get; set; }
        public Dictionary<string, object> Variables { get; set; }

        public Inputs GetInputs()
        {
            if (Variables != null && Variables.Any())
            {
                var sanitizedVariables = new Dictionary<string, object>();

                foreach (var key in Variables.Keys)
                {
                    var value = Variables[key];

                    if (value is JObject)
                    {
                        // fix the nesting ( {{ }} )
                        var serialized = JsonConvert.SerializeObject(value);
                        var deserialized = JsonConvert.DeserializeObject<Dictionary<string, object>>(serialized);
                        sanitizedVariables.Add(key, deserialized);
                    }
                    else
                    {
                        sanitizedVariables.Add(key, value);
                    }
                }

                return new Inputs(sanitizedVariables);
            }

            return (Variables != null) ? new Inputs(Variables) : new Inputs();
        }
    }
}
