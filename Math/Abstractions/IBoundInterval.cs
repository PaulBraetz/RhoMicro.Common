using System;

namespace RhoMicro.Common.Math.Abstractions
{
	/// <summary>
	/// Represents a left- and right-bounded interval.
	/// </summary>
	/// <typeparam name="T">The type of values represented by the interval.</typeparam>
	public interface IBoundedInterval<T> : IRightBoundedInterval<T>, ILeftBoundedInterval<T>
	{
	}
}
