using GraphQl3.GraphQl.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.Services
{
    /// <inheritdoc />
    public class AccountService : IAccountService
    {
        private const int MaxAccountId = 100;
        private static readonly Dictionary<int, Account> AccountDictionary = PopulateFakeAccountData();
        private static readonly Dictionary<int, Balance> BalanceDictionary = PopulateFakeBalanceData();
        private static readonly Dictionary<int, Revenue> RevenueDictionary = PopulateFakeRevenueData();
        private static readonly Dictionary<int, QuarterlyRevenue> QuarterlyRevenueDictionary = PopulateFakeQuarterlyRevenueData();

        /// <inheritdoc />
        public async Task<Account> GetByAccountIdAsync(int accountId)
        {
            return await Task.Run(() =>
            {
                Debug.WriteLine($"AccountService.GetByAccountId({accountId})");
                // Thread.Sleep(100);
                Task.Delay(100);
                return AccountDictionary[accountId];
            });
        }

        /// <inheritdoc />
        public async Task<DateTime?> GetLastCompletelyProcessedDateAsync(int accountId)
        {
            return await Task.Run(() =>
            {
                Debug.WriteLine($"AccountService.GetLastCompletelyProcessedDate({accountId})");
                // Thread.Sleep(100);
                var account = AccountDictionary[accountId];
                return account.ManagementStartDate?.AddDays(3);
            });
        }

        /// <inheritdoc />
        public async Task<DateTime?> LatestAssetValueUpdateAsync(int accountId)
        {
            return await Task.Run(() =>
            {
                Debug.WriteLine($"AccountService.LatestAssetValueUpdate({accountId})");
                // Thread.Sleep(100);
                var account = AccountDictionary[accountId];
                return account.ManagementStartDate?.AddDays(4);
            });
        }

        /// <inheritdoc />
        public async Task<Balance> GetBalanceAsync(int accountId)
        {
            return await Task.Run(() =>
            {
                Debug.WriteLine($"AccountService.GetBalance({accountId})");
                // Thread.Sleep(100);
                return BalanceDictionary[accountId];
            });
        }

        /// <inheritdoc />
        public async Task<Revenue> GetRevenuePercentagesAsync(int accountId)
        {
            return await Task.Run(() =>
            {
                Debug.WriteLine($"AccountService.GetRevenuePercentages({accountId})");
                // Thread.Sleep(100);
                return RevenueDictionary[accountId];
            });
        }

        /// <inheritdoc />
        public async Task<QuarterlyRevenue> GetQuarterlyRevenueAsync(int accountId)
        {
            return await Task.Run(() =>
            {
                Debug.WriteLine($"AccountService.GetQuarterlyRevenue({accountId})");
                // Thread.Sleep(100);
                return QuarterlyRevenueDictionary[accountId];
            });
        }

        private static Dictionary<int, Account> PopulateFakeAccountData()
        {
            ConfigureFakeAccount();

            var dictionary = new Dictionary<int, Account>();
            for (int i = 1; i <= MaxAccountId; ++i)
            {
                bool inactive = GenFu.GenFu.Random.Next(0, 1000) < 300;
                var account = GenFu.GenFu.New<Account>();
                account.AccountId = i;
                if (inactive)
                {
                    account.ManagementStartDate = null;
                    Debug.WriteLine($"AccountService: accountId {i} has no management date");
                }

                dictionary.Add(i, account);
            }

            return dictionary;
        }

        private static Dictionary<int, Balance> PopulateFakeBalanceData()
        {
            var dictionary = new Dictionary<int, Balance>();
            for (int i = 1; i <= MaxAccountId; ++i)
                dictionary.Add(i, GenFu.GenFu.New<Balance>());

            return dictionary;
        }

        private static Dictionary<int, Revenue> PopulateFakeRevenueData()
        {
            var dictionary = new Dictionary<int, Revenue>();
            for (int i = 1; i <= MaxAccountId; ++i)
                dictionary.Add(i, GenFu.GenFu.New<Revenue>());

            return dictionary;
        }

        private static Dictionary<int, QuarterlyRevenue> PopulateFakeQuarterlyRevenueData()
        {
            var dictionary = new Dictionary<int, QuarterlyRevenue>();
            for (int i = 1; i <= MaxAccountId; ++i)
                dictionary.Add(i, GenFu.GenFu.New<QuarterlyRevenue>());

            return dictionary;
        }

        private static void ConfigureFakeAccount()
        {
            GenFu.GenFu.Configure<Account>()
                .Fill(_ => _.BamAccountId).WithinRange(1, 100)
                .Fill(_ => _.CppiConfiguration).WithRandom(new []{"AM_NL_CONFIG", "AM_BE_CONFIG"})
                .Fill(_ => _.CppiPercentage).WithRandom(new double[]{5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95});
        }
    }
}