using System;

// ReSharper disable once UnusedAutoPropertyAccessor.Global

namespace GraphQl3.GraphQl.Models
{
    /// <summary>
    /// Model for the floor change
    /// </summary>
    public class FloorChange
    {
        /// <summary>
        /// Id
        /// </summary>
        public int CppiFloorHistoryId { get; set; }

        /// <summary>
        /// Account cppi config id
        /// </summary>
        public int AccountCppiConfigId { get; set; }

        /// <summary>
        /// Floor value
        /// </summary>
        public double CppiFloor { get; set; }

        /// <summary>
        /// Change date time
        /// </summary>
        public DateTime ChangeDateTime { get; set; }

        /// <summary>
        /// Cash flow
        /// </summary>
        public double? CashFlow { get; set; }

        /// <summary>
        /// Reason for change
        /// </summary>
        public FloorChangeReason ChangeReason { get; set; }
    }
}
