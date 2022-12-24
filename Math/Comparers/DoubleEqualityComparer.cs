using Fort;
using RhoMicro.Common.Math.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.Math.Comparers
{
	/// <summary>
	/// Equality comparer for instances of <see cref="Double"/>.
	/// </summary>
	public readonly struct DoubleEqualityComparer : IEquatable<DoubleEqualityComparer>, IEqualityComparer<Double>
	{
		private DoubleEqualityComparer(Double absoluteTolerance, Double relativeTolerance)
		{
			AbsoluteTolerance = absoluteTolerance;
			RelativeTolerance = relativeTolerance;
		}
		/// <summary>
		/// The absolute tolerance for comparing two values for equality.
		/// </summary>
		public Double AbsoluteTolerance { get; }
		/// <summary>
		/// The relative tolerance for comparing two values for equality.
		/// </summary>
		public Double RelativeTolerance { get; }
		/// <summary>
		/// The default comparer instance.
		/// </summary>
		public static readonly IEqualityComparer<Double> Default = new DoubleEqualityComparer();

		/// <summary>
		/// Creates a new instance with the absolute and reöative tolerance provided.
		/// </summary>
		/// <param name="absoluteTolerance">The new comparers absolute tolerance.</param>
		/// <param name="relativeTolerance">The nw comparers relative tolerance.</param>
		/// <returns>A new instance of <see cref="IEqualityComparer{T}"/>.</returns>
		public static IEqualityComparer<Double> Create(Double absoluteTolerance, Double relativeTolerance)
		{
			absoluteTolerance.ThrowIfNot(t => t >= 0, $"{nameof(absoluteTolerance)} must be >= 0.", nameof(absoluteTolerance));
			relativeTolerance.ThrowIfNot(t => t >= 0, $"{nameof(relativeTolerance)} must be >= 0.", nameof(relativeTolerance));

			var result = new DoubleEqualityComparer(absoluteTolerance, relativeTolerance);

			return result;
		}
		/// <inheritdoc/>
		public override Boolean Equals(Object obj)
		{
			return obj is DoubleEqualityComparer comparer && Equals(comparer);
		}

		/// <inheritdoc/>
		public Boolean Equals(DoubleEqualityComparer other)
		{
			return AbsoluteTolerance == other.AbsoluteTolerance &&
				   RelativeTolerance == other.RelativeTolerance;
		}

		/// <summary>
		/// Returns the bounds of tolerance afforded to a value by <see cref="AbsoluteTolerance"/> and <see cref="RelativeTolerance"/>.
		/// </summary>
		/// <param name="value">The value whose tolerance bounds to calculate.</param>
		/// <returns>A new bounded interval that defines the tolerance for <paramref name="value"/>.</returns>
		public IBoundedInterval<Double> GetToleranceBounds(Double value)
		{
			var tolerance = value * RelativeTolerance + AbsoluteTolerance;
			var rightBound = value + tolerance;
			var leftBound = value - tolerance;
			var result = BoundedInterval<Double>.Create(leftBound, rightBound, true, true);

			return result;
		}

		/// <inheritdoc/>
		public Boolean Equals(Double x, Double y)
		{
			var result = GetToleranceBounds(x).Contains(y);

			return result;
		}

		/// <inheritdoc/>
		public override Int32 GetHashCode()
		{
			return HashCode.Combine(AbsoluteTolerance, RelativeTolerance);
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(Double obj)
		{
			var bounds = GetToleranceBounds(obj);
			return BoundedIntervalEqualityComparer<Double>.Instance.GetHashCode(bounds);
		}

		/// <inheritdoc/>
		public static Boolean operator ==(DoubleEqualityComparer left, DoubleEqualityComparer right)
		{
			return left.Equals(right);
		}

		/// <inheritdoc/>
		public static Boolean operator !=(DoubleEqualityComparer left, DoubleEqualityComparer right)
		{
			return !(left == right);
		}
	}
}
