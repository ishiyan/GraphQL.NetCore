using GraphQL.Validation.Complexity;
using GraphQl3.GraphiQl;
using GraphQl3.GraphQl.Mutations;
using GraphQl3.GraphQl.Queries;
using GraphQl3.GraphQl.Types;
using GraphQl3.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace GraphQl3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQl(schema =>
            {
                schema.SetQueryType<TestQuery>();
                schema.SetMutationType<TestMutation>();
            });

            // All graph types must be registered
            services.AddTransient<AccountType>();
            services.AddTransient<BalanceType>();
            services.AddTransient<FloorChangeReasonType>();
            services.AddTransient<FloorChangeType>();
            services.AddTransient<FloorChangeInputType>();
            services.AddTransient<QuarterlyRevenueType>();
            services.AddTransient<RevenueType>();

            services.AddSingleton<TestQuery>();
            services.AddSingleton<TestMutation>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<ICppiFloorService, CppiFloorService>();

            services.AddMvc();
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseGraphiQl(GraphiQlMiddlewareOptions.SlashGraphql, options =>
            {
                options.GraphQlEndpoint = GraphiQlMiddlewareOptions.SlashGraphql;
            });
            app.UseGraphQl(GraphiQlMiddlewareOptions.SlashGraphql, options =>
            {
                // optional if only one schema is registered
                // options.SchemaName = "SecondSchema";
                // options.AuthorizationPolicy = "Authenticated";
                // options.FormatOutput = false;
                // options.ExposeExceptions = true;
                options.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
            });

            app.UseMvc();
        }
    }
}
