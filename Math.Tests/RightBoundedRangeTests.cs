namespace RhoMicro.Common.Math.Tests
{
	[TestClass]
	public class RightBoundedRangeTests
	{
		private static Object[][] ContainsData
		{
			get
			{
				return new Object[][]
				{
					new object[]{15,true,0},
new object[]{15,true,-2147483648},
new object[]{15,true,15},
new object[]{15,false,0},
new object[]{15,false,-2147483648},
new object[]{15,false,14},
new object[]{22,true,0},
new object[]{22,true,-2147483648},
new object[]{22,true,22},
new object[]{22,false,0},
new object[]{22,false,-2147483648},
new object[]{22,false,21},
new object[]{71,true,0},
new object[]{71,true,-2147483648},
new object[]{71,true,71},
new object[]{71,false,0},
new object[]{71,false,-2147483648},
new object[]{71,false,70},
new object[]{82,true,0},
new object[]{82,true,-2147483648},
new object[]{82,true,82},
new object[]{82,false,0},
new object[]{82,false,-2147483648},
new object[]{82,false,81},
new object[]{59,true,0},
new object[]{59,true,-2147483648},
new object[]{59,true,59},
new object[]{59,false,0},
new object[]{59,false,-2147483648},
new object[]{59,false,58}
				};
			}
		}
		private static Object[][] NotContainsData
		{
			get
			{
				return new Object[][]
				{
					new object[]{26,false,2147483647},
new object[]{26,false,26},
new object[]{26,true,2147483647},
new object[]{26,true,27},
new object[]{34,false,2147483647},
new object[]{34,false,34},
new object[]{34,true,2147483647},
new object[]{34,true,35},
new object[]{22,false,2147483647},
new object[]{22,false,22},
new object[]{22,true,2147483647},
new object[]{22,true,23},
new object[]{13,false,2147483647},
new object[]{13,false,13},
new object[]{13,true,2147483647},
new object[]{13,true,14},
new object[]{23,false,2147483647},
new object[]{23,false,23},
new object[]{23,true,2147483647},
new object[]{23,true,24}
				};
			}
		}

		public void ContainsDataGenerator()
		{
			var results = Enumerable.Range(0, 5)
				.Select(i => Random.Shared.Next(1, 100))
				.SelectMany(b => new[] { true, false }.Select(c => (rightClosed: c, rightBound: b)))
				.SelectMany(t => new[]{
					(t.rightBound, t.rightClosed, value:0),
					(t.rightBound, t.rightClosed, value:Int32.MinValue),
					(t.rightBound, t.rightClosed, value:t.rightClosed?t.rightBound:(t.rightBound-1))
				}).Select(t => $@"new object[]{{{t.rightBound},{t.rightClosed},{t.value}}}".ToLower());
			var result = String.Join(",\r\n", results);
		}
		public void NotContainsDataGenerator()
		{
			var results = Enumerable.Range(0, 5)
				.Select(i => Random.Shared.Next(1, 100))
				.SelectMany(b => new[] { false, true }.Select(c => (rightClosed: c, rightBound: b)))
				.SelectMany(t => new[]{
					(t.rightBound, t.rightClosed, value:Int32.MaxValue),
					(t.rightBound, t.rightClosed, value:t.rightClosed?t.rightBound+1:t.rightBound),
				}).Select(t => $@"new object[]{{{t.rightBound},{t.rightClosed},{t.value}}}".ToLower());
			var result = String.Join(",\r\n", results);
		}

		[TestMethod]
		[DynamicData(nameof(ContainsData))]
		public void Contains(Int32 rightBound, Boolean rightClosed, Int32 value)
		{
			var interval = RightBoundedInterval<Int32>.Create(rightBound, rightClosed);
			var actual = interval.Contains(value);

			Assert.IsTrue(actual);
		}
		[TestMethod]
		[DynamicData(nameof(NotContainsData))]
		public void NotContains(Int32 rightBound, Boolean rightClosed, Int32 value)
		{
			var interval = RightBoundedInterval<Int32>.Create(rightBound, rightClosed);
			var actual = interval.Contains(value);

			Assert.IsFalse(actual);
		}
	}
}
