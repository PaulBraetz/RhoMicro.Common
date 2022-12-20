using RhoMicro.Common.Math.Abstractions;
using RhoMicro.Common.Math.Comparers;

namespace RhoMicro.Common.Math.Tests
{
	[TestClass]
	public class UnitEqualityComparerTests
	{
		private static Object[][] EqualUnits => TestData.EqualUnits;
		[TestMethod]
		[DynamicData(nameof(EqualUnits))]
		public void Equals(IUnit unitA, IUnit unitB)
		{
			Boolean actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsTrue(actual);
		}
		[TestMethod]
		public void NullEquals()
		{
			IUnit unitA = null;
			IUnit unitB = null;

			Boolean actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsTrue(actual);
		}

		private static Object[][] NotEqualUnits => TestData.NotEqualUnits;
		[TestMethod]
		[DynamicData(nameof(NotEqualUnits))]
		public void NotEquals(IUnit unitA, IUnit unitB)
		{
			Boolean actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsFalse(actual);
		}

		private static Object[][] Units => TestData.Units;
		[TestMethod]
		[DynamicData(nameof(Units))]
		public void NullNotEquals1(IUnit unitA)
		{
			IUnit unitB = null;

			Boolean actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsFalse(actual);
		}
		[TestMethod]
		[DynamicData(nameof(Units))]
		public void NullNotEquals2(IUnit unitB)
		{
			IUnit unitA = null;

			Boolean actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsFalse(actual);
		}

		[TestMethod]
		[DynamicData(nameof(EqualUnits))]
		public void GetHashCode(IUnit unitA, IUnit unitB)
		{
			Int32 expected = UnitEqualityComparer.Instance.GetHashCode(unitA);
			Int32 actual = UnitEqualityComparer.Instance.GetHashCode(unitB); ;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NullGetHashCode()
		{
			_ = Assert.ThrowsException<ArgumentNullException>(() => UnitEqualityComparer.Instance.GetHashCode(null));
		}
	}
}
