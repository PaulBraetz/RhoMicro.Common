using Fort;
using RhoMicro.Common.System.Collections.Generic.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic
{
	/// <summary>
	/// Provides extensions for adapting .Net collections to interfaces found in the <c>RhoMicro.Common.System.Collections.Generic.Abstractions</c> namespace.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Adapts an instance of <see cref="global::System.Collections.Generic.ICollection{T}"/> to the <see cref="ICollectionAdapter{T}"/> interface.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements contained in the collection.
		/// </typeparam>
		/// <param name="collection">
		/// The collection to adapt.
		/// </param>
		/// <returns>
		/// A new instance of <see cref="ICollectionAdapter{T}"/>, with its underlying collection set to <paramref name="collection"/>.
		/// </returns>
		public static ICollectionAdapter<T> Adapt<T>(this ICollection<T>  collection)
		{ 
			collection.ThrowIfNull(nameof(collection));

			var result = new CollectionAdapter<T, ICollection<T>>(collection);

			return result;
		}
		/// <summary>
		/// Adapts an instance of <see cref="IReadOnlyCollection{T}"/> to the <see cref="IReadOnlyCollectionAdapter{T}"/> interface.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements contained in the collection.
		/// </typeparam>
		/// <param name="collection">
		/// The collection to adapt.
		/// </param>
		/// <returns>
		/// A new instance of <see cref="IReadOnlyCollectionAdapter{T}"/>, with its underlying collection set to <paramref name="collection"/>.
		/// </returns>
		public static IReadOnlyCollectionAdapter<T> Adapt<T>(this IReadOnlyCollection<T>  collection)
		{
			collection.ThrowIfNull(nameof(collection));

			var result = new ReadOnlyCollectionAdapter<T, IReadOnlyCollection<T>>(collection);

			return result;
		}
		/// <summary>
		/// Adapts an instance of <see cref="IList{T}"/> to the <see cref="IListAdapter{T}"/> interface.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements contained in the collection.
		/// </typeparam>
		/// <param name="collection">
		/// The collection to adapt.
		/// </param>
		/// <returns>
		/// A new instance of <see cref="IListAdapter{T}"/>, with its underlying collection set to <paramref name="collection"/>.
		/// </returns>
		public static IListAdapter<T> Adapt<T>(this IList<T> collection)
		{
			collection.ThrowIfNull(nameof(collection));

			var result = new ListAdapter<T, IList<T>>(collection);

			return result;
		}
		/// <summary>
		/// Adapts an instance of <see cref="Stack{T}"/> to the <see cref="IStack{T}"/> interface.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements contained in the collection.
		/// </typeparam>
		/// <param name="collection">
		/// The collection to adapt.
		/// </param>
		/// <returns>
		/// A new instance of <see cref="IStack{T}"/>, with its underlying collection set to <paramref name="collection"/>.
		/// </returns>
		public static IStack<T> Adapt<T>(this Stack<T> collection)
		{
			collection.ThrowIfNull(nameof(collection));

			var result = new StackAdapter<T, Stack<T>>(collection);

			return result;
		}
		/// <summary>
		/// Adapts an instance of <see cref="Queue{T}"/> to the <see cref="IQueue{T}"/> interface.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements contained in the collection.
		/// </typeparam>
		/// <param name="collection">
		/// The collection to adapt.
		/// </param>
		/// <returns>
		/// A new instance of <see cref="IQueue{T}"/>, with its underlying collection set to <paramref name="collection"/>.
		/// </returns>
		public static IQueue<T> Adapt<T>(this Queue<T> collection)
		{
			collection.ThrowIfNull(nameof(collection));

			var result = new QueueAdapter<T, Queue<T>>(collection);

			return result;
		}
	}
}
