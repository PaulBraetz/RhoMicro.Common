using Fort;
using RhoMicro.Common.Math.Abstractions;
using RhoMicro.Common.Math.Comparers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace RhoMicro.Common.Math
{
	/// <summary>
	/// Default implementation of <see cref="IBoundedInterval{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the interval.</typeparam>
	[DebuggerDisplay("{ToString()}")]
	public readonly struct BoundedInterval<T> : IBoundedInterval<T>, IEquatable<IBoundedInterval<T>>
	{
		private BoundedInterval(T leftBound, T rightBound, Boolean leftClosed = true, Boolean rightClosed = false)
		{
			LeftBound = leftBound;
			RightBound = rightBound;
			LeftClosed = leftClosed;
			RightClosed = rightClosed;
		}

		/// <inheritdoc/>
		public T LeftBound { get; }
		/// <inheritdoc/>
		public T RightBound { get; }
		/// <inheritdoc/>
		public Boolean LeftClosed { get; }
		/// <inheritdoc/>
		public Boolean RightClosed { get; }

		/// <summary>
		/// Creates a left- and right-bounded interval.
		/// </summary>
		/// <param name="leftBound">The intervals left (usually lower) bound.</param>
		/// <param name="rightBound">The intervals right (usually upper) bound.</param>
		/// <param name="leftClosed">Indicates whether or not <paramref name="leftBound"/> is to be included in the interval. The default is <see langword="true"/>.</param>
		/// <param name="rightClosed">Indicates whether or not <paramref name="rightBound"/> is to be included in the interval. The default is <see langword="false"/>.</param>
		/// <returns>A new left- and right-bounded interval.</returns>
		public static IBoundedInterval<T> Create(T leftBound,
											   T rightBound,
											   Boolean leftClosed = true,
											   Boolean rightClosed = false)
		{
			var interval = new BoundedInterval<T>(leftBound, rightBound, leftClosed, rightClosed);

			return interval;
		}
		/// <summary>
		/// The empty interval.
		/// </summary>
		public static readonly IBoundedInterval<T> Empty = new BoundedInterval<T>();
		/// <summary>
		/// Creates a single-element interval.
		/// </summary>
		/// <param name="element">The intervals single element.</param>
		/// <returns>The degenerate (left- and right-closed) interval, whose left and right bound are <paramref name="element"/>.</returns>
		public static IBoundedInterval<T> Degenerate(T element)
		{
			var result = Create(element, element, true, true);

			return result;
		}

		/// <inheritdoc/>
		public override String ToString()
		{
			String result = $"{bracket(LeftClosed)}{LeftBound}, {RightBound}{bracket(!RightClosed)}";

			return result;

			Char bracket(Boolean inclusive)
			{
				return inclusive ? '[' : ']';
			}
		}

		/// <inheritdoc/>
		public override Boolean Equals(Object obj)
		{
			return obj is BoundedInterval<T> interval && Equals(interval);
		}

		/// <inheritdoc/>
		public Boolean Equals(IBoundedInterval<T> other)
		{
			return BoundedIntervalEqualityComparer<T>.Instance.Equals(this, other);
		}

		/// <inheritdoc/>
		public override Int32 GetHashCode()
		{
			return BoundedIntervalEqualityComparer<T>.Instance.GetHashCode(this);
		}

		/// <inheritdoc/>
		public static Boolean operator ==(BoundedInterval<T> left, BoundedInterval<T> right)
		{
			return left.Equals(right);
		}

		/// <inheritdoc/>
		public static Boolean operator !=(BoundedInterval<T> left, BoundedInterval<T> right)
		{
			return !(left == right);
		}
	}
}
