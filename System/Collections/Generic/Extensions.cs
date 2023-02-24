using Fort;
using RhoMicro.Common.System.Collections.Generic.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic
{
	/// <summary>
	/// Provides extensions for adapting .Net collections onto interfaces found in the <c>RhoMicro.Common.System.Collections.Generic.Abstractions</c> namespace..
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Adapts an instance of <see cref="ICollection{T}"/> to the <see cref="ICollectionAdapter{T}"/> interface.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		/// <param name="collection">
		/// The collection to adapt.
		/// </param>
		/// <returns>A new instance of <see cref="ICollectionAdapter{T}"/>, with its underlying collection set to <paramref name="collection"/>
		/// .
		/// </returns>
		public static ICollectionAdapter<T> Adapt<T>(this ICollection<T> collection)
		{
			collection.ThrowIfNull(nameof(collection));

			var result = new CollectionAdapter<T, ICollection<T>>(collection);

			return result;
		}
		/// <summary>
		/// Adapts an instance of <see cref="IList{T}"/> to the <see cref="IListAdapter{T}"/> interface.
		/// </summary>
		/// <typeparam name="T">
		/// The type of elements in the collection.
		/// </typeparam>
		/// <param name="collection">
		/// The collection to adapt.
		/// </param>
		/// <returns>A new instance of <see cref="IListAdapter{T}"/>, with its underlying collection set to <paramref name="collection"/>
		/// .
		/// </returns>
		public static IListAdapter<T> Adapt<T>(this IList<T> collection)
		{
			collection.ThrowIfNull(nameof(collection));

			var result = new ListAdapter<T, IList<T>>(collection);

			return result;
		}
	}
}
