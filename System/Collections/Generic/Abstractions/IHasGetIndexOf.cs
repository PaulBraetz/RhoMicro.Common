using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a function for determining the index of the first occurence of an element in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasGetIndexOf<T, out TIndex> 
	{
		/// <summary>
		/// Determines the index of the first occurence of an element in the collection.
		/// </summary>
		/// <param name="element">
		/// The element whose index to determine.
		/// </param>
		/// <returns>
		/// The index of the first occurence of <paramref name="element"/>.
		/// </returns>
		TIndex GetIndexOf(T element);
	}
	/// <summary>
	/// Provides a function for determining the index of the first occurence of an element in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasGetIndexOf<T>: IHasGetIndexOf<T, Int32>
	{
	}
	/// <summary>
	/// Provides a function for attempting to determine the index of the first occurence of an element in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasTryGetIndexOf<T, TIndex> 
	{
		/// <summary>
		/// Attempts to determine the index of the first occurence of an element in the collection.
		/// </summary>
		/// <param name="element">
		/// The element whose index to determine.
		/// </param>
		/// <param name="index">
		/// The index of the first occurence of <paramref name="element"/> if it exists; otherwise, <c>0</c>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="element"/> exists in the collection; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TryGetIndexOf(T element, out TIndex index);
	}
	/// <summary>
	/// Provides a function for determining the index of the first occurence of an element in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryIndexOf<T> : IHasTryGetIndexOf<T, Int32>
	{
	}
}
