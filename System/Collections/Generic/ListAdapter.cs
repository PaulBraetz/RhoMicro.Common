using RhoMicro.Common.System.Collections.Generic.Abstractions;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic
{
	/// <summary>
	/// Default implementation of <see cref="IListAdapter{T}"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	/// <typeparam name="TList">
	/// The type of collection to adapt.
	/// </typeparam>
	public class ListAdapter<T, TList> : CollectionAdapter<T, TList>, IListAdapter<T>
		where TList : IList<T>
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public ListAdapter(TList collection) : base(collection)
		{
		}

		public T RemoveAt(Int32 index)
		{
			var result = this[index];
			BaseCollection.RemoveAt(index);

			return result;
		}
		public T this[Int32 index]
		{
			get => BaseCollection[index];
			set => BaseCollection[index] = value;
		}
		public Int32 GetIndexOf(T item)
		{
			return BaseCollection.IndexOf(item);
		}

		Int32 IList<T>.IndexOf(T item)
		{
			return BaseCollection.IndexOf(item);
		}
		void IList<T>.Insert(Int32 index, T item)
		{
			BaseCollection.Insert(index, item);
		}
		void IList<T>.RemoveAt(Int32 index)
		{
			BaseCollection.RemoveAt(index);
		}
		T IList<T>.this[Int32 index]
		{
			get => BaseCollection[index];
			set => BaseCollection[index] = value;
		}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
