using GraphQL.Types;
using GraphQl3.GraphQl.Types;
using GraphQl3.Services;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.GraphQl.Queries
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery(IAccountService accountService, ICppiFloorService cppiFloorService)
        {
            Name = "TestQuery";

            Field<AccountType>(
              "account",
              arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "accountId", Description = "The account id." }
              ),
              resolve: context =>
              {
                  var accountId = context.GetArgument<int>("accountId");
                  return accountService.GetByAccountIdAsync(accountId);
              }
            );

            Field<BalanceType>(
                "balance",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "accountId", Description = "The account id." }
                ),
                resolve: context =>
                {
                    var accountId = context.GetArgument<int>("accountId");
                    return accountService.GetBalanceAsync(accountId);
                }
            );

            Field<RevenueType>(
                "revenue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "accountId", Description = "The account id." }
                ),
                resolve: context =>
                {
                    var accountId = context.GetArgument<int>("accountId");
                    return accountService.GetRevenuePercentagesAsync(accountId);
                }
            );

            Field<QuarterlyRevenueType>(
                "quarterlyRevenue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "accountId", Description = "The account id." }
                ),
                resolve: context =>
                {
                    var accountId = context.GetArgument<int>("accountId");
                    return accountService.GetQuarterlyRevenueAsync(accountId);
                }
            );

            Field<ListGraphType<FloorChangeType>>(
                "floorHistory",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "accountId", Description = "The account id." }
                ),
                resolve: context =>
                {
                    var accountId = context.GetArgument<int>("accountId");
                    return cppiFloorService.SelectHistoryByAccountAsync(accountId);
                }
            );
        }
    }
}
