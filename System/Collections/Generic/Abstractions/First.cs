using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a method for adding an element at the start of the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasAddFirst<T>:IEnumerable<T>
	{
		/// <summary>
		/// Adds an element at the start of the collection.
		/// </summary>
		/// <param name="element">
		/// The element to add.
		/// </param>
		void AddFirst(T element);
	}
	/// <summary>
	/// Provides a function for attempting to add an element at the start of the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasTryAddFirst<T> : IEnumerable<T>
	{
		/// <summary>
		/// Attempts to add an element at the start of the collection.
		/// </summary>
		/// <param name="element">
		/// The element to add.
		/// </param>
		/// <returns>
		/// <see langword="true"/>
		///  if the element was successfully added; otherwise, 
		/// <see langword="false"/>.
		/// </returns>
		Boolean TryAddFirst(T element);
	}

	/// <summary>
	/// Provides a method for removing the first element from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasRemoveFirst<out T> : IEnumerable<T>
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
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasTryRemoveFirst<T> : IEnumerable<T>
	{
		/// <summary>
		/// Attempts to remove the first element from the collection.
		/// </summary>
		/// <param name="element">
		/// The first element, if it was successfully removed; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/>
		///  if the first element was successfully removed; otherwise, 
		/// <see langword="false"/>.
		/// </returns>
		bool TryRemoveFirst(out T element);
	}

	/// <summary>
	/// Provides a function for retrieving the first element from the collection without removing it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasGetFirst<out T> : IEnumerable<T>
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
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasTryGetFirst<T> : IEnumerable<T>
	{
		/// <summary>
		/// Attempts to retrieve the first element from the collection without removing it.
		/// </summary>
		/// <param name="element">
		/// The first element, if it was successfully retrieved; otherwise, <see langword="default"/>.
		/// </param>
		/// <returns>
		/// <see langword="true"/>
		///  if the first element was successfully retrieved; otherwise, 
		/// <see langword="false"/>.
		/// </returns>
		Boolean TryGetFirst(out T element);
	}

	/// <summary>
	/// Provides a method for setting the first element in the collection, replacing any previously first element.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasSetFirst<T> : IEnumerable<T>
	{
		/// <summary>
		/// Sets the first element in the collection, replacing any previously first element.
		/// </summary>
		/// <param name="element">
		/// The element to set.
		/// </param>
		void SetFirst(T element);
	}
	/// <summary>
	/// Provides a function for attempting to set the first element in the collection, replacing any previously first element.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasTrySetFirst<T> : IEnumerable<T>
	{
		/// <summary>
		/// Attempts to set the first element in the collection, replacing any previously first element.
		/// </summary>
		/// <param name="element">
		/// The element at to set.
		/// </param>
		/// <returns>
		/// <see langword="true"/>
		///  if the first element was successfully set; otherwise, 
		/// <see langword="false"/>.
		/// </returns>
		Boolean TrySetFirst(T element);
	}
}
