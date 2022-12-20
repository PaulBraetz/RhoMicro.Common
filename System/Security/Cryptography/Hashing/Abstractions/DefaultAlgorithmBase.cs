using Fort;
using System;
using System.Security.Cryptography;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions
{
	/// <summary>
	/// Base class for types implementing <see cref="IAlgorithm{T}"/> based on built-in algorithms.
	/// </summary>
	/// <typeparam name="T">
	/// The type of object this algorithm is able to calculate hash values for.
	/// </typeparam>
	public abstract class DefaultAlgorithmBase<T> : DisposableBase, IAlgorithm<T>
	{
		/// <summary>
		/// The built-in algorithms available
		/// </summary>
		public enum BuiltinAlgorithm
		{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
			sha256,
			sha384,
			sha512,
			md5,
			sha1
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		}
		/// <summary>
		/// Initializes a new instance with the algorithm provided.
		/// </summary>
		/// <param name="algorithm">The algorithm to use for hashing.</param>
		public DefaultAlgorithmBase(BuiltinAlgorithm algorithm)
		{
			_algorithm = algorithm;
		}

		private readonly BuiltinAlgorithm _algorithm;

		/// <summary>
		/// Creates a strategy-based algorithm based on a built-in hashing algorithm.
		/// </summary>
		/// <param name="convertStrategy">The strategy used to convert an instance of <typeparamref name="T"/> into a byte array.</param>
		/// <param name="algorithm">The algorithm used to hash the bytes produced by <paramref name="convertStrategy"/>.</param>
		/// <returns>An instance of <see cref="IAlgorithm{T}"/>, based on <paramref name="convertStrategy"/> and <paramref name="algorithm"/>.</returns>
		public static IAlgorithm<T> Create(Func<T, Byte[]> convertStrategy, BuiltinAlgorithm algorithm)
		{
			var result = new DefaultAlgorithmStrategy<T>(convertStrategy, algorithm);

			return result;
		}
		/// <summary>
		/// Creates a strategy-based algorithm.
		/// </summary>
		/// <param name="hashingStrategy">The strategy used to hash instances of <typeparamref name="T"/>.</param>
		/// <returns>A hashing algorithm, based on <paramref name="hashingStrategy"/>.</returns>
		public static IAlgorithm<T> Create(Func<T, Byte[]> hashingStrategy)
		{
			var result = new AlgorithmStrategy<T>(hashingStrategy);

			return result;
		}

		/// <summary>
		/// Converts plain data to an array of bytes; ready for hashing.
		/// </summary>
		/// <param name="plainData">The data to convert.</param>
		/// <returns>An array of bytes representing <paramref name="plainData"/>.</returns>
		protected abstract Byte[] Convert(T plainData);

		/// <inheritdoc/>
		public IHash<T> Hash(T plainData)
		{
			plainData.ThrowIfDefault(nameof(plainData));

			Byte[] bytes = Convert(plainData);
			Byte[] hashBytes = null;
			using (var algorithm = HashAlgorithm.Create(_algorithm.ToString()))
			{
				hashBytes = algorithm.ComputeHash(bytes);
			}

			var result = new Hash<T>(hashBytes, this);

			return result;
		}
	}
}
