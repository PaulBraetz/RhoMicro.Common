using Fort;
using RhoMicro.Common.System.Monads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Timers;

namespace RhoMicro.Common.System.Collections.Generic
{
	public sealed class MaxHeap<T>:
	{
		public MaxHeap(Int32 initialCapacity, IComparer<T> comparer)
		{
			comparer.ThrowIfNull(nameof(comparer));
			initialCapacity.ThrowIfNot(c => c >= 0, $"{nameof(initialCapacity)} must be >= 0.", nameof(initialCapacity));

			_comparer = comparer;
			_heap = new List<T>(initialCapacity);
		}

		public MaxHeap(IComparer<T> comparer) : this(DEFAULT_INITIAL_SIZE, comparer)
		{

		}

		private const Int32 DEFAULT_INITIAL_SIZE = 32;

		private readonly IComparer<T> _comparer;
		private readonly List<T> _heap;

		/// <summary>
		/// Gets the current size of the heap.
		/// </summary>
		public Int32 Size => _heap.Count;

		public void Add(T item)
		{
			_heap.Insert(0, item);
			MaxHeapify();
		}
		public T RemoveFirst()
		{
			if (Size == 0)
			{
				throw new InvalidOperationException("Unable to remove item: heap is empty.");
			}

			var head = _heap[0];
			_heap.Switch(0, Size - 1);
			_heap.RemoveAt(Size - 1);

			if (Size > 0)
			{
				MaxHeapify();
			}


			return head;
		}
		private void MaxHeapify()
		{
			var parent = 0;
			var largest = parent;

			do
			{
				parent = largest;

				var leftChild = GetLeftChild(parent);
				if (leftChild < Size && GreaterThanOrEqual(leftChild, largest))
				{
					largest = leftChild;
				}

				var rightChild = GetRightChild(parent);
				if (rightChild < Size && GreaterThanOrEqual(rightChild, largest))
				{
					largest = rightChild;
				}

				_heap.Switch(parent, largest);
			} while (parent != largest);
		}
		private Boolean GreaterThanOrEqual(Int32 xIndex, Int32 yIndex)
		{
			var x = _heap[xIndex];
			var y = _heap[yIndex];

			var result = y == null || x != null && _comparer.Compare(x, y) >= 0;

			return result;
		}
		private static Int32 GetLeftChild(Int32 parent)
		{
			var result = 2 * parent + 1;

			return result;
		}
		private static Int32 GetRightChild(Int32 parent)
		{
			var result = 2 * parent + 2;

			return result;
		}
		private static Int32 GetParent(Int32 child)
		{
			var result = (child - 1) / 2;

			return result;
		}
	}
}
