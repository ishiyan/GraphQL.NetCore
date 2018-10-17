// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GraphQl3.GraphQl.Models
{
    /// <summary>
    /// The revenue model
    /// </summary>
    public class Revenue
    {
        /// <summary>
        /// Revenue percentage from the last day of the previous year till now
        /// </summary>
        public double RevenuePercentageCurrentYear { get; set; }

        /// <summary>
        /// Revenue percentage from the start of the account till now
        /// </summary>
        public double RevenuePercentageSinceStartAccount { get; set; }

        /// <summary>
        /// The asset value added to the calculation for the last day
        /// </summary>
        public double CurrentAssetValue { get; set; }
    }
}
