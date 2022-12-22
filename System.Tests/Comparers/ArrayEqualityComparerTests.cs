using RhoMicro.Common.System.Comparers;

namespace System.Tests.Comparers
{
	[TestClass]
	public class ArrayEqualityComparerTests
	{
		private static Object[][] EqualArrays
		{
			get
			{
				return new Object[][][]
				{
					new Object[][]
					{
						new Object[]{"0","1", "2", "3", "4"}
					},
					new Object[][]
					{
						new Object[]{"0","1", "2", "3", null}
					},
					new Object[][]
					{
						Enumerable.Range(0,10).Select(i=>i*5).OfType<Object>().ToArray()
					},
					new Object[][]
					{
						Array.Empty<Object>()
					},
					new Object[][]
					{
						(Object[])null
					}
				}.Select(args => args.SelectMany(arg => new Object[] { arg, arg?.ToArray() }).ToArray()).ToArray();
			}
		}
		[TestMethod]
		[DynamicData(nameof(EqualArrays))]
		public void Equals(Object[] arrayA, Object[] arrayB)
		{
			var actual = ArrayEqualityComparer<Object>.Instance.Equals(arrayA, arrayB);

			Assert.IsTrue(actual);
		}

		private static Object[][] NotEqualArrays
		{
			get
			{
				return new Object[][]
				{
					new Object[]
					{
						new []{"0","1", "2", "3", "4"},
						new []{"0", "1", "2", "3"}
					},
					new Object[]
					{
						new []{"0","1", "2", "3", "4"},
						new []{"0", "1", "2", "3", null}
					},
					new Object[]
					{
						new []{"0","1", "2", "3", null},
						new []{"0", "1", "2", "3", "4"}
					},
					new Object[]
					{
						new []{"0","1", null, "3", "4"},
						new []{"0", "1", "2", "3", "4"}
					},
					new Object[]
					{
						new []{"0","1", "2", "3", "4"},
						new []{"0", "1", null, "3", "4"}
					},
					new Object[]
					{
						new []{null,"1", "2", "3", "4"},
						new []{"0", "1", "2", "3", "4"}
					},
					new Object[]
					{
						new []{"0","1", "2", "3", "4"},
						new []{null, "1", "2", "3", "4"}
					},
					new Object[]
					{
						Enumerable.Range(0,10).Select(i=>i*5).OfType<Object>().ToArray(),
						Enumerable.Range(0,10).Select(i=>i*10).OfType<Object>().ToArray()
					},
					new Object[]
					{
						Array.Empty<Object>(),
						new []{"0","1", "2", "3", "4"}

					},
					new Object[]
					{
						new []{"0","1", "2", "3", "4"},
						Array.Empty<Object>()

					},
					new Object[]
					{
						Array.Empty<Object>(),
						null

					},
					new Object[]
					{
						new []{"0","1", "2", "3", "4"},
						null

					},
					new Object[]
					{
						Array.Empty<Object>(),
						null

					},
					new Object[]
					{
						new []{"0","1", "2", "3", "4"},
						null
					}
				};
			}
		}
		[TestMethod]
		[DynamicData(nameof(NotEqualArrays))]
		public void NotEquals(Object[] arrayA, Object[] arrayB)
		{
			var actual = ArrayEqualityComparer<Object>.Instance.Equals(arrayA, arrayB);

			Assert.IsFalse(actual);
		}
	}
}
