using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

// ReSharper disable once MemberCanBePrivate.Global

namespace GraphQl3
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                // https://github.com/graphql-dotnet/graphql-dotnet/issues/478
                //.UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();
    }
}
