using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhoMicro.Common.System.Collections;

namespace System.Tests.Collections
{
	[TestClass]
	public class SearchTests
	{
		private static Object[][] FuzzyInts
		{
			get
			{
				return new Object[][]
				{
					new Object[]
					{
						new Int32[]{1,0,3,-2,6}, 5, false, 6
					},
					new Object[]
					{
						new Int32[]{1,-44,30,-12,26}, -29, false, -44
					},
					new Object[]
					{
						new Int32[]{-3451,35,683,3583,-283568,36}, 2354, false, 3583
					},
					new Object[]
					{
						new Int32[]{571,-6840,243,-245267,673}, 6345, false, 673
					},
					new Object[]
					{
						new Int32[]{-2,0,1,3,6}, 5, true, 6
					},
					new Object[]
					{
						new Int32[]{-44,-12,-1,26,30}, -29, true, -44
					},
					new Object[]
					{
						new Int32[]{-283568, -3451,35,36,683,3583}, -2354, true, -3451
					},
					new Object[]
					{
						new Int32[]{ -245267, -6840, 243, 571, 673}, 300, true, 243
					}
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(FuzzyInts))]
		public void BinaryFuzzy(Int32[] values, Int32 query, Boolean sorted, Int32 expected)
		{
			var actual = values.BinaryFuzzySearch(v => v - query, sorted);
			Assert.AreEqual(expected, actual);
		}
	}
}
