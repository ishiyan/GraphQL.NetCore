using System;
using System.Threading.Tasks;
using GraphQl3.GraphQl.Models;

namespace GraphQl3.Services
{
    /// <summary>
    /// Accounts Interface to provide Bam account services.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Returns Bam account details.
        /// </summary>
        /// <param name="accountId"></param>
        Task<Account> GetByAccountIdAsync(int accountId);

        /// <summary>
        /// Returns the last date that has been completely processed (close quotes available and no open orders)
        /// </summary>
        /// <param name="accountId">The account id for the orders</param>
        /// <returns>Returns last date all orders for the account have been processed</returns>
        Task<DateTime?> GetLastCompletelyProcessedDateAsync(int accountId);

        /// <summary>
        /// Last date of update for the account asset value
        /// </summary>
        Task<DateTime?> LatestAssetValueUpdateAsync(int accountId);

        /// <summary>
        /// The buffer and reserve of the specified account
        /// </summary>
        Task<Balance> GetBalanceAsync(int accountId);

        /// <summary>
        /// The revenue percentage from the start of the account and the one for the current year
        /// </summary>
        Task<Revenue> GetRevenuePercentagesAsync(int accountId);

        /// <summary>
        /// The revenue since the last quarterly report
        /// </summary>
        /// <param name="accountId">account id</param>
        Task<QuarterlyRevenue> GetQuarterlyRevenueAsync(int accountId);
    }
}