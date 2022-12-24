using RhoMicro.Common.Math.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.Math.Comparers
{
	/// <summary>
	/// Equality comparer for instances of <see cref="IBoundedInterval{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the intervals.</typeparam>
	public sealed class BoundedIntervalEqualityComparer<T> : IEqualityComparer<IBoundedInterval<T>>
	{
		private BoundedIntervalEqualityComparer() { }

		/// <summary>
		/// Instance of <see cref="IEqualityComparer{T}"/> where <c>T</c> is <see cref="IBoundedInterval{T}"/>.
		/// </summary>
		public static readonly IEqualityComparer<IBoundedInterval<T>> Instance = new BoundedIntervalEqualityComparer<T>();

		/// <inheritdoc/>
		public Boolean Equals(IBoundedInterval<T> x, IBoundedInterval<T> y)
		{
			if (Object.ReferenceEquals(x, y))
			{
				return true;
			}
			else if (x == null || y == null)
			{
				return false;
			}

			var result = x.IsEmpty() && y.IsEmpty() ||
						 x.IsDegenerate() && y.IsDegenerate() && EqualityComparer<T>.Default.Equals(x.LeftBound, y.LeftBound) ||
						 x.LeftClosed == y.LeftClosed &&
						 x.RightClosed == y.RightClosed &&
						 EqualityComparer<T>.Default.Equals(x.LeftBound, y.LeftBound) &&
						 EqualityComparer<T>.Default.Equals(x.RightBound, y.RightBound);

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(IBoundedInterval<T> obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			var result = HashCode.Combine(obj.LeftBound,
										  obj.LeftClosed,
										  obj.RightBound,
										  obj.RightClosed);

			return result;
		}
	}
}
