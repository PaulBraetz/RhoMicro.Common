using Fort;

namespace RhoMicro.Common.Math.Tests
{
	[TestClass]
	public class UnitTests
	{
		[TestMethod]
		public void NullNameConstructor()
		{
			_ = Assert.ThrowsException<ArgumentNullException>(() => new Unit(null));
		}

		[TestMethod]
		public void EmptyNameConstructor()
		{
			_ = Assert.ThrowsException<ArgumentException>(() => new Unit(String.Empty));
		}

		private static Object[][] Names => TestData.Names;
		[TestMethod]
		[DynamicData(nameof(Names))]
		public void NameConstructor(String name)
		{
			var unit = new Unit(name);

			Assert.AreEqual(name, unit.Name);
		}

		private static Object[][] EqualNames => TestData.EqualNames;
		[TestMethod]
		[DynamicData(nameof(EqualNames))]
		public void ObjectEquals(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s == nameB);

			var unitA = new Unit(nameA);
			Object unitB = new Unit(nameB);

			Boolean actual = unitA.Equals(unitB);

			Assert.IsTrue(actual);
		}
		[TestMethod]
		[DynamicData(nameof(EqualNames))]
		public void UnitEquals(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s == nameB);

			var unitA = new Unit(nameA);
			var unitB = new Unit(nameB);

			Boolean actual = unitA.Equals(unitB);

			Assert.IsTrue(actual);
		}
		[TestMethod]
		[DynamicData(nameof(EqualNames))]
		public void OperatorEquals1(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s == nameB);

			var unitA = new Unit(nameA);
			var unitB = new Unit(nameB);

			Boolean actual = unitA == unitB;

			Assert.IsTrue(actual);
		}
		[TestMethod]
		[DynamicData(nameof(EqualNames))]
		public void OperatorEquals2(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s == nameB);

			var unitA = new Unit(nameA);
			var unitB = new Unit(nameB);

			Boolean actual = unitA != unitB;

			Assert.IsFalse(actual);
		}

		private static Object[][] NotEqualNames => TestData.NotEqualNames;
		[TestMethod]
		[DynamicData(nameof(NotEqualNames))]
		public void ObjectNotEquals(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s != nameB);

			var unitA = new Unit(nameA);
			Object unitB = new Unit(nameB);

			Boolean actual = unitA.Equals(unitB);

			Assert.IsFalse(actual);
		}
		[TestMethod]
		[DynamicData(nameof(NotEqualNames))]
		public void UnitNotEquals(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s != nameB);

			var unitA = new Unit(nameA);
			var unitB = new Unit(nameB);

			Boolean actual = unitA.Equals(unitB);

			Assert.IsFalse(actual);
		}
		[TestMethod]
		[DynamicData(nameof(NotEqualNames))]
		public void OperatorNotEquals1(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s != nameB);

			var unitA = new Unit(nameA);
			var unitB = new Unit(nameB);

			Boolean actual = unitA == unitB;

			Assert.IsFalse(actual);
		}
		[TestMethod]
		[DynamicData(nameof(NotEqualNames))]
		public void OperatorNotEquals2(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s != nameB);

			var unitA = new Unit(nameA);
			var unitB = new Unit(nameB);

			Boolean actual = unitA != unitB;

			Assert.IsTrue(actual);
		}

		[TestMethod]
		[DynamicData(nameof(EqualNames))]
		public void InternedName(String nameA, String nameB)
		{
			nameA.ThrowIfNot(s => s == nameB);

			String newNameA = String.Concat(nameA.ToCharArray());
			String newNameB = String.Concat(nameA.ToCharArray());

			Boolean actualEquality = Object.ReferenceEquals(nameA, nameB);
			Assert.IsFalse(actualEquality);

			var unitA = new Unit(newNameA);
			var unitB = new Unit(newNameB);

			actualEquality = Object.ReferenceEquals(unitA.Name, unitB.Name);
			Assert.IsTrue(actualEquality);
		}
	}
}