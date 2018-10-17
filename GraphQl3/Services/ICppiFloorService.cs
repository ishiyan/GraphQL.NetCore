using System.Threading.Tasks;
using GraphQl3.GraphQl.Models;

namespace GraphQl3.Services
{
    /// <summary>
    /// A service that offers functionality related to the CPPI floor.
    /// </summary>
    public interface ICppiFloorService
    {
        /// <summary>
        /// Select all (current and historic) CPPI floors for a given account.
        /// </summary>
        /// <param name="accountId">The technical id of an account.</param>
        Task<FloorChange[]> SelectHistoryByAccountAsync(int accountId);

        /// <summary>
        /// Add a floor change for a given account.
        /// </summary>
        /// <param name="accountId">The technical id of an account.</param>
        /// <param name="cashFlow">The cash flow.</param>
        /// <param name="changeReason">The reason for change.</param>
        Task<FloorChange> AddFloorChangeAsync(int accountId, double? cashFlow, FloorChangeReason changeReason);
    }
}