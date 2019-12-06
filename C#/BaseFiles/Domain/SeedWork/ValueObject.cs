using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable SA1300
namespace Rezare.rSite.Domain.SeedWork
#pragma warning restore SA1300
{
    /// <summary>
    /// Value object base implementation.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Determines if its two operands are equal.
        /// </summary>
        /// <remarks>
        /// This violates Sonar rule S3875 https://rules.sonarsource.com/csharp/RSPEC-3875.
        /// Suppressing S3875 is more appropriate, as ValueObject should be sealed if IEquatable is implemented.
        /// </remarks>
        /// <param name="left">The left side of the equality operator.</param>
        /// <param name="right">The right side of the equality operator.</param>
        /// <returns>
        ///   <c>true</c> if its operands are equal; otherwise, <c>false</c>.
        /// </returns>
#pragma warning disable S3875
        public static bool operator ==(ValueObject? left, ValueObject? right)
#pragma warning restore S3875
        {
            var isLeftNull = left is null;
            var isRightNull = right is null;

            if (isLeftNull && isRightNull)
            {
                return true;
            }

            if (isLeftNull || isRightNull)
            {
                return false;
            }

            return left!.Equals(right);
        }

        /// <summary>
        /// Determines if its two operands are not equal.
        /// </summary>
        /// <param name="left">The left side of the inequality operator.</param>
        /// <param name="right">The right side of the inequality operator.</param>
        /// <returns>
        ///   <c>true</c> if its operands are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

        /// <summary>
        /// Determines whether the specified object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to compare with this object.</param>
        /// <returns>
        ///   <c>true</c> if the specified object is equal to this object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            var isObjNullOrDifferentType = GetType() != obj?.GetType();

            if (isObjNullOrDifferentType)
            {
                return false;
            }

            var valueObject = (ValueObject)obj!;
            var valueObjectEqualityComponents = valueObject.GetEqualityComponents();

            return GetEqualityComponents().SequenceEqual(valueObjectEqualityComponents);
        }

        /// <summary>
        /// Generate a hash code for the current value object.
        /// </summary>
        /// <remarks>
        /// The HashCode class source code can be found here:
        /// https://github.com/dotnet/coreclr/blob/master/src/System.Private.CoreLib/shared/System/HashCode.cs.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.hashcode?view=netcore-2.1.
        /// This method is tested be checking that it produces the same hashcode for two instances of the same object.
        /// This is done because it is only guaranteed to produce the same hashcode per Operating System Process.
        /// </remarks>
        /// <returns>
        ///   The hash code for the current value object.
        /// </returns>
        public override int GetHashCode()
        {
            var hash = default(HashCode);

            foreach (var component in GetEqualityComponents())
            {
                hash.Add(component);
            }

            return hash.ToHashCode();
        }

        /// <summary>
        /// Gets the ValueObject components that will be used to compare equality.
        /// </summary>
        /// <returns>
        ///   The List of components used to compare equality.
        /// </returns>
        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
