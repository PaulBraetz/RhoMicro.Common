using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhoMicro.Common.System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Tests.Threading
{
	[TestClass]
	public class TaskConsumerTests
	{
		private static Object[][] AddData
		{
			get
			{
				return new Object[][]
				{
					new Object[] { new[] { 10, 20, 30, 40, 50 } } ,
					new Object[] { new[] { 100, 200, 300, 400, 500 } },
					new Object[] { new[] {0, 0, 5,100 } }
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(AddData))]
		public async Task Add(Int32[] delays)
		{
			var consumer = new TaskConsumer();
			var largestDelay = 0;
			foreach (var delay in delays)
			{
				largestDelay = Math.Max(delay, largestDelay);
				var task = Task.Delay(delay);
				consumer.Add(task);
			}

			await Task.Delay(largestDelay * 2);

			Assert.AreEqual(0, consumer.Count);
		}
	}
}
