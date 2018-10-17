using GraphQL.Types;
using GraphQl3.GraphQl.Models;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.GraphQl.Types
{
    public class BalanceType : ObjectGraphType<Balance>
    {
        public BalanceType()
        {
            Name = "Balance";
            Description = "The buffer and reserve of the specified account";

            Field(_ => _.Capital).Description("Total asset value including cash.");
            Field(_ => _.Buffer).Description("The buffer on the account.");
            Field(_ => _.Reserve).Description("The reserve of an account.");
            Field(_ => _.BufferPercentage, true).Description("The buffer in percentage of the total capital, null if no capital.");
        }
    }
}
