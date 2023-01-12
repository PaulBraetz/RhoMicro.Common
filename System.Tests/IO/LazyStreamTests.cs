using Microsoft.VisualStudio.TestTools.UnitTesting;
using RhoMicro.Common.System.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Tests.IO
{
	[TestClass]
	public class LazyStreamTests
	{
		private static Object[][] Data
		{
			get
			{
				return new Object[][]
				{
					new Object[]
					{
						"Here is a stream value."
					}
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(Data))]
		public void Initialization(String streamValue)
		{
			var bytes = Encoding.UTF8.GetBytes(streamValue);
			var stream = new MemoryStream(bytes);
			var initialized = false;
			var lazyStream = new LazyStream<MemoryStream>(() =>
			{
				initialized = true;
				return stream;
			});
			Assert.IsFalse(initialized);
			Assert.IsFalse(lazyStream.IsStreamCreated);
			_ = lazyStream.BaseStream;
			Assert.IsTrue(initialized);
			Assert.IsTrue(lazyStream.IsStreamCreated);
		}
		[TestMethod]
		[DynamicData(nameof(Data))]
		public void Dispose(String streamValue)
		{
			var lazyStream = GetLazyStream(streamValue);

			lazyStream.Dispose();

			Assert.ThrowsException<ObjectDisposedException>(() => lazyStream.BaseStream);
		}
		[TestMethod]
		[DynamicData(nameof(Data))]
		public void DisposeAfterInitialization(String streamValue)
		{
			var lazyStream = GetLazyStream(streamValue);

			_ = lazyStream.BaseStream;
			lazyStream.Dispose();

			Assert.ThrowsException<ObjectDisposedException>(() => lazyStream.BaseStream);
		}
		[TestMethod]
		[DynamicData(nameof(Data))]
		public void PipeRead(String streamValue)
		{
			var stream = GetStream(streamValue);

			var reader = new StreamReader(stream);
			var actual = reader.ReadToEnd();

			Assert.AreEqual(streamValue, actual);
		}

		[TestMethod]
		[DynamicData(nameof(Data))]
		public void Reset(String streamValue)
		{
			var lazyStream = GetLazyStream(streamValue);

			var firstStream = lazyStream.BaseStream;
			lazyStream.Reset();
			var secondStream = lazyStream.BaseStream;

			Assert.AreNotEqual(firstStream, secondStream);
		}
		[TestMethod]
		[DynamicData(nameof(Data))]
		public void ResetNotCreated(String streamValue)
		{
			var lazyStream = GetLazyStream(streamValue);

			_ = lazyStream.BaseStream;
			lazyStream.Reset();
			Assert.IsFalse(lazyStream.IsStreamCreated);
		}

		private static Stream GetStream(String streamValue)
		{
			var bytes = Encoding.UTF8.GetBytes(streamValue);
			var stream = new MemoryStream(bytes);

			return stream;
		}

		private static LazyStream<Stream> GetLazyStream(String streamValue)
		{
			var stream = GetStream(streamValue);
			var lazyStream = new LazyStream<Stream>(() => GetStream(streamValue));

			return lazyStream;
		}

	}
}
