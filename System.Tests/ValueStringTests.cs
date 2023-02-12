using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhoMicro.Common.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Tests
{
	[TestClass]
	public class ValueStringTests
	{
		private static Object?[][] Values
		{
			get
			{
				return new Object?[][]
				{
					new Object[] {"", String.Empty},
					new Object?[] {null, String.Empty},
					new Object[] {String.Empty, String.Empty}, //redundant
					new Object[] {"Actually has a value now.", "Actually has a value now."},
					new Object[] { "Actually has another value now.", "Actually has another value now."}
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(Values))]
		public void StringEqual(string value, string expected)
		{
			ValueString instance = value;
			Assert.AreEqual(expected, (string)instance);
		}
		[TestMethod]
		[DynamicData(nameof(Values))]
		public void ObjectEqual(string value, object expected)
		{
			ValueString instance = value;
			Assert.IsTrue(instance.Equals(expected));
			Assert.IsTrue(instance.Equals((Object)instance));
		}
		[TestMethod]
		[DynamicData(nameof(Values))]
		public void ValueStringEqual(string value, string expected)
		{
			ValueString instance = value;
			Assert.AreEqual((ValueString)expected, instance);
		}
	}
}
