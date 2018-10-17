using GraphQL.Types;
using GraphQl3.GraphQl.Models;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.GraphQl.Types
{
    public class RevenueType : ObjectGraphType<Revenue>
    {
        public RevenueType()
        {
            Name = "Revenue";
            Description = "The revenue percentage from the start of the account and for the current year along with the current asset value";

            Field(_ => _.RevenuePercentageCurrentYear).Description("Revenue percentage from the last day of the previous year till now.");
            Field(_ => _.RevenuePercentageSinceStartAccount).Description("Revenue percentage from the start of the account till now.");
            Field(_ => _.CurrentAssetValue).Description("The asset value added to the calculation for the last day.");
        }
    }
}
