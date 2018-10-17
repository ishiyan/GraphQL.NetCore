using System;
using GraphQlClient.Request;

namespace GraphQlClient
{
    internal static class Program
    {
        private static readonly GraphQlRequest MyRequest = new GraphQlRequest
        {
            Query = @"query q1($accountId: Int!){account(accountId: $accountId){accountId cppiConfiguration}}",
            Variables = new {accountId = 1}
        };

        static void Main(/*string[] args*/)
        {
            Console.WriteLine("query:");
            Console.WriteLine(MyRequest.Query);
            var graphQlClient = new Client.GraphQlClient("http://localhost:5000/graphql");
            var graphQlResponse = graphQlClient.PostAsync(MyRequest).GetAwaiter().GetResult();

            var data = graphQlResponse.Data;
            Console.WriteLine("response:");
            Console.WriteLine(data);
        }
    }
}
