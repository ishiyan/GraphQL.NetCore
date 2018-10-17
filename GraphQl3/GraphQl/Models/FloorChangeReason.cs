// ReSharper disable UnusedMember.Global

namespace GraphQl3.GraphQl.Models
{
    /// <summary>
    /// Model for floor change reason
    /// </summary>
    public enum FloorChangeReason
    {
        /// <summary>
        /// Risk intake
        /// </summary>
        RiskIntake,

        /// <summary>
        /// Deposit
        /// </summary>
        Deposit,

        /// <summary>
        /// Withdrawal
        /// </summary>
        Withdrawal,

        /// <summary>
        /// Liquidation
        /// </summary>
        Liquidation,

        /// <summary>
        /// (Periodic)Reset by the system
        /// </summary>
        SystemReset
    }
}
