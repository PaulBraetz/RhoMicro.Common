using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a function for inserting an element into the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasInsert<T> 
	{
		/// <summary>
		/// Inserts an element into the collection.
		/// </summary>
		/// <param name="element">
		/// The element to insert.
		/// </param>
		void Insert(T element);
	}
	/// <summary>
	/// Provides a function for attempting to insert an element into the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryInsert<T>
	{
		/// <summary>
		/// Attempts to insert an element into the collection.
		/// </summary>
		/// <param name="element">
		/// The element to insert.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the element was successfully inserted; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean TryInsert(T element);
	}
	/// <summary>
	/// Provides a function for removing an element from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasRemove<T> 
	{
		/// <summary>
		/// Removes an element from the collection.
		/// </summary>
		/// <param name="element">
		/// The element to remove.
		/// </param>
		Boolean Remove(T element);
	}
}
