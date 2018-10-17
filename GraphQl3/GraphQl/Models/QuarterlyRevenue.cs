using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GraphQl3.GraphQl.Models
{
    /// <summary>
    /// The quarterly revenue model
    /// </summary>
    public class QuarterlyRevenue
    {
        /// <summary>
        /// The reported revenue
        /// </summary>
        public double RevenuePercentage { get; set; }

        /// <summary>
        /// Asset value on the last day of the previous quarter
        /// </summary>
        public double QuarterlyAssetValue { get; set; }

        /// <summary>
        /// Date of the last day of the previous quarter
        /// </summary>
        public DateTime QuarterlyAssetValueDate { get; set; }

        /// <summary>
        /// Latest asset value as reported in the CAT system
        /// </summary>
        public double LatestAssetValue { get; set; }

        /// <summary>
        /// Date of the latest asset value as reported in the CAT system
        /// </summary>
        public DateTime LatestAssetValueDate { get; set; }

        /// <summary>
        /// Indication if we have a drop to report
        /// </summary>
        public bool ReportDrop { get; set; }

        /// <summary>
        /// The reported bracket
        /// </summary>
        public int? DropBracket { get; set; }
    }
}
