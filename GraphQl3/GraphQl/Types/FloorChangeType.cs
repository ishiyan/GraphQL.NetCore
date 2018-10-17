using GraphQL.Types;
using GraphQl3.GraphQl.Models;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.GraphQl.Types
{
    public class FloorChangeType : ObjectGraphType<FloorChange>
    {
        public FloorChangeType()
        {
            Name = "FloorChange";

            Field(_ => _.CppiFloorHistoryId).Description("The CPPI floor history id.");
            Field(_ => _.AccountCppiConfigId).Description("Account CPPI config id.");
            Field(_ => _.CppiFloor).Description("CPPI floor value.");
            Field(_ => _.ChangeDateTime).Description("The date and time of the change.");
            Field(_ => _.CashFlow, true).Description("The cash flow.");

            // Field(_ => _.ChangeReason).Description("Reason for change.");
            Field<FloorChangeReasonType>("changeReason", "Reason for change.");
        }
    }
}
