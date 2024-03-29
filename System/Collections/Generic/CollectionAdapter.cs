﻿using Fort;
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
	/// The type of elements contained in the collection.
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
		public CollectionAdapter(TCollection collection)
		{
			collection.ThrowIfNull(nameof(collection));

			BaseCollection = collection;
		}

		public Int32 Size => BaseCollection.Count;
		public void Clear()
		{
			BaseCollection.Clear();
		}
		public Boolean Remove(T element)
		{
			return BaseCollection.Remove(element);
		}
		public void Insert(T element)
		{
			BaseCollection.Add(element);
		}
		public Boolean Contains(T element)
		{
			return BaseCollection.Contains(element);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return BaseCollection.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)BaseCollection).GetEnumerator();
		}

		public void CopyTo(T[] array, Int32 arrayIndex)
		{
			BaseCollection.CopyTo(array, arrayIndex);
		}

		void ICollection<T>.Add(T item)
		{
			BaseCollection.Add(item);
		}		
		Int32 ICollection<T>.Count => BaseCollection.Count;
		Boolean ICollection<T>.IsReadOnly => BaseCollection.IsReadOnly;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
