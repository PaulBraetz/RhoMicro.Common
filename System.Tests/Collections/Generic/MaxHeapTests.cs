using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhoMicro.Common.System.Collections.Generic;
using RhoMicro.Common.System.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Tests.Collections.Generic
{
	[TestClass]
	public class MaxHeapTests
	{
		private static Object[][] PushPopData
		{
			get
			{
				return new Object[][]
				{
					new[]{new[] { 10, 5, 2, 4, 8, 9, 2, 5 }},
					new[]{new[] { 0, 1, 2, 3, 4, 5 }},
					new[]{new[] { 5, 4, 3, 2, 1 }},
					new[]{new[] { 0, 1, 3, 2, 3, 4, 3, 5 }},
					new[]{new[] { 5, 4, 3, 3, 2, 3, 1 }},
					new[]{new[] { 11,11,11,11,500,11}},
					new[]{ Enumerable.Range(0, 10)},
					new[]{ Enumerable.Range(0, 100)},
					new[]{ Enumerable.Range(0, 1_000)},
					new[]{ Enumerable.Range(0, 10_000) },
					new[]{ Enumerable.Range(0, 100_000) },
					//new[]{ Enumerable.Range(0, 1_000_000) },
					new[]{ Enumerable.Range(0, 10).Select(i=>-1*i)},
					new[]{ Enumerable.Range(0, 100).Select(i=>-1*i)},
					new[]{ Enumerable.Range(0, 1_000).Select(i=>-1*i)},
					new[]{ Enumerable.Range(0, 10_000).Select(i=>-1*i) },
					new[]{ Enumerable.Range(0, 100_000).Select(i=>-1*i) },
					//new[]{ Enumerable.Range(0, 1_000_000).Select(i=>-1*i) }
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(PushPopData))]
		public void PushPop(IEnumerable<Int32> items)
		{
			var comparer = Comparer.Create<Int32>();
			var heap = new MaxHeap<Int32>(comparer);
			foreach (var item in items)
			{
				heap.Insert(item);
			}

			var lastItem = Int32.MaxValue;
			while (heap.Size > 0)
			{
				var nextItem = heap.RemoveFirst();
				Assert.IsTrue(nextItem <= lastItem);
				lastItem = nextItem;
			}
		}
	}
}
