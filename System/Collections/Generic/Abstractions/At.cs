using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	#region Generic Index
	/// <summary>
	/// Provides a function for determining whether an element can be found at a specified index in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasHasElementAt<out T, in TIndex> 
	{
		/// <summary>
		/// Determines whether an element can be found at a specified index in the collection.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		Boolean HasElementAt(TIndex index);
	}
	/// <summary>
	/// Provides a function for inserting an element at a specified index to the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasInsertAt<T, in TIndex> 
	{
		/// <summary>
		/// Inserts an element at the specified index to the collection.
		/// </summary>
		/// <param name="index">
		/// The index at which to insert an element.
		/// </param>
		/// <param name="element">
		/// The element to insert.
		/// </param>
		void InsertAt(TIndex index, T element);
	}
	/// <summary>
	/// Provides a function for attempting to insert an element at a specified index to the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasTryInsertAt<T, in TIndex> 
	{
		/// <summary>
		/// Attempts to insert an element at the specified index to the collection.
		/// </summary>
		/// <param name="index">
		/// The index at which to attempt to insert an element.
		/// </param>
		/// <param name="element">
		/// The element to insert.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the element was successfully inserted; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TryInsertAt(TIndex index, T element);
	}

	/// <summary>
	/// Provides a function for removing an element at a specified index from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasRemoveAt<out T, in TIndex> 
	{
		/// <summary>
		/// Removes an element at the specified index from the collection.
		/// </summary>
		/// <param name="index">
		/// The index at which to remove an element.
		/// </param>
		/// <returns>
		/// The element removed.
		/// </returns>
		T RemoveAt(TIndex index);
	}
	/// <summary>
	/// Provides a function for attempting to remove an element at a specified index from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasTryRemoveAt<T, in TIndex> 
	{
		/// <summary>
		/// Attempts to remove an element at the specified index from the collection.
		/// </summary>
		/// <param name="index">
		/// The index at which to attempt to remove an element.
		/// </param>
		/// <param name="element">
		/// The element at the specified index, if it was successfully removed; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true "/> if the element was successfully removed; otherwise, <see langword="false"/>.
		/// </returns>
		bool TryRemoveAt(TIndex index, out T element);
	}

	/// <summary>
	/// Provides a function for retrieving an element at a specified index from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasGetAt<out T, in TIndex> 
	{
		/// <summary>
		/// Retrieves an element at the specified index from the collection without removing it.
		/// </summary>
		/// <param name="index">
		/// The index at which to retrieve an element.
		/// </param>
		/// <returns>
		/// The element at the specified index.
		/// </returns>
		T GetAt(TIndex index);
	}
	/// <summary>
	/// Provides a function for attempting to retrieve the an element at a specified index from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasTryGetAt<T, in TIndex> 
	{
		/// <summary>
		/// Attempts to retrieve an element at the specified index from the collection without removing it.
		/// </summary>
		/// <param name="index">
		/// The index at which to attempt to retrieve an element.
		/// </param>
		/// <param name="element">
		/// The element at the specified index, if it was successfully retrieved; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the element was successfully retrieved; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TryGetAt(TIndex index, out T element);
	}

	/// <summary>
	/// Provides a function for setting an element at a specified index in the collection, replacing any element previously at that index.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasSetAt<T, in TIndex> 
	{
		/// <summary>
		/// Sets an element at the specified index in the collection, replacing any element previously at that index.
		/// </summary>
		/// <param name="index">
		/// The index at which to set an element.
		/// </param>
		/// <param name="element">
		/// The element to set.
		/// </param>
		/// <returns>
		/// The element previously found at the specified index, if one exists; otherwise, <see langword="default"/>.
		/// </returns>
		T SetAt(TIndex index, T element);
	}
	/// <summary>
	/// Provides a function for attempting to set an element at a specified index in the collection, replacing any element previously at that index.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasTrySetAt<T, in TIndex> 
	{
		/// <summary>
		/// Attempts to set an element at the specified index in the collection, replacing any element previously at that index.
		/// </summary>
		/// <param name="index">
		/// The index at which to attempt set an element.
		/// </param>
		/// <param name="element">
		/// The element at to set.
		/// </param>
		/// <param name="previous">
		/// The element previously found at the specified index, if one exists; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the element was successfully set; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TrySetAt(TIndex index, T element, out T previous);
	}
	#endregion
	#region Int32 Index
	/// <summary>
	/// Provides a function for determining whether an element can be found at a specified index in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasHasElementAt<out T> : IHasHasElementAt<T, Int32>
	{
	}
	/// <summary>
	/// Provides a function for inserting an element at a specified index to the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasInsertAt<T> : IHasInsertAt<T, Int32>
	{

	}
	/// <summary>
	/// Provides a function for attempting to insert an element at a specified index to the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryInsertAt<T> : IHasTryInsertAt<T, Int32>
	{
	}

	/// <summary>
	/// Provides a function for removing an element at a specified index from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasRemoveAt<out T> 
	{
	}
	/// <summary>
	/// Provides a function for attempting to remove an element at a specified index from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryRemoveAt<T> : IHasTryRemoveAt<T, Int32>
	{
	}

	/// <summary>
	/// Provides a function for retrieving an element at a specified index from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasGetAt<out T> 
	{
	}
	/// <summary>
	/// Provides a function for attempting to retrieve the an element at a specified index from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryGetAt<T> : IHasTryGetAt<T, Int32>
	{
	}

	/// <summary>
	/// Provides a function for setting an element at a specified index in the collection, replacing any element previously at that index.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasSetAt<T> : IHasSetAt<T, Int32>
	{
	}
	/// <summary>
	/// Provides a function for attempting to set an element at a specified index in the collection, replacing any element previously at that index.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTrySetAt<T> : IHasTrySetAt<T, Int32>
	{
	}
	#endregion
}
