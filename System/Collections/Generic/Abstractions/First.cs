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
	public interface IHasInsertFirst<T> 
	{
		/// <summary>
		/// Inserts an element at the end of the collection.
		/// </summary>
		/// <param name="element">
		/// The element to insert.
		/// </param>
		void InsertFirst(T element);
	}
	/// <summary>
	/// Provides a function for attempting to insert an element at the end of the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryInsertFirst<T> 
	{
		/// <summary>
		/// Attempts to insert an element at the end of the collection.
		/// </summary>
		/// <param name="element">
		/// The element to insert.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the first element was successfully inserted; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TryInsertFirst(T element);
	}

	/// <summary>
	/// Provides a function for removing the first element from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasRemoveFirst<out T> 
	{
		/// <summary>
		/// Removes the first element from the collection.
		/// </summary>
		/// <returns>
		/// The element removed.
		/// </returns>
		T RemoveFirst();
	}
	/// <summary>
	/// Provides a function for attempting to remove the first element from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryRemoveFirst<T> 
	{
		/// <summary>
		/// Attempts to remove the first element from the collection.
		/// </summary>
		/// <param name="element">
		/// The first element, if it was successfully removed; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the first element was successfully removed; otherwise, <see langword="false"/>.
		/// </returns>
		bool TryRemoveFirst(out T element);
	}

	/// <summary>
	/// Provides a function for retrieving the first element from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasGetFirst<out T> 
	{
		/// <summary>
		/// Retrieves the first element from the collection without removing it.
		/// </summary>
		/// <returns>
		/// The first element.
		/// </returns>
		T GetFirst();
	}
	/// <summary>
	/// Provides a function for attempting to retrieve the first element from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryGetFirst<T> 
	{
		/// <summary>
		/// Attempts to retrieve the first element from the collection without removing it.
		/// </summary>
		/// <param name="element">
		/// The first element, if it was successfully retrieved; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the first element was successfully retrieved; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TryGetFirst(out T element);
	}

	/// <summary>
	/// Provides a function for setting the first element in the collection, replacing any previously first element.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasSetFirst<T> 
	{
		/// <summary>
		/// Sets the first element in the collection, replacing any previously first element.
		/// </summary>
		/// <param name="element">
		/// The element to set.
		/// </param>
		/// <returns>
		/// The previously first element, if one exists; otherwise, <see langword="default"/>.
		/// </returns>
		T SetFirst(T element);
	}
	/// <summary>
	/// Provides a function for attempting to set the first element in the collection, replacing any previously first element.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTrySetFirst<T> 
	{
		/// <summary>
		/// Attempts to set the first element in the collection, replacing any previously first element.
		/// </summary>
		/// <param name="element">
		/// The element at to set.
		/// </param>
		/// <param name="previous">
		/// The previously first element, if one exists; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the first element was successfully set; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TrySetFirst(T element, out T previous);
	}
}
