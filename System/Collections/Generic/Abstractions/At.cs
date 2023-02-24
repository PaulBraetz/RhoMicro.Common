using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	internal class At
	{
		#region Generic Index

		/// <summary>
		/// Provides a method for adding an element at a specified index to the collection.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		/// <typeparam name="TIndex">
		/// The type of index supported by the collection.
		/// </typeparam>
		public interface IHasAddAt<T, in TIndex> : IEnumerable<T>
		{
			/// <summary>
			/// Adds an element at the specified index to the collection.
			/// </summary>
			/// <param name="index">
			/// The index at which to add an element.
			/// </param>
			/// <param name="element">
			/// The element to add.
			/// </param>
			void AddAt(TIndex index, T element);
		}
		/// <summary>
		/// Provides a function for attempting to add an element at a specified index to the collection.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		/// <typeparam name="TIndex">
		/// The type of index supported by the collection.
		/// </typeparam>
		public interface IHasTryAddAt<T, in TIndex> : IEnumerable<T>
		{
			/// <summary>
			/// Attempts to add an element at the specified index to the collection.
			/// </summary>
			/// <param name="index">
			/// The index at which to attempt to add an element.
			/// </param>
			/// <param name="element">
			/// The element to add.
			/// </param>
			/// <returns>
			/// <see langword="true"/>
			///  if the element was successfully added; otherwise, 
			/// <see langword="false"/>.
			/// </returns>
			Boolean TryAddAt(TIndex index, T element);
		}

		/// <summary>
		/// Provides a function for removing an element at a specified index from the collection.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		/// <typeparam name="TIndex">
		/// The type of index supported by the collection.
		/// </typeparam>
		public interface IHasRemoveAt<out T, in TIndex> : IEnumerable<T>
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
		/// The type of elements in the collection.
		/// </typeparam>
		/// <typeparam name="TIndex">
		/// The type of index supported by the collection.
		/// </typeparam>
		public interface IHasTryRemoveAt<T, in TIndex> : IEnumerable<T>
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
		/// The type of elements in the collection.
		/// </typeparam>
		/// <typeparam name="TIndex">
		/// The type of index supported by the collection.
		/// </typeparam>
		public interface IHasGetAt<out T, in TIndex> : IEnumerable<T>
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
		/// The type of elements in the collection.
		/// </typeparam>
		/// <typeparam name="TIndex">
		/// The type of index supported by the collection.
		/// </typeparam>
		public interface IHasTryGetAt<T, in TIndex> : IEnumerable<T>
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
		/// Provides a method for setting an element at a specified index in the collection, replacing any element previously at that index.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		/// <typeparam name="TIndex">
		/// The type of index supported by the collection.
		/// </typeparam>
		public interface IHasSetAt<T, in TIndex> : IEnumerable<T>
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
			void SetAt(TIndex index, T element);
		}
		/// <summary>
		/// Provides a function for attempting to set an element at a specified index in the collection, replacing any element previously at that index.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		/// <typeparam name="TIndex">
		/// The type of index supported by the collection.
		/// </typeparam>
		public interface IHasTrySetAt<T, in TIndex> : IEnumerable<T>
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
			/// <returns>
			/// <see langword="true"/> if the element was successfully set; otherwise, <see langword="false"/>.
			/// </returns>
			Boolean TrySetAt(TIndex index, T element);
		}
		#endregion
		#region Int32 Index
		/// <summary>
		/// Provides a method for adding an element at a specified index to the collection.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		public interface IHasAddAt<T> : IHasAddAt<T, Int32>
		{
			
		}
		/// <summary>
		/// Provides a function for attempting to add an element at a specified index to the collection.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		public interface IHasTryAddAt<T> : IHasTryAddAt<T, Int32>
		{
		}

		/// <summary>
		/// Provides a function for removing an element at a specified index from the collection.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		public interface IHasRemoveAt<out T> : IEnumerable<T>
		{
		}
		/// <summary>
		/// Provides a function for attempting to remove an element at a specified index from the collection.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		public interface IHasTryRemoveAt<T> : IHasTryRemoveAt<T, Int32>
		{
		}

		/// <summary>
		/// Provides a function for retrieving an element at a specified index from the collection without removing it.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		public interface IHasGetAt<out T> : IEnumerable<T>
		{
		}
		/// <summary>
		/// Provides a function for attempting to retrieve the an element at a specified index from the collection without removing it.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		public interface IHasTryGetAt<T> : IHasTryGetAt<T, Int32>
		{
		}

		/// <summary>
		/// Provides a method for setting an element at a specified index in the collection, replacing any element previously at that index.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		public interface IHasSetAt<T> : IHasSetAt<T, Int32>
		{
		}
		/// <summary>
		/// Provides a function for attempting to set an element at a specified index in the collection, replacing any element previously at that index.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		public interface IHasTrySetAt<T> : IHasTrySetAt<T, Int32>
		{
		}
		#endregion
	}
}
