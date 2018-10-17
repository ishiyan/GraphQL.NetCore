using System;
using GenFu;
using GraphQl3.GraphQl.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once ClassNeverInstantiated.Global

namespace GraphQl3.Services
{
    /// <inheritdoc />
    public class CppiFloorService : ICppiFloorService
    {
        private const int MaxAccountId = 100;
        private static readonly Dictionary<int, FloorChange[]> Dictionary = PopulateFakeData();

        /// <inheritdoc />
        public async Task<FloorChange[]> SelectHistoryByAccountAsync(int accountId)
        {
            return await Task.Run(() =>
            {
                Debug.WriteLine($"CppiFloorService.SelectHistoryByAccount({accountId})");
                // Thread.Sleep(100);
                return Dictionary[accountId];
            });
        }

        /// <inheritdoc />
        public async Task<FloorChange> AddFloorChangeAsync(int accountId, double? cashFlow, FloorChangeReason changeReason)
        {
            return await Task.Run(() =>
            {
                Debug.WriteLine($"CppiFloorService.AddFloorChange({accountId}, {cashFlow}, {changeReason})");
                // Thread.Sleep(100);
                var item = new FloorChange
                {
                    CppiFloorHistoryId = 888,
                    AccountCppiConfigId = 999,
                    CashFlow = cashFlow,
                    ChangeReason = changeReason,
                    ChangeDateTime = DateTime.Now,
                    CppiFloor = 777
                };
                var list = Dictionary[accountId].ToList();
                list.Add(item);
                Dictionary[accountId] = list.ToArray();
                return item;
            });
        }

        private static Dictionary<int, FloorChange[]> PopulateFakeData()
        {
            ConfigureFakeFloorChange();

            var dictionary = new Dictionary<int, FloorChange[]>();
            for (int i = 1; i <= MaxAccountId; ++i)
            {
                var count = GenFu.GenFu.Random.Next(0, 10);
                dictionary.Add(i, count == 0 ? new FloorChange[0] : GenFu.GenFu.ListOf<FloorChange>(count).ToArray());
            }

            return dictionary;
        }

        private static void ConfigureFakeFloorChange()
        {
            GenFu.GenFu.Configure<FloorChange>()
                .Fill(_ => _.AccountCppiConfigId).WithinRange(1, 100)
                .Fill(_ => _.ChangeDateTime).AsPastDate()
                .Fill(_ => _.CppiFloorHistoryId).WithinRange(1, 100);
        }
    }
}