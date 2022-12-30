using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhoMicro.Common.System.IO;
using System.Net.Http.Headers;

namespace System.Tests.IO
{
	[TestClass]
	public class StreamReadQueueTests
	{
		private static Object[][] Data
		{
			get
			{
				return new Object[][]
{
	new Object[]
	{
		new MemoryStream(new Byte[]{182,76,119,138,139,110,27,238}),
		new MemoryStream(new Byte[]{23,72,79,83,150,253,92,85}),
		new MemoryStream(new Byte[]{246,149,70,101,81,120,117,8}),
		new MemoryStream(new Byte[]{110,157,227,138,46,130,193,251}),
		new MemoryStream(new Byte[]{225,188,206,31,154,54,252,230})
	},
new Object[]
	{
		new MemoryStream(new Byte[]{230,13,106,155,23,141,248,65}),
		new MemoryStream(new Byte[]{46,81,210,230,57,126,58,160}),
		new MemoryStream(new Byte[]{50,224,151,47,253,227,62,69}),
		new MemoryStream(new Byte[]{117,140,253,130,127,44,118,143}),
		new MemoryStream(new Byte[]{96,12,19,35,169,179,219,213})
	},
new Object[]
	{
		new MemoryStream(new Byte[]{237,54,240,41,85,171,233,234}),
		new MemoryStream(new Byte[]{29,236,86,185,58,201,52,142}),
		new MemoryStream(new Byte[]{43,57,204,30,27,90,189,129}),
		new MemoryStream(new Byte[]{102,44,196,36,201,116,22,187}),
		new MemoryStream(new Byte[]{83,150,22,183,11,91,125,47})
	},
new Object[]
	{
		new MemoryStream(new Byte[]{168,52,18,77,169,3,166,24}),
		new MemoryStream(new Byte[]{85,23,159,149,59,55,200,174}),
		new MemoryStream(new Byte[]{15,23,217,120,66,214,218,17}),
		new MemoryStream(new Byte[]{0,184,182,124,50,237,161,167}),
		new MemoryStream(new Byte[]{50,218,95,36,243,129,132,87})
	}
};
			}
		}

		//[TestMethod]
		public void DataGenerator()
		{
			var result =
$@"new Object[][]
{{
	{String.Join(",\r\n", Enumerable.Range(0, 4).Select(i =>
   $@"new Object[]
	{{
{String.Join(",\r\n", Enumerable.Range(0, 5)
		.Select(i => { var bytes = new Byte[8]; Random.Shared.NextBytes(bytes); return bytes; })
		.Select(b =>
$"		new MemoryStream(new Byte[]{{{String.Join(',', b)}}})"))}
	}}"))}
}};";
		}

		[TestMethod]
		[DynamicData(nameof(Data))]
		public void Append(MemoryStream a, MemoryStream b, MemoryStream c, MemoryStream d, MemoryStream e)
		{
			using var actualStream = a.Append(b);
			var actualBytes = new Byte[actualStream.Length];
			actualStream.Read(actualBytes, 0, actualBytes.Length);

			a.Seek(0, SeekOrigin.Begin);
			b.Seek(0, SeekOrigin.Begin);
			var expectedBytes = new Byte[a.Length + b.Length];
			var aRead = a.Read(expectedBytes, 0, (Int32)a.Length);
			b.Read(expectedBytes, aRead, (Int32)b.Length);

			var actualString = String.Concat(actualBytes);
			var expectedString = String.Concat(expectedBytes);

			Assert.AreEqual(expectedString, actualString);
		}

		[TestMethod]
		[DynamicData(nameof(Data))]
		public void AppendRange(MemoryStream a, MemoryStream b, MemoryStream c, MemoryStream d, MemoryStream e)
		{
			using var actualStream = a.AppendRange(false, b, c, d, e);
			var actualBytes = new Byte[actualStream.Length];
			actualStream.Read(actualBytes, 0, actualBytes.Length);

			var expectedBytes = new Byte[a.Length + b.Length + c.Length + d.Length + e.Length];
			a.Seek(0, SeekOrigin.Begin);
			var offset = 0;
			offset += a.Read(expectedBytes, offset, (Int32)a.Length);
			b.Seek(0, SeekOrigin.Begin);
			offset += b.Read(expectedBytes, offset, (Int32)b.Length);
			c.Seek(0, SeekOrigin.Begin);
			offset += c.Read(expectedBytes, offset, (Int32)c.Length);
			d.Seek(0, SeekOrigin.Begin);
			offset += d.Read(expectedBytes, offset, (Int32)d.Length);
			e.Seek(0, SeekOrigin.Begin);
			e.Read(expectedBytes, offset, (Int32)e.Length);

			var actualString = String.Concat(actualBytes);
			var expectedString = String.Concat(expectedBytes);

			Assert.AreEqual(expectedString, actualString);
		}
	}
}
