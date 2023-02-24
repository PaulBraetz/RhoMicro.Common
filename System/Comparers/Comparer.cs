using Fort;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Comparers
{
	/// <summary>
	/// Provides functions for creating comparers.
	/// </summary>
	public static class Comparer
	{
		/// <summary>
		/// Creates a comparer utilizing the implementation of <see cref="IComparable{T}.CompareTo(T)"/> provided by <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of objects to compare using the comparer created.</typeparam>
		/// <returns>An new instance of <see cref="IComparer{T}"/>.</returns>
		public static IComparer<T> Create<T>()
			where T : IComparable<T>
		{
			var result = DefaultComparer<T>.Instance;

			return result;
		}
		/// <summary>
		/// Creates a comparer utilizing the comparison strategy provided.
		/// </summary>
		/// <typeparam name="T">The type of objects to compare using the comparer created.</typeparam>
		/// <returns>An new instance of <see cref="IComparer{T}"/>.</returns>
		public static IComparer<T> Create<T>(Func<T, T, Int32> strategy)
		{
			strategy.ThrowIfNull(nameof(strategy));

			var result = new ComparerStrategy<T>(strategy);

			return result;
		}
	}
}
