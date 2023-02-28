using System;

namespace RhoMicro.Common.Math.Abstractions
{
	/// <summary>
	/// Represents a right-bounded interval.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the interval.</typeparam>
	public interface IRightBoundedInterval<T>
	{
		/// <summary>
		/// Indicates whether or not <see cref="RightBound"/> is to be included in the interval.
		/// </summary>
		Boolean RightClosed { get; }
		/// <summary>
		/// The intervals right (usually upper) bound.
		/// </summary>
		T RightBound { get; }
	}
}