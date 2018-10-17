using GraphQL.Types;
using GraphQl3.GraphQl.Models;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.GraphQl.Types
{
    public class QuarterlyRevenueType : ObjectGraphType<QuarterlyRevenue>
    {
        public QuarterlyRevenueType()
        {
            Name = "QuarterlyRevenue";
            Description = "The revenue since the last quarterly report";

            Field(_ => _.RevenuePercentage).Description("The reported revenue.");
            Field(_ => _.QuarterlyAssetValue).Description("Asset value on the last day of the previous quarter.");
            Field(_ => _.QuarterlyAssetValueDate).Description("Date of the last day of the previous quarter.");
            Field(_ => _.LatestAssetValue).Description("Latest asset value as reported in the CAT system.");
            Field(_ => _.LatestAssetValueDate).Description("Date of the latest asset value as reported in the CAT system.");
            Field(_ => _.ReportDrop).Description("Indication if we have a drop to report.");
            Field(_ => _.DropBracket, true).Description("The reported bracket.");
        }
    }
}
