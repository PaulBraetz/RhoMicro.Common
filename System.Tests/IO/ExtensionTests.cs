using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhoMicro.Common.System.IO;

namespace System.Tests.IO
{
	[TestClass]
	public class ExtensionTests
	{
		private static Object[][] FullLineData
		{
			get
			{
				return new Object[][]
				{
					new object[]
					{
						"Some\nLines\rAre not \r\n created\n equal.",
						new String[]{"Some\n", "Lines\rAre not \r\n", " created\n", " equal."}
					},
					new object[]
					{
						"\rSome\nLines\rAre not \r\n created\n equal.",
						new String[]{"\rSome\n", "Lines\rAre not \r\n", " created\n", " equal."}
					},
					new object[]
					{
						"\nSome\nLines\rAre not \r\n created\n equal.",
						new String[]{"\n","Some\n", "Lines\rAre not \r\n", " created\n", " equal."}
					},
					new object[]
					{
						"\nSome\nLines\rAre no\n\nt \r\n created\n equal.",
						new String[]{"\n","Some\n", "Lines\rAre no\n","\n","t \r\n", " created\n", " equal."}
					}
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(FullLineData))]
		public void ReadFullLine(String text, String[] expectedLines)
		{
			using var reader = new StringReader(text);
			for(var i = 0; i < expectedLines.Length; i++)
			{
				var expectedLine = expectedLines[i];
				var actual = reader.ReadFullLine();
				Assert.AreEqual(expectedLine, actual);
			}
		}
	}
}
