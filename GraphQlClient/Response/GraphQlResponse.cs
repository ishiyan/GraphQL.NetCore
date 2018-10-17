// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace GraphQlClient.Response
{
    /// <summary>
    /// Represent the response of a <see cref="GraphQlClient.Request.GraphQlRequest"/>
    /// For more information <see href="http://graphql.org/learn/serving-over-http/#response"/>
    /// </summary>
    public class GraphQlResponse
    {
        /// <summary>
        /// The data of the response
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public dynamic Data { get; set; }

        /// <summary>
        /// The Errors if ocurred
        /// </summary>
        public GraphQlError[] Errors { get; set; }

        /// <summary>
        /// Get a field of <see cref="Data"/> as Type
        /// </summary>
        /// <typeparam name="Type">The expected type</typeparam>
        /// <param name="fieldName">The name of the field</param>
        /// <returns>The field of data as an object</returns>
        public Type GetDataFieldAs<Type>(string fieldName)
        {
            var value = Data.GetValue(fieldName);
            return value.ToObject<Type>();
        }
    }
}
