using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace GraphQl3.GraphQl.Models
{
    /// <summary>
    /// BAM account model
    /// </summary>
    public class Account
    {
        /// <summary>
        /// CPPI configuration
        /// </summary>
        public string CppiConfiguration { get; set; }

        /// <summary>
        /// The topline id for the account
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// The bam id for the account
        /// </summary>
        public int BamAccountId { get; set; }

        /// <summary>
        /// The current CPPI percentage
        /// </summary>
        public double CppiPercentage { get; set; }

        /// <summary>
        /// The management startdate
        /// </summary>
        public DateTime? ManagementStartDate { get; set; }

        /* Extra fields delivered by different services */

        /// <summary>
        /// The most recent date the account was processed
        /// </summary>
        public DateTime? LastProcessedDate { get; set; }

        /// <summary>
        /// The most recent date the account asset value was updated
        /// </summary>
        public DateTime? LastAssetValueUpdate { get; set; }
    }
}
