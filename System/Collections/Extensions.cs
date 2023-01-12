using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.System.Collections
{
	/// <summary>
	/// Provides general extensions for collections.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Enumeratesthe elements of an instance of <see cref="IAsyncEnumerable{T}"/> into an instance of <see cref="List{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of element contained in <paramref name="enumerable"/>.</typeparam>
		/// <param name="enumerable">The colelction to enumerate.</param>
		/// <returns>An instance of <see cref="List{T}"/>, containing the leements found in <paramref name="enumerable"/>.</returns>
		public static async Task<List<T>> ToList<T>(this IAsyncEnumerable<T> enumerable)
		{
			var result = new List<T>();
			await foreach (var item in enumerable)
			{
				result.Add(item);
			}

			return result;
		}
	}
}
