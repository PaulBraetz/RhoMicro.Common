using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a function for determining the index of the first occurence of an element in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasGetIndexOf<T, out TIndex> : IEnumerable<T>
	{
		/// <summary>
		/// Determines the index of the first occurence of an element in the collection.
		/// </summary>
		/// <param name="item">
		/// The item whose index to determine.
		/// </param>
		/// <returns>The index of the first occurence of <paramref name="item"/>
		/// .
		/// </returns>
		TIndex GetIndexOf(T item);
	}
	/// <summary>
	/// Provides a function for determining the index of the first occurence of an element in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasIndexOf<T>: IHasGetIndexOf<T, Int32>
	{
	}
}
