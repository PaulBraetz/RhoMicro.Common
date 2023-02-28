using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides an indexed property for setting the element at a specified index in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasIndexedSetter<T, in TIndex> 
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
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasIndexedSetter<T> : IHasIndexedSetter<T, Int32>
	{
	}
}
