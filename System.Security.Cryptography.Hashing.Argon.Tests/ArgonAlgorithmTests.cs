using Konscious.Security.Cryptography;
using RhoMicro.Common.System.Comparers;
using RhoMicro.Common.System.Security.Cryptography.Hashing;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System.Text;

namespace System.Security.Cryptography.Hashing.Argon.Tests
{
	[TestClass]
	public class ArgonAlgorithmTests
	{
		private const Int32 LIMIT = 2;

		private static Object[][] Data
		{
			get
			{
				var iterations = Enumerable.Range(1, LIMIT);
				var degrees = Enumerable.Range(9 - LIMIT, LIMIT);
				var memorySizes = Enumerable.Range(1, LIMIT).Select(i => i * (4096 / LIMIT));
				var resultSizes = Enumerable.Range(1, LIMIT).Select(i => i * (1024 / LIMIT));
				var versions = Enum.GetValues<ArgonVersion>().OfType<ArgonVersion>();
				var salts = Enumerable.Range(1, LIMIT).Select(i => i * (65536 / LIMIT)).Select(i => i.ToString()).Select(Encoding.Unicode.GetBytes);
				var settings = iterations
					.SelectMany(i => degrees.Select(d => (i, d)))
					.SelectMany(t => memorySizes.Select(m => (m, t.i, t.d)))
					.SelectMany(t => resultSizes.Select(r => (r, t.m, t.i, t.d)))
					.SelectMany(t => versions.Select(v => (v, t.r, t.m, t.i, t.d)))
					.SelectMany(t => salts.Select(s => (s, t.v, t.r, t.m, t.i, t.d)))
					.Select(t => new ArgonSettings()
					{
						Iterations = t.i,
						DegreeOfParallelism = t.d,
						MemorySize = t.m,
						ResultSize = t.r,
						Version = t.v,
						Salt = t.s
					});

				var bytes = new[]
				{
					"Data 1",
					"Some more random data",
					Decimal.MaxValue.ToString()
				}.Select(Encoding.Unicode.GetBytes);
				var result = bytes.SelectMany(b => settings.Select(s => (b, s)))
				.Select(t => (t.b, t.s, h: Hash(t.b, t.s)))
				.Select(t => new Object[] { t.b, t.s, t.h })
				.ToArray();

				return result;
			}
		}

		[TestMethod]
		[DynamicData(nameof(Data))]
		public void MyTestMethod(Byte[] plainData, IArgonSettings settings, Byte[] expected)
		{
			var algorithm = ArgonAlgorithm<Byte[]>.Create(b => b, settings);
			var hashValue = algorithm.Hash(plainData).Value;

			var actual = ArrayEqualityComparer<Byte>.Instance.Equals(hashValue, expected);

			Assert.IsTrue(actual);
		}

		private static Byte[] Hash(Byte[] plainData, IArgonSettings settings)
		{
			switch (settings.Version)
			{
				case ArgonVersion.Argon2d:
					return new Argon2d(plainData)
					{
						AssociatedData = settings.AssociatedData,
						DegreeOfParallelism = settings.DegreeOfParallelism,
						Iterations = settings.Iterations,
						KnownSecret = settings.KnownSecret,
						Salt = settings.Salt,
						MemorySize = settings.MemorySize
					}.GetBytes(settings.ResultSize);
				case ArgonVersion.Argon2i:
					return new Argon2i(plainData)
					{
						AssociatedData = settings.AssociatedData,
						DegreeOfParallelism = settings.DegreeOfParallelism,
						Iterations = settings.Iterations,
						KnownSecret = settings.KnownSecret,
						Salt = settings.Salt,
						MemorySize = settings.MemorySize
					}.GetBytes(settings.ResultSize);
				default:
					return new Argon2id(plainData)
					{
						AssociatedData = settings.AssociatedData,
						DegreeOfParallelism = settings.DegreeOfParallelism,
						Iterations = settings.Iterations,
						KnownSecret = settings.KnownSecret,
						Salt = settings.Salt,
						MemorySize = settings.MemorySize
					}.GetBytes(settings.ResultSize);
			}
		}
	}
}
