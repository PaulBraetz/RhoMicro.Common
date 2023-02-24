using Fort;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Reflection;
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
		/// Enumerates the elements of an instance of <see cref="IAsyncEnumerable{T}"/> into an instance of <see cref="List{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of element contained in <paramref name="enumerable"/>
		/// .
		/// </typeparam>
		/// <param name="enumerable">
		/// The colelction to enumerate.
		/// </param>
		/// <returns>An instance of <see cref="List{T}"/>, containing the leements found in <paramref name="enumerable"/>
		/// .
		/// </returns>
		public static async Task<List<T>> ToList<T>(this IAsyncEnumerable<T> enumerable)
		{
			var result = new List<T>();
			await foreach (var item in enumerable)
			{
				result.Add(item);
			}

			return result;
		}
		/// <summary>
		/// Attempts to perform a tuple switch on two elements of a list.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the list.
		/// </typeparam>
		/// <param name="list">
		/// The list whose elements to switch.
		/// </param>
		/// <param name="xIndex">The index of the element to switch with the element at <paramref name="yIndex"/>
		/// .
		/// </param>
		/// <param name="yIndex">The index of the element to switch with the element at <paramref name="xIndex"/>
		/// .
		/// </param>
		/// <returns><see langword="true"/> if the switch succeeded; otherwise, <see langword="false"/>
		/// .
		/// </returns>
		public static Boolean TrySwitch<T>(this IList<T> list, Int32 xIndex, Int32 yIndex)
		{
			list.ThrowIfNull(nameof(list));

			var maxIndex = list.Count - 1;
			if (xIndex > maxIndex || yIndex > maxIndex)
			{
				return false;
			}
			if (xIndex == yIndex)
			{
				return true;
			}

			(list[xIndex], list[yIndex]) = (list[yIndex], list[xIndex]);
			return true;
		}
		/// <summary>
		/// Perform a tuple switch on two elements of a list.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the list.
		/// </typeparam>
		/// <param name="list">
		/// The list whose elements to switch.
		/// </param>
		/// <param name="xIndex">The index of the element to switch with the element at <paramref name="yIndex"/>
		/// .
		/// </param>
		/// <param name="yIndex">The index of the element to switch with the element at <paramref name="xIndex"/>
		/// .
		/// </param>
		public static void Switch<T>(this IList<T> list, Int32 xIndex, Int32 yIndex)
		{
			list.ThrowIfNull(nameof(list));
			if(xIndex >= list.Count)
			{
				throw new IndexOutOfRangeException(nameof(xIndex));
			}
			if (yIndex >= list.Count)
			{
				throw new IndexOutOfRangeException(nameof(yIndex));
			}
			if(xIndex == yIndex)
			{
				return;
			}

			(list[xIndex], list[yIndex]) = (list[yIndex], list[xIndex]);
		}
	}
}
