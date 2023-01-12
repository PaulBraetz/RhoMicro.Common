using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhoMicro.Common.System.IO;
using RhoMicro.Common.System.Collections;

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
		private static Object[][] AsEnumerableData
		{
			get
			{
				return new Object[][]
				{
					new Object[]
					{
						Encoding.UTF8.GetBytes("This is some good data."),
						3,true

					},
					new Object[]
					{
						Encoding.UTF8.GetBytes("This is not."),
						2,true
					},
					new Object[]
					{
						Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."),
						12,true
					},
					new Object[]
					{
						Encoding.UTF8.GetBytes("This is some good data."),
						3,false

					},
					new Object[]
					{
						Encoding.UTF8.GetBytes("This is not."),
						2,false
					},
					new Object[]
					{
						Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."),
						12,false
					}
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(FullLineData))]
		public void ReadFullLine(String text, String[] expectedLines)
		{
			using var reader = new StringReader(text);
			for (var i = 0; i < expectedLines.Length; i++)
			{
				var expectedLine = expectedLines[i];
				var actual = reader.ReadFullLine();
				Assert.AreEqual(expectedLine, actual);
			}
		}

		[TestMethod]
		[DynamicData(nameof(AsEnumerableData))]
		public void AsEnumerable(Byte[] bytes, Int32 bufferSize, Boolean yieldIncomplete)
		{
			var expecteds = bytes
				.Select((b, i) => (g: i / bufferSize, b))
				.GroupBy(t => t.g)
				.Select(g => g.Select(t => t.b).ToArray())
				.Where(b => yieldIncomplete || b.Length == bufferSize)
				.ToArray();

			var token = new CancellationTokenSource(100).Token;
			var actuals = new MemoryStream(bytes).AsEnumerable(bufferSize, yieldIncomplete, token).ToArray();

			Assert.AreEqual(actuals.Length, expecteds.Length);

			for (var i = 0; i < expecteds.Length; i++)
			{
				var expected = expecteds[i];
				var actual = actuals[i];

				Assert.AreEqual(expected.Length, actual.Length);

				for (var j = 0; j < expected.Length; j++)
				{
					Assert.AreEqual(expected[j], actual[j]);
				}
			}
		}
		[TestMethod]
		[DynamicData(nameof(AsEnumerableData))]
		public async Task AsAsyncEnumerable(Byte[] bytes, Int32 bufferSize, Boolean yieldIncomplete)
		{
			var expecteds = bytes
				.Select((b, i) => (g: i / bufferSize, b))
				.GroupBy(t => t.g)
				.Select(g => g.Select(t => t.b).ToArray())
				.Where(b => yieldIncomplete || b.Length == bufferSize)
				.ToArray();

			var token = new CancellationTokenSource(100).Token;
			var actuals = await new MemoryStream(bytes).AsAsyncEnumerable(bufferSize, yieldIncomplete, token).ToList();

			Assert.AreEqual(actuals.Count, expecteds.Length);

			for (var i = 0; i < expecteds.Length; i++)
			{
				var expected = expecteds[i];
				var actual = actuals[i];

				Assert.AreEqual(expected.Length, actual.Length);

				for (var j = 0; j < expected.Length; j++)
				{
					Assert.AreEqual(expected[j], actual[j]);
				}
			}
		}
	}
}
