using System;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable PossibleNullReferenceException
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GraphQlClient.Response
{
    /// <summary>
    /// Represents the location where the <see cref="GraphQlError"/> has been found
    /// </summary>
    public class GraphQlLocation : IEquatable<GraphQlLocation>
    {
        /// <summary>
        /// The Column
        /// </summary>
        public uint Column { get; set; }

        /// <summary>
        /// The Line
        /// </summary>
        public uint Line { get; set; }

        #region IEquatable
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode() => Column.GetHashCode() ^ Line.GetHashCode();

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object
        /// </summary>
        /// <param name="obj">The object to compare with this instance</param>
        /// <returns>true if obj is an instance of <see cref="GraphQlLocation"/> and equals the value of the instance; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            // ReSharper disable once MergeCastWithTypeCheck
            if (obj is GraphQlLocation)
            {
                // ReSharper disable once TryCastAlwaysSucceeds
                return Equals(obj as GraphQlLocation);
            }
            return false;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object
        /// </summary>
        /// <param name="other">The object to compare with this instance</param>
        /// <returns>true if other is an instance of <see cref="GraphQlLocation"/> and equals the value of the instance; otherwise, false</returns>
        public bool Equals(GraphQlLocation other) => Equals(Column, other.Column) && Equals(Line, other.Line);

        /// <summary>
        /// Tests whether two specified <see cref="GraphQlLocation"/> instances are equivalent
        /// </summary>
        /// <param name="left">The <see cref="GraphQlLocation"/> instance that is to the left of the equality operator</param>
        /// <param name="right">The <see cref="GraphQlLocation"/> instance that is to the right of the equality operator</param>
        /// <returns>true if left and right are equal; otherwise, false</returns>
        public static bool operator ==(GraphQlLocation left, GraphQlLocation right) => left.Equals(right);

        /// <summary>
        /// Tests whether two specified <see cref="GraphQlLocation"/> instances are not equal
        /// </summary>
        /// <param name="left">The <see cref="GraphQlLocation"/> instance that is to the left of the not equal operator</param>
        /// <param name="right">The <see cref="GraphQlLocation"/> instance that is to the right of the not equal operator</param>
        /// <returns>true if left and right are unequal; otherwise, false</returns>
        public static bool operator !=(GraphQlLocation left, GraphQlLocation right) => !left.Equals(right);
        #endregion
    }
}
