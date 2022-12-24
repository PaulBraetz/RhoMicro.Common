using System;

namespace RhoMicro.Common.Math.Abstractions
{
	/// <summary>
	/// Represents a bound (defines upper and lower bound) interval.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the interval.</typeparam>
	public interface IBoundedInterval<T> : IRightBoundedInterval<T>, ILeftBoundedInterval<T>
	{
	}
}
