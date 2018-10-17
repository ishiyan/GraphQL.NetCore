using GraphQl3.GraphQl.Models;
using GraphQL.Types;
using GraphQl3.GraphQl.Types;
using GraphQl3.Services;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once UnusedParameter.Local

namespace GraphQl3.GraphQl.Mutations
{
    public class TestMutation : ObjectGraphType
    {
        public TestMutation(IAccountService accountService, ICppiFloorService cppiFloorService)
        {
            Name = "TestMutation";

            Field<FloorChangeType>(
                "addFloorChange",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "accountId", Description = "The account id." },
                    new QueryArgument<FloatGraphType> { Name = "cashFlow", Description = "The cash flow." },
                    new QueryArgument<NonNullGraphType<FloorChangeReasonType>> { Name = "changeReason", Description = "Reason for change." }
                ),
                resolve: context =>
                {
                    var accountId = context.GetArgument<int>("accountId");
                    var cashFlow = context.GetArgument<double?>("cashFlow");
                    var changeReason = context.GetArgument<FloorChangeReason>("changeReason");
                    return cppiFloorService.AddFloorChangeAsync(accountId, cashFlow, changeReason);
                }
            );
        }
    }
}
