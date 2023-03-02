using RhoMicro.Common.System.Collections.Generic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RhoMicro.Common.System.Collections.Generic
{
	/// <summary>
	/// Default implementation of <see cref="IListAdapter{T}"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
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
		public T this[Int32 index] 
		{
			get => BaseCollection[index]; 
			set => BaseCollection[index] = value;
		}
		T IHasIndexedSetter<T, Int32>.this[Int32 index] { set => throw new NotImplementedException(); }
		public Int32 GetIndexOf(T element)
		{
			return BaseCollection.IndexOf(element);
		}
		public void InsertAt(Int32 index, T element)
		{
			BaseCollection.Insert(index, element);
		}
		public T RemoveAt(Int32 index)
		{
			var result = BaseCollection.ElementAt(index);
			BaseCollection.RemoveAt(index);

			return result;
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
		T IList<T>.this[Int32 index] { get => BaseCollection[index]; set => BaseCollection[index] = value; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
