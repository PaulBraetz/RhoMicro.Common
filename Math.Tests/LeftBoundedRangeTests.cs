namespace RhoMicro.Common.Math.Tests
{
	[TestClass]
	public class LeftBoundedRangeTests
	{
		private static Object[][] ContainsData
		{
			get
			{
				return new Object[][]
				{
new object[]{-3,true,0},
new object[]{-3,true,2147483647},
new object[]{-3,true,-3},
new object[]{-3,false,0},
new object[]{-3,false,2147483647},
new object[]{-3,false,-2},
new object[]{-80,true,0},
new object[]{-80,true,2147483647},
new object[]{-80,true,-80},
new object[]{-80,false,0},
new object[]{-80,false,2147483647},
new object[]{-80,false,-79},
new object[]{-60,true,0},
new object[]{-60,true,2147483647},
new object[]{-60,true,-60},
new object[]{-60,false,0},
new object[]{-60,false,2147483647},
new object[]{-60,false,-59},
new object[]{-80,true,0},
new object[]{-80,true,2147483647},
new object[]{-80,true,-80},
new object[]{-80,false,0},
new object[]{-80,false,2147483647},
new object[]{-80,false,-79},
new object[]{-27,true,0},
new object[]{-27,true,2147483647},
new object[]{-27,true,-27},
new object[]{-27,false,0},
new object[]{-27,false,2147483647},
new object[]{-27,false,-26}
				};
			}
		}
		private static Object[][] NotContainsData
		{
			get
			{
				return new Object[][]
				{
new object[]{-71,false,-2147483648},
new object[]{-71,false,-71},
new object[]{-71,true,-2147483648},
new object[]{-71,true,-72},
new object[]{-88,false,-2147483648},
new object[]{-88,false,-88},
new object[]{-88,true,-2147483648},
new object[]{-88,true,-89},
new object[]{-7,false,-2147483648},
new object[]{-7,false,-7},
new object[]{-7,true,-2147483648},
new object[]{-7,true,-8},
new object[]{-24,false,-2147483648},
new object[]{-24,false,-24},
new object[]{-24,true,-2147483648},
new object[]{-24,true,-25},
new object[]{-86,false,-2147483648},
new object[]{-86,false,-86},
new object[]{-86,true,-2147483648},
new object[]{-86,true,-87}
				};
			}
		}

		public void ContainsDataGenerator()
		{
			var results = Enumerable.Range(0, 5)
				.Select(i => Random.Shared.Next(-100, -1))
				.SelectMany(b => new[] { true, false }.Select(c => (leftClosed: c, leftBound: b)))
				.SelectMany(t => new[]{
					(t.leftBound, t.leftClosed, value:0),
					(t.leftBound, t.leftClosed, value:Int32.MaxValue),
					(t.leftBound, t.leftClosed, value:t.leftClosed?t.leftBound:(t.leftBound+1))
				}).Select(t => $@"new object[]{{{t.leftBound},{t.leftClosed},{t.value}}}".ToLower());
			var result = String.Join(",\r\n", results);
		}
		public void NotContainsDataGenerator()
		{
			var results = Enumerable.Range(0, 5)
				.Select(i => Random.Shared.Next(-100, -1))
				.SelectMany(b => new[] { false, true }.Select(c => (leftClosed: c, leftBound: b)))
				.SelectMany(t => new[]{
					(t.leftBound, t.leftClosed, value:Int32.MinValue),
					(t.leftBound, t.leftClosed, value:t.leftClosed?t.leftBound-1:t.leftBound),
				}).Select(t => $@"new object[]{{{t.leftBound},{t.leftClosed},{t.value}}}".ToLower());
			var result = String.Join(",\r\n", results);
		}

		[TestMethod]
		[DynamicData(nameof(ContainsData))]
		public void Contains(Int32 leftBound, Boolean leftClosed, Int32 value)
		{
			var interval = LeftBoundedInterval<Int32>.Create(leftBound, leftClosed);
			var actual = interval.Contains(value);

			Assert.IsTrue(actual);
		}
		[TestMethod]
		[DynamicData(nameof(NotContainsData))]
		public void NotContains(Int32 leftBound, Boolean leftClosed, Int32 value)
		{
			var interval = LeftBoundedInterval<Int32>.Create(leftBound, leftClosed);
			var actual = interval.Contains(value);

			Assert.IsFalse(actual);
		}
	}
}
