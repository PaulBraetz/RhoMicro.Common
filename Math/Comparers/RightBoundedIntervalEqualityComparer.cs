using RhoMicro.Common.Math.Abstractions;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.Math.Comparers
{
	/// <summary>
	/// Equality comparer for instances of <see cref="IRightBoundedInterval{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the intervals.</typeparam>
	public sealed class RightBoundedIntervalEqualityComparer<T> : IEqualityComparer<IRightBoundedInterval<T>>
	{
		private RightBoundedIntervalEqualityComparer() { }

		/// <summary>
		/// Instance of <see cref="IEqualityComparer{T}"/> where <c>T</c> is <see cref="IRightBoundedInterval{T}"/>.
		/// </summary>
		public static readonly IEqualityComparer<IRightBoundedInterval<T>> Instance = new RightBoundedIntervalEqualityComparer<T>();

		/// <inheritdoc/>
		public Boolean Equals(IRightBoundedInterval<T> x, IRightBoundedInterval<T> y)
		{
			if (Object.ReferenceEquals(x, y))
			{
				return true;
			}
			else if (x == null || y == null)
			{
				return false;
			}

			var result = x.RightClosed == y.RightClosed &&
						 EqualityComparer<T>.Default.Equals(x.RightBound, y.RightBound);

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(IRightBoundedInterval<T> obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			var result = HashCode.Combine(obj.RightBound,
										  obj.RightClosed);

			return result;
		}
	}
}
