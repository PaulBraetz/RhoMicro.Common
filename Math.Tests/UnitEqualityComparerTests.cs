using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhoMicro.Common.Math.Abstractions;
using RhoMicro.Common.Math.Comparers;

namespace RhoMicro.Common.Math.Tests
{
	[TestClass]
	public class UnitEqualityComparerTests
	{
		private static Object[][] EqualUnits
		{
			get
			{
				return TestData.EqualUnits;
			}
		}
		[TestMethod]
		[DynamicData(nameof(EqualUnits))]
		public void Equals(IUnit unitA, IUnit unitB)
		{
			var actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsTrue(actual);
		}
		[TestMethod]
		public void NullEquals()
		{
			IUnit unitA = null;
			IUnit unitB = null;

			var actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsTrue(actual);
		}


		private static Object[][] NotEqualUnits { get { return TestData.NotEqualUnits; } }
		[TestMethod]
		[DynamicData(nameof(NotEqualUnits))]
		public void NotEquals(IUnit unitA, IUnit unitB)
		{
			var actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsFalse(actual);
		}

		private static Object[][] Units { get { return TestData.Units; } }
		[TestMethod]
		[DynamicData(nameof(Units))]
		public void NullNotEquals1(IUnit unitA)
		{
			IUnit unitB = null;

			var actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsFalse(actual);
		}
		[TestMethod]
		[DynamicData(nameof(Units))]
		public void NullNotEquals2(IUnit unitB)
		{
			IUnit unitA = null;

			var actual = UnitEqualityComparer.Instance.Equals(unitA, unitB);

			Assert.IsFalse(actual);
		}

		[TestMethod]
		[DynamicData(nameof(EqualUnits))]
		public void GetHashCode(IUnit unitA, IUnit unitB)
		{
			var expected = UnitEqualityComparer.Instance.GetHashCode(unitA);
			var actual = UnitEqualityComparer.Instance.GetHashCode(unitB); ;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NullGetHashCode()
		{
			Assert.ThrowsException<ArgumentNullException>(() => UnitEqualityComparer.Instance.GetHashCode(null));
		}
	}
}
