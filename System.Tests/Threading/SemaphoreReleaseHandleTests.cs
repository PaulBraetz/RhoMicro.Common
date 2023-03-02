using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhoMicro.Common.System.Threading;

namespace System.Tests.Threading
{
	[TestClass]
	public class SemaphoreReleaseHandleTests
	{
		private static Object[][] DisposeData
		{
			get
			{
				return new Object[][]{
					new Object[]{ 10, 1 },
					new Object[]{ 10, 4 },
					new Object[]{ 10, 10 },
					new Object[]{ 10, 3 },
					new Object[]{ 10, 2 }
				};
			}
		}
		private static Object[][] InvalidReleaseCountData
		{
			get
			{
				return new Object[][]{
					new Object[]{ 10, 0 },
					new Object[]{ 10, -4},
					new Object[]{ 10, Int32.MinValue}
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(DisposeData))]
		public void Dispose(Int32 initialCapacity, Int32 releaseCount)
		{
			var gate = new SemaphoreSlim(initialCapacity, initialCapacity);
			for (var i = 0; i < releaseCount; i++)
			{
				gate.Wait();
			}
			using (gate.GetReleaseHandle(releaseCount)) { }

			Assert.AreEqual(initialCapacity, gate.CurrentCount);
		}
		[TestMethod]
		[DynamicData(nameof(InvalidReleaseCountData))]
		public void InvalidReleaseCount(Int32 initialCapacity, Int32 releaseCount)
		{
			var gate = new SemaphoreSlim(initialCapacity, initialCapacity);
			Assert.ThrowsException<ArgumentException>(() => gate.GetReleaseHandle(releaseCount));
		}
	}
}
