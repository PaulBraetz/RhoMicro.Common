using System;
using System.Collections;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides an indexed property for retrieving the element at a specified index from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasIndexedGetter<out T, in TIndex>:IEnumerable<T>
	{
		/// <summary>
		/// Retrieves an element at the specified index from the collection without removing it.
		/// </summary>
		/// <param name="index">
		/// The index whose element to retrieve.
		/// </param>
		/// <returns>
		/// The element at <paramref name="index"/>.
		/// </returns>
		T this[TIndex index] { get; }
	}
	/// <summary>
	/// Provides an indexed property for retrieving the element at a specified index from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasIndexedGetter<out T> : IHasIndexedGetter<T, Int32>
	{
	}
}
