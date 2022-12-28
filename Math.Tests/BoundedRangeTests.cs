using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.Math.Tests
{
	[TestClass]
	public class BoundedRangeTests
	{
		private static Object[][] ContainsData
		{
			get
			{
				return new Object[][]
				{new object[]{-8,5,false,false,0},
new object[]{-8,5,false,false,-7},
new object[]{-8,5,false,false,4},
new object[]{-8,5,false,true,0},
new object[]{-8,5,false,true,-7},
new object[]{-8,5,false,true,5},
new object[]{-8,5,true,false,0},
new object[]{-8,5,true,false,-8},
new object[]{-8,5,true,false,4},
new object[]{-8,5,true,true,0},
new object[]{-8,5,true,true,-8},
new object[]{-8,5,true,true,5},
new object[]{-6,8,false,false,0},
new object[]{-6,8,false,false,-5},
new object[]{-6,8,false,false,7},
new object[]{-6,8,false,true,0},
new object[]{-6,8,false,true,-5},
new object[]{-6,8,false,true,8},
new object[]{-6,8,true,false,0},
new object[]{-6,8,true,false,-6},
new object[]{-6,8,true,false,7},
new object[]{-6,8,true,true,0},
new object[]{-6,8,true,true,-6},
new object[]{-6,8,true,true,8},
new object[]{-7,9,false,false,0},
new object[]{-7,9,false,false,-6},
new object[]{-7,9,false,false,8},
new object[]{-7,9,false,true,0},
new object[]{-7,9,false,true,-6},
new object[]{-7,9,false,true,9},
new object[]{-7,9,true,false,0},
new object[]{-7,9,true,false,-7},
new object[]{-7,9,true,false,8},
new object[]{-7,9,true,true,0},
new object[]{-7,9,true,true,-7},
new object[]{-7,9,true,true,9},
new object[]{-9,8,false,false,0},
new object[]{-9,8,false,false,-8},
new object[]{-9,8,false,false,7},
new object[]{-9,8,false,true,0},
new object[]{-9,8,false,true,-8},
new object[]{-9,8,false,true,8},
new object[]{-9,8,true,false,0},
new object[]{-9,8,true,false,-9},
new object[]{-9,8,true,false,7},
new object[]{-9,8,true,true,0},
new object[]{-9,8,true,true,-9},
new object[]{-9,8,true,true,8},
new object[]{-7,8,false,false,0},
new object[]{-7,8,false,false,-6},
new object[]{-7,8,false,false,7},
new object[]{-7,8,false,true,0},
new object[]{-7,8,false,true,-6},
new object[]{-7,8,false,true,8},
new object[]{-7,8,true,false,0},
new object[]{-7,8,true,false,-7},
new object[]{-7,8,true,false,7},
new object[]{-7,8,true,true,0},
new object[]{-7,8,true,true,-7},
new object[]{-7,8,true,true,8},
new object[]{-22,57,false,false,0},
new object[]{-22,57,false,false,-21},
new object[]{-22,57,false,false,56},
new object[]{-22,57,false,true,0},
new object[]{-22,57,false,true,-21},
new object[]{-22,57,false,true,57},
new object[]{-22,57,true,false,0},
new object[]{-22,57,true,false,-22},
new object[]{-22,57,true,false,56},
new object[]{-22,57,true,true,0},
new object[]{-22,57,true,true,-22},
new object[]{-22,57,true,true,57},
new object[]{-44,19,false,false,0},
new object[]{-44,19,false,false,-43},
new object[]{-44,19,false,false,18},
new object[]{-44,19,false,true,0},
new object[]{-44,19,false,true,-43},
new object[]{-44,19,false,true,19},
new object[]{-44,19,true,false,0},
new object[]{-44,19,true,false,-44},
new object[]{-44,19,true,false,18},
new object[]{-44,19,true,true,0},
new object[]{-44,19,true,true,-44},
new object[]{-44,19,true,true,19},
new object[]{-53,80,false,false,0},
new object[]{-53,80,false,false,-52},
new object[]{-53,80,false,false,79},
new object[]{-53,80,false,true,0},
new object[]{-53,80,false,true,-52},
new object[]{-53,80,false,true,80},
new object[]{-53,80,true,false,0},
new object[]{-53,80,true,false,-53},
new object[]{-53,80,true,false,79},
new object[]{-53,80,true,true,0},
new object[]{-53,80,true,true,-53},
new object[]{-53,80,true,true,80},
new object[]{-45,28,false,false,0},
new object[]{-45,28,false,false,-44},
new object[]{-45,28,false,false,27},
new object[]{-45,28,false,true,0},
new object[]{-45,28,false,true,-44},
new object[]{-45,28,false,true,28},
new object[]{-45,28,true,false,0},
new object[]{-45,28,true,false,-45},
new object[]{-45,28,true,false,27},
new object[]{-45,28,true,true,0},
new object[]{-45,28,true,true,-45},
new object[]{-45,28,true,true,28},
new object[]{-70,41,false,false,0},
new object[]{-70,41,false,false,-69},
new object[]{-70,41,false,false,40},
new object[]{-70,41,false,true,0},
new object[]{-70,41,false,true,-69},
new object[]{-70,41,false,true,41},
new object[]{-70,41,true,false,0},
new object[]{-70,41,true,false,-70},
new object[]{-70,41,true,false,40},
new object[]{-70,41,true,true,0},
new object[]{-70,41,true,true,-70},
new object[]{-70,41,true,true,41}
				};
			}
		}
		private static Object[][] NotContainsData
		{
			get
			{
				return new Object[][]
				{
					new object[]{-22,13,false,false,-2147483648},
new object[]{-22,13,false,false,2147483647},
new object[]{-22,13,false,false,-22},
new object[]{-22,13,false,false,13},
new object[]{-22,13,false,true,-2147483648},
new object[]{-22,13,false,true,2147483647},
new object[]{-22,13,false,true,-22},
new object[]{-22,13,false,true,14},
new object[]{-22,13,true,false,-2147483648},
new object[]{-22,13,true,false,2147483647},
new object[]{-22,13,true,false,-23},
new object[]{-22,13,true,false,13},
new object[]{-22,13,true,true,-2147483648},
new object[]{-22,13,true,true,2147483647},
new object[]{-22,13,true,true,-23},
new object[]{-22,13,true,true,14},
new object[]{-41,12,false,false,-2147483648},
new object[]{-41,12,false,false,2147483647},
new object[]{-41,12,false,false,-41},
new object[]{-41,12,false,false,12},
new object[]{-41,12,false,true,-2147483648},
new object[]{-41,12,false,true,2147483647},
new object[]{-41,12,false,true,-41},
new object[]{-41,12,false,true,13},
new object[]{-41,12,true,false,-2147483648},
new object[]{-41,12,true,false,2147483647},
new object[]{-41,12,true,false,-42},
new object[]{-41,12,true,false,12},
new object[]{-41,12,true,true,-2147483648},
new object[]{-41,12,true,true,2147483647},
new object[]{-41,12,true,true,-42},
new object[]{-41,12,true,true,13},
new object[]{-52,95,false,false,-2147483648},
new object[]{-52,95,false,false,2147483647},
new object[]{-52,95,false,false,-52},
new object[]{-52,95,false,false,95},
new object[]{-52,95,false,true,-2147483648},
new object[]{-52,95,false,true,2147483647},
new object[]{-52,95,false,true,-52},
new object[]{-52,95,false,true,96},
new object[]{-52,95,true,false,-2147483648},
new object[]{-52,95,true,false,2147483647},
new object[]{-52,95,true,false,-53},
new object[]{-52,95,true,false,95},
new object[]{-52,95,true,true,-2147483648},
new object[]{-52,95,true,true,2147483647},
new object[]{-52,95,true,true,-53},
new object[]{-52,95,true,true,96},
new object[]{-63,8,false,false,-2147483648},
new object[]{-63,8,false,false,2147483647},
new object[]{-63,8,false,false,-63},
new object[]{-63,8,false,false,8},
new object[]{-63,8,false,true,-2147483648},
new object[]{-63,8,false,true,2147483647},
new object[]{-63,8,false,true,-63},
new object[]{-63,8,false,true,9},
new object[]{-63,8,true,false,-2147483648},
new object[]{-63,8,true,false,2147483647},
new object[]{-63,8,true,false,-64},
new object[]{-63,8,true,false,8},
new object[]{-63,8,true,true,-2147483648},
new object[]{-63,8,true,true,2147483647},
new object[]{-63,8,true,true,-64},
new object[]{-63,8,true,true,9},
new object[]{-41,35,false,false,-2147483648},
new object[]{-41,35,false,false,2147483647},
new object[]{-41,35,false,false,-41},
new object[]{-41,35,false,false,35},
new object[]{-41,35,false,true,-2147483648},
new object[]{-41,35,false,true,2147483647},
new object[]{-41,35,false,true,-41},
new object[]{-41,35,false,true,36},
new object[]{-41,35,true,false,-2147483648},
new object[]{-41,35,true,false,2147483647},
new object[]{-41,35,true,false,-42},
new object[]{-41,35,true,false,35},
new object[]{-41,35,true,true,-2147483648},
new object[]{-41,35,true,true,2147483647},
new object[]{-41,35,true,true,-42},
new object[]{-41,35,true,true,36}
				};
			}
		}

		public void ContainsDataGenerator()
		{
			var results = Enumerable.Range(0, 5)
				.Select(i => (leftBound: Random.Shared.Next(-100, -1), rightBound: Random.Shared.Next(1, 100)))
				.SelectMany(tb => new[]
					{
						(leftClosed:false, rightClosed:false),
						(leftClosed:false, rightClosed:true),
						(leftClosed:true, rightClosed:false),
						(leftClosed:true, rightClosed:true)
					}.Select(tc => (tc.leftClosed, tc.rightClosed, tb.leftBound, tb.rightBound)))
				.SelectMany(t => new[]{
					(t.leftBound, t.rightBound, t.leftClosed, t.rightClosed, value:0),
					(t.leftBound, t.rightBound, t.leftClosed, t.rightClosed, value:t.leftClosed?t.leftBound:(t.leftBound+1)),
					(t.leftBound, t.rightBound, t.leftClosed, t.rightClosed, value:t.rightClosed?t.rightBound:(t.rightBound-1))
				}).Select(t => $@"new object[]{{{t.leftBound},{t.rightBound},{t.leftClosed},{t.rightClosed},{t.value}}}".ToLower());
			var result = String.Join(",\r\n", results);
		}
		public void NotContainsDataGenerator()
		{
			var results = Enumerable.Range(0, 5)
				.Select(i => (leftBound: Random.Shared.Next(-100, -1), rightBound: Random.Shared.Next(1, 100)))
				.SelectMany(tb => new[]
					{
						(leftClosed:false, rightClosed:false),
						(leftClosed:false, rightClosed:true),
						(leftClosed:true, rightClosed:false),
						(leftClosed:true, rightClosed:true)
					}.Select(tc => (tc.leftClosed, tc.rightClosed, tb.leftBound, tb.rightBound)))
				.SelectMany(t => new[]{
					(t.leftBound, t.rightBound, t.leftClosed, t.rightClosed, value:Int32.MinValue),
					(t.leftBound, t.rightBound, t.leftClosed, t.rightClosed, value:Int32.MaxValue),
					(t.leftBound, t.rightBound, t.leftClosed, t.rightClosed, value:t.leftClosed?t.leftBound-1:t.leftBound),
					(t.leftBound, t.rightBound, t.leftClosed, t.rightClosed, value:t.rightClosed?t.rightBound+1:t.rightBound)
				}).Select(t => $@"new object[]{{{t.leftBound},{t.rightBound},{t.leftClosed},{t.rightClosed},{t.value}}}".ToLower());
			var result = String.Join(",\r\n", results);
		}

		[TestMethod]
		[DynamicData(nameof(ContainsData))]
		public void Contains(Int32 leftBound, Int32 rightBound, Boolean leftClosed, Boolean rightClosed, Int32 value)
		{
			var interval = BoundedInterval<Int32>.Create(leftBound, rightBound, leftClosed, rightClosed);
			var actual = interval.Contains(value);

			Assert.IsTrue(actual);
		}
		[TestMethod]
		[DynamicData(nameof(NotContainsData))]
		public void NotContains(Int32 leftBound, Int32 rightBound, Boolean leftClosed, Boolean rightClosed, Int32 value)
		{
			var interval = BoundedInterval<Int32>.Create(leftBound, rightBound, leftClosed, rightClosed);
			var actual = interval.Contains(value);

			Assert.IsFalse(actual);
		}
	}
}
