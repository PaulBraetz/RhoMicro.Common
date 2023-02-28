using System;

namespace RhoMicro.Common.Math.Abstractions
{
	/// <summary>
	/// Represents an left-bounded interval.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the interval.</typeparam>
	public interface ILeftBoundedInterval<T>
	{
		/// <summary>
		/// Indicates whether or not <see cref="LeftBound"/> is to be included in the interval.
		/// </summary>
		Boolean LeftClosed { get; }
		/// <summary>
		/// The intervals left (usually lower) bound.
		/// </summary>
		T LeftBound { get; }
	}
}