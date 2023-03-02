using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections
{
	internal interface IPriorityQueueItem<TValue>
	{
		public TValue Value { get; }
		public Int32 Priority { get; }
	}
	internal sealed class MaxPriorityQueue<TValue>
	{
		private sealed class PriorityQueueItem : IPriorityQueueItem<TValue>
		{
			public TValue Value { get; }
			public Int32 Priority { get; set; }
		}

		private readonly List<PriorityQueueItem> _heap = new List<PriorityQueueItem>();
		private Int32 _heapSize;

		public void Insert(TValue value, Int32 priority)
		{

		}
		public IPriorityQueueItem<TValue> Max()
		{
			AssertHeapIndex(0);

			var result = _heap[0];

			return result;
		}
		public IPriorityQueueItem<TValue> ExtractMax()
		{
			AssertHeapIndex(0);

			var max = _heap[0];
			_heap[0] = _heap[_heapSize - 1];
			_heapSize--;

			MaxHeapify(0);

			return max;
		}

		private void MaxHeapify(Int32 index)
		{

		}

		public void IncreaseKey(TValue old)
		{

		}

		private void AssertHeapIndex(Int32 index)
		{
			if (_heapSize < index - 1)
			{
				throw new InvalidOperationException($"Heap is of size {_heapSize}, but index {index} was requested.");
			}
		}
	}
}
