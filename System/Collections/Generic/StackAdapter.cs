using RhoMicro.Common.System.Collections.Generic.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic
{
	/// <summary>
	/// Default implementation of <see cref="IStack{T}"/>. Adapts instances of <see cref="Stack{T}"/> to the <see cref="IStack{T}"/> interface.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TStack">
	/// The type of stack adapted by instances of this type.
	/// </typeparam>
	public class StackAdapter<T, TStack> : ReadOnlyCollectionAdapter<T, TStack>, IStack<T>
		where TStack : Stack<T>
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public StackAdapter(TStack collection) : base(collection)
		{
		}

		public void InsertFirst(T element)
		{
			BaseCollection.Push(element);
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
			return BaseCollection.Pop();
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
