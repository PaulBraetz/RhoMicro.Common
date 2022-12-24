using RhoMicro.Common.Math.Abstractions;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.Math.Comparers
{
	/// <summary>
	/// Equality comparer for instances of <see cref="ILeftBoundedInterval{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the intervals.</typeparam>
	public sealed class LeftBoundedIntervalEqualityComparer<T> : IEqualityComparer<ILeftBoundedInterval<T>>
	{
		private LeftBoundedIntervalEqualityComparer() { }

		/// <summary>
		/// Instance of <see cref="IEqualityComparer{T}"/> where <c>T</c> is <see cref="ILeftBoundedInterval{T}"/>.
		/// </summary>
		public static readonly IEqualityComparer<ILeftBoundedInterval<T>> Instance = new LeftBoundedIntervalEqualityComparer<T>();

		/// <inheritdoc/>
		public Boolean Equals(ILeftBoundedInterval<T> x, ILeftBoundedInterval<T> y)
		{
			if (Object.ReferenceEquals(x, y))
			{
				return true;
			}
			else if (x == null || y == null)
			{
				return false;
			}

			var result = x.LeftClosed == y.LeftClosed &&
						 EqualityComparer<T>.Default.Equals(x.LeftBound, y.LeftBound);

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(ILeftBoundedInterval<T> obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			var result = HashCode.Combine(obj.LeftBound,
										  obj.LeftClosed);

			return result;
		}
	}
}
