using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a function for inserting an element at the end of the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasInsertLast<T> 
	{
		/// <summary>
		/// Inserts an element at the end of the collection.
		/// </summary>
		/// <param name="element">
		/// The element to insert.
		/// </param>
		void InsertLast(T element);
	}
	/// <summary>
	/// Provides a function for attempting to insert an element at the end of the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryInsertLast<T> 
	{
		/// <summary>
		/// Attempts to insert an element at the end of the collection.
		/// </summary>
		/// <param name="element">
		/// The element to insert.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the last element was successfully inserted; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TryInsertLast(T element);
	}

	/// <summary>
	/// Provides a function for removing the last element from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasRemoveLast<out T> 
	{
		/// <summary>
		/// Removes the last element from the collection.
		/// </summary>
		/// <returns>
		/// The element removed.
		/// </returns>
		T RemoveLast();
	}
	/// <summary>
	/// Provides a function for attempting to remove the last element from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryRemoveLast<T> 
	{
		/// <summary>
		/// Attempts to remove the last element from the collection.
		/// </summary>
		/// <param name="element">
		/// The last element, if it was successfully removed; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the last element was successfully removed; otherwise, <see langword="false"/>.
		/// </returns>
		bool TryRemoveLast(out T element);
	}

	/// <summary>
	/// Provides a function for retrieving the last element from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasGetLast<out T> 
	{
		/// <summary>
		/// Retrieves the last element from the collection without removing it.
		/// </summary>
		/// <returns>
		/// The last element.
		/// </returns>
		T GetLast();
	}
	/// <summary>
	/// Provides a function for attempting to retrieve the last element from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryGetLast<T> 
	{
		/// <summary>
		/// Attempts to retrieve the last element from the collection without removing it.
		/// </summary>
		/// <param name="element">
		/// The last element, if it was successfully retrieved; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the last element was successfully retrieved; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TryGetLast(out T element);
	}

	/// <summary>
	/// Provides a function for setting the last element in the collection, replacing any previously last element.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasSetLast<T> 
	{
		/// <summary>
		/// Sets the last element in the collection, replacing any previously last element.
		/// </summary>
		/// <param name="element">
		/// The element to set.
		/// </param>
		/// <returns>
		/// The previously last element, if one exists; otherwise, <see langword="default"/>.
		/// </returns>
		T SetLast(T element);
	}
	/// <summary>
	/// Provides a function for attempting to set the last element in the collection, replacing any previously last element.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTrySetLast<T> 
	{
		/// <summary>
		/// Attempts to set the last element in the collection, replacing any previously last element.
		/// </summary>
		/// <param name="element">
		/// The element at to set.
		/// </param>
		/// <param name="previous">
		/// The previously last element, if one exists; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the last element was successfully set; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TrySetLast(T element, out T previous);
	}
}
