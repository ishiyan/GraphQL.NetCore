using GraphQl3.GraphQl.Models;
using GraphQL.Types;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.GraphQl.Types
{
    public class FloorChangeReasonType : EnumerationGraphType<FloorChangeReason>
    {
        public FloorChangeReasonType()
        {
            Name = "FloorChangeReason";
            Description = "Enumeration for floor change reason.";
        }
    }
}
