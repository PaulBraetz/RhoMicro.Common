using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides an indexed property for setting the element at a specified index in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasIndexedSet<T, in TIndex> : IEnumerable<T>
	{
		/// <summary>
		/// Sets the element at the specified index in the collection.
		/// </summary>
		/// <param name="index">
		/// The index whose element to set.
		/// </param>
		T this[TIndex index] { set; }
	}
	/// <summary>
	/// Provides an indexed property for setting the element at a specified index in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasIndexedSet<T> : IHasIndexedSet<T, Int32>
	{
	}
}
