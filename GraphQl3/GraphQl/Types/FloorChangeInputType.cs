using GraphQL.Types;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.GraphQl.Types
{
    public class FloorChangeInputType : InputObjectGraphType
    {
        public FloorChangeInputType()
        {
            Name = "FloorChangeInput";

            Field<NonNullGraphType<IntGraphType>>("accountId", "The account id.");
            Field<FloatGraphType>("cashFlow", "The cash flow.");
            Field<NonNullGraphType<FloorChangeReasonType>>("changeReason", "Reason for change.");
        }
    }
}
