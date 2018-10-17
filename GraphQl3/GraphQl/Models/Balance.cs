// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GraphQl3.GraphQl.Models
{
    /// <summary>
    /// The balance model
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// Total asset value including cash
        /// </summary>
        public double Capital { get; set; }

        /// <summary>
        /// The buffer on the account
        /// </summary>
        public double Buffer { get; set; }

        /// <summary>
        /// The reserve of an account
        /// </summary>
        public double Reserve { get; set; }

        /// <summary>
        /// The buffer in percentage of the total capital, null if no capital
        /// </summary>
        public double? BufferPercentage { get; set; }
    }
}
