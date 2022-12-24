using RhoMicro.Common.Math.Abstractions;
using RhoMicro.Common.Math.Comparers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.Math
{
	/// <summary>
	/// Default implementation of <see cref="IRightBoundedInterval{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the interval.</typeparam>
	public readonly struct RightBoundedInterval<T> : IRightBoundedInterval<T>, IEquatable<IRightBoundedInterval<T>>
	{
		private RightBoundedInterval(T rightBound, Boolean rightClosed)
		{
			RightBound = rightBound;
			RightClosed = rightClosed;
		}

		/// <inheritdoc/>
		public Boolean RightClosed { get; }
		/// <inheritdoc/>
		public T RightBound { get; }

		/// <summary>
		/// Creates a right-bounded interval.
		/// </summary>
		/// <param name="rightBound">The intervals right (usually upper) bound.</param>
		/// <param name="rightClosed">Indicates wether or not <paramref name="rightBound"/> is to be included in the interval. The default is <see langword="false"/>.</param>
		/// <returns>A new right-bounded interval.</returns>
		public static IRightBoundedInterval<T> Create(T rightBound, Boolean rightClosed)
		{
			var result = new RightBoundedInterval<T>(rightBound, rightClosed);

			return result;
		}

		/// <inheritdoc/>
		public override Boolean Equals(Object obj)
		{
			return obj is IRightBoundedInterval<T> interval && Equals(interval);
		}

		/// <inheritdoc/>
		public Boolean Equals(IRightBoundedInterval<T> other)
		{
			return RightBoundedIntervalEqualityComparer<T>.Instance.Equals(this, other);
		}

		/// <inheritdoc/>
		public override Int32 GetHashCode()
		{
			return RightBoundedIntervalEqualityComparer<T>.Instance.GetHashCode(this);
		}

		/// <inheritdoc/>
		public override String ToString()
		{
			var result = $"]-inf, {RightBound}{(RightClosed ? ']' : '[')}";

			return result;
		}

		/// <inheritdoc/>
		public static Boolean operator ==(RightBoundedInterval<T> left, RightBoundedInterval<T> right)
		{
			return left.Equals(right);
		}

		/// <inheritdoc/>
		public static Boolean operator !=(RightBoundedInterval<T> left, RightBoundedInterval<T> right)
		{
			return !(left == right);
		}
	}
}
