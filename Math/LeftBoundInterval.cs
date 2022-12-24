using RhoMicro.Common.Math.Abstractions;
using RhoMicro.Common.Math.Comparers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.Math
{
	/// <summary>
	/// Default implementation of <see cref="ILeftBoundedInterval{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the interval.</typeparam>
	public readonly struct LeftBoundedInterval<T> : ILeftBoundedInterval<T>, IEquatable<ILeftBoundedInterval<T>>
	{
		private LeftBoundedInterval(T leftBound, Boolean leftClosed)
		{
			LeftBound = leftBound;
			LeftClosed = leftClosed;
		}

		/// <inheritdoc/>
		public Boolean LeftClosed { get; }
		/// <inheritdoc/>
		public T LeftBound { get; }

		/// <summary>
		/// Creates a left-bounded interval.
		/// </summary>
		/// <param name="leftBound">The intervals left (usually upper) bound.</param>
		/// <param name="leftClosed">Indicates wether or not <paramref name="leftBound"/> is to be included in the interval. The default is <see langword="false"/>.</param>
		/// <returns>A new left-bounded interval.</returns>
		public static ILeftBoundedInterval<T> Create(T leftBound, Boolean leftClosed)
		{
			var result = new LeftBoundedInterval<T>(leftBound, leftClosed);

			return result;
		}

		/// <inheritdoc/>
		public override Boolean Equals(Object obj)
		{
			return obj is ILeftBoundedInterval<T> interval && Equals(interval);
		}

		/// <inheritdoc/>
		public Boolean Equals(ILeftBoundedInterval<T> other)
		{
			return LeftBoundedIntervalEqualityComparer<T>.Instance.Equals(this, other);
		}

		/// <inheritdoc/>
		public override Int32 GetHashCode()
		{
			return LeftBoundedIntervalEqualityComparer<T>.Instance.GetHashCode(this);
		}

		/// <inheritdoc/>
		public override String ToString()
		{
			var result = $"]-inf, {LeftBound}{(LeftClosed ? ']' : '[')}";

			return result;
		}

		/// <inheritdoc/>
		public static Boolean operator ==(LeftBoundedInterval<T> left, LeftBoundedInterval<T> right)
		{
			return left.Equals(right);
		}

		/// <inheritdoc/>
		public static Boolean operator !=(LeftBoundedInterval<T> left, LeftBoundedInterval<T> right)
		{
			return !(left == right);
		}
	}
}
