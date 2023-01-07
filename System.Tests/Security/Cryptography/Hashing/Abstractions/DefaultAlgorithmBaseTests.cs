using RhoMicro.Common.System.Comparers;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System.Text;

namespace System.Tests.Security.Cryptography.Hashing.Abstractions
{
	[TestClass]
	public class DefaultAlgorithmBaseTests
	{
		private static Object[][] HashData
		{
			get
			{
				return new String[]
				{
					"Value 1",
					String.Empty,
					"Some Other Value",
					"C7FxkdxUql4tzYb93WaEa7mxbxNhtGI0LhjyIIblgpo"
				}.SelectMany(
					b => Enum.GetValues<DefaultAlgorithmBase<String>.BuiltinAlgorithm>()
						.OfType<DefaultAlgorithmBase<String>.BuiltinAlgorithm>()
						.Select(e => new Object[] { b, e, Hash(b, e.ToString()) }))
				.ToArray();
			}
		}
		[TestMethod]
		[DynamicData(nameof(HashData))]
		public void HashSerialize(String plainData, DefaultAlgorithmBase<String>.BuiltinAlgorithm algorithm, Byte[] expected)
		{
			var algorithmInstance = DefaultAlgorithmBase<String>.Create(Serialize, algorithm);
			var result = algorithmInstance.Hash(plainData);

			var actual = ArrayEqualityComparer<Byte>.Instance.Equals(result.Value, expected);
			Assert.IsTrue(actual);

			var recalculated = result.Algorithm.Hash(plainData);
			actual = ArrayEqualityComparer<Byte>.Instance.Equals(result.Value, recalculated.Value);
			Assert.IsTrue(actual);

			Assert.AreEqual(result, recalculated);
		}

		private static Stream Serialize(String plainData)
		{
			var bytes = Convert(plainData);
			var result = new MemoryStream(bytes, false);

			return result;
		}
		private static Byte[] Convert(String plainData)
		{
			try
			{
				//var result = System.Convert.FromBase64String(plainData);
				var result = Encoding.UTF8.GetBytes(plainData);

				return result;
			}
			catch (Exception es)
			{
				throw;
			}
		}
		private static Byte[] Hash(String plainData, String algorithm)
		{
			var bytes = Convert(plainData);
			using var transform = System.Security.Cryptography.HashAlgorithm.Create(algorithm) ?? throw new NotSupportedException($"Unknown algorithm: {algorithm}");
			var result = transform.ComputeHash(bytes);

			return result;
		}
	}
}
