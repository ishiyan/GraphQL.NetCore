using GraphQL.Types;
using GraphQl3.GraphQl.Models;
using GraphQl3.Services;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.GraphQl.Types
{
    public class AccountType : ObjectGraphType<Account>
    {
        public AccountType(IAccountService accountService)
        {
            Name = "Account";
            Description = "Account details";

            Field(_ => _.CppiConfiguration).Description("The CPPI configuration.");
            Field(_ => _.AccountId).Description("The topline id for the account.");
            Field(_ => _.BamAccountId).Description("The bam id for the account.");
            Field(_ => _.CppiPercentage).Description("The current CPPI percentage.");
            Field(_ => _.ManagementStartDate, true).Description("The management startdate.");

            Field<DateGraphType>(
                "LastProcessedDate",
                "The most recent date the account was processed.",
                resolve: context => accountService.GetLastCompletelyProcessedDateAsync(context.Source.AccountId));

            Field<DateGraphType>(
                "LastAssetValueUpdate",
                "The most recent date the account asset value was updated.",
                resolve: context => accountService.LatestAssetValueUpdateAsync(context.Source.AccountId));

            /*Field<DateGraphType>().Name("LastProcessedDate").Description("The most recent date the account was processed.")
                .ResolveAsync(context => accountService.GetLastCompletelyProcessedDateAsync(context.Source.AccountId));

            Field<DateGraphType>().Name("LastAssetValueUpdate").Description("The most recent date the account asset value was updated.")
                .ResolveAsync(context => accountService.LatestAssetValueUpdateAsync(context.Source.AccountId));*/
        }
    }
}
