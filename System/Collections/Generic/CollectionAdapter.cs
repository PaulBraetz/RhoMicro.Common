using Fort;
using RhoMicro.Common.System.Collections.Generic.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic
{
	/// <summary>
	/// Default implementation of <see cref="ICollectionAdapter{T}"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	/// <typeparam name="TCollection">
	/// The type of collection to adapt.
	/// </typeparam>
	public class CollectionAdapter<T, TCollection> : ICollectionAdapter<T>
		where TCollection : ICollection<T>
	{
		/// <summary>
		/// The underlying collection.
		/// </summary>
		protected TCollection BaseCollection;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public Int32 Length => BaseCollection.Count;

		public CollectionAdapter(TCollection collection)
		{
			collection.ThrowIfNull(nameof(collection));

			BaseCollection = collection;
		}

		public void Clear()
		{
			BaseCollection.Clear();
		}
		public Boolean TryRemoveFirst(T element)
		{
			return BaseCollection.Remove(element);
		}
		public void Add(T element)
		{
			BaseCollection.Add(element);
		}
		public Boolean Contains(T element)
		{
			return BaseCollection.Contains(element);
		}

		void ICollection<T>.Add(T item)
		{
			BaseCollection.Add(item);
		}
		void ICollection<T>.Clear()
		{
			BaseCollection.Clear();
		}
		Boolean ICollection<T>.Contains(T item)
		{
			return BaseCollection.Contains(item);
		}
		void ICollection<T>.CopyTo(T[] array, Int32 arrayIndex)
		{
			BaseCollection.CopyTo(array, arrayIndex);
		}
		Boolean ICollection<T>.Remove(T item)
		{
			return BaseCollection.Remove(item);
		}
		Int32 ICollection<T>.Count => BaseCollection.Count;
		Boolean ICollection<T>.IsReadOnly => BaseCollection.IsReadOnly;

		public IEnumerator<T> GetEnumerator()
		{
			return BaseCollection.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)BaseCollection).GetEnumerator();
		}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
