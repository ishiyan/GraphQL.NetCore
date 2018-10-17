using System.Threading.Tasks;
using GraphQlClient.Request;
using GraphQlClient.Response;

// ReSharper disable UnusedMember.Global

namespace GraphQlClient.Client
{
    /// <summary>
    /// Extension Methods for <see cref="GraphQlClient"/>
    /// </summary>
    public static class GraphQlClientExtensions
    {

        private static readonly GraphQlRequest IntrospectionQuery = new GraphQlRequest
        {
            Query = @"
				query IntrospectionQuery {
					__schema {
						queryType {
							name
						},
						mutationType {
							name
						},
						subscriptionType {
							name
						},
						types {
							...FullType
						},
						directives {
							name,
							description,
							args {
								...InputValue
							},
							onOperation,
							onFragment,
							onField
						}
					}
				}

				fragment FullType on __Type {
					kind,
					name,
					description,
					fields(includeDeprecated: true) {
						name,
						description,
						args {
							...InputValue
						},
						type {
							...TypeRef
						},
						isDeprecated,
						deprecationReason
					},
					inputFields {
						...InputValue
					},
					interfaces {
						...TypeRef
					},
					enumValues(includeDeprecated: true) {
						name,
						description,
						isDeprecated,
						deprecationReason
					},
					possibleTypes {
						...TypeRef
					}
				}

				fragment InputValue on __InputValue {
					name,
					description,
					type {
						...TypeRef
					},
					defaultValue
				}

				fragment TypeRef on __Type {
					kind,
					name,
					ofType {
						kind,
						name,
						ofType {
							kind,
							name,
							ofType {
								kind,
								name
							}
						}
					}
				}".Replace("\t", "").Replace("\n", "").Replace("\r", ""),
            Variables = null
        };

        /// <summary>
        /// Send an IntrospectionQuery via GET
        /// </summary>
        /// <param name="graphQlClient">The GraphQlClient</param>
        /// <returns>The GraphQlResponse</returns>
        public static async Task<GraphQlResponse> GetIntrospectionQueryAsync(this GraphQlClient graphQlClient) =>
            await graphQlClient.GetAsync(IntrospectionQuery).ConfigureAwait(false);

        /// <summary>
        /// Send an IntrospectionQuery via POST
        /// </summary>
        /// <param name="graphQlClient">The GraphQlClient</param>
        /// <returns>The GraphQlResponse</returns>
        public static async Task<GraphQlResponse> PostIntrospectionQueryAsync(this GraphQlClient graphQlClient) =>
            await graphQlClient.PostAsync(IntrospectionQuery).ConfigureAwait(false);

    }

}
