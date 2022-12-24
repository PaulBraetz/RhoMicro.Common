using RhoMicro.Common.Math.Statistics;

namespace Math.Statistics.Tests
{
	[TestClass]
	public class EmpiricalStatisticTests
	{
		private static Object[][] Samples
		{
			get
			{
				return new Object[][]
				{
					new Object[]
					{
						new Double[]
						{
							4.9,4.8,5.0,5.2,5.2,5.1,4.7,5.0,5.0,4.9,4.8,4.9,5.1,5.0,5.0,5.1,5.0,4.9,4.8,4.9,4.9,5.0,5.0,5.1,5.0
						}
					}
				};
			}
		}
		[TestMethod]
		[DynamicData(nameof(Samples))]
		public void Create(Double[] samples)
		{
			var statistic = Statistic.Create<Double>(samples, division1, division2);

			Double division1(Double d, Int32 i)
			{
				return d / d;
			}
			Double division2(Int32 i, Double d)
			{
				return i / d;
			}
		}
	}
}