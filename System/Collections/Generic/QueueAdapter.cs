using RhoMicro.Common.System.Collections.Generic.Abstractions;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic
{
	/// <summary>
	/// Default implementation of <see cref="IQueue{T}"/>. Adapts instances of <see cref="Queue{T}"/> to the <see cref="IQueue{T}"/> interface.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TQueue">
	/// The type of queue adapted by instances of this type.
	/// </typeparam>
	public class QueueAdapter<T, TQueue> : ReadOnlyCollectionAdapter<T, TQueue>, IQueue<T>
		where TQueue : Queue<T>
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public QueueAdapter(TQueue collection) : base(collection)
		{
		}

		public void InsertLast(T element)
		{
			BaseCollection.Enqueue(element);
		}
		public void Clear()
		{
			BaseCollection.Clear();
		}
		public Boolean Contains(T element)
		{
			return BaseCollection.Contains(element);
		}
		public T GetFirst()
		{
			return BaseCollection.Peek();
		}
		public T RemoveFirst()
		{
			return BaseCollection.Dequeue();
		}
		public T[] ToArray()
		{
			return BaseCollection.ToArray();
		}
		public void CopyTo(T[] array, Int32 arrayIndex)
		{
			BaseCollection.CopyTo(array, arrayIndex);
		}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
