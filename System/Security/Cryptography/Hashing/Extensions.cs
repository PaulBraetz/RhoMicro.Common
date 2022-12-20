using Fort;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	/// <summary>
	/// Provides extension methods for hashing.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Decorates an instance of <typeparamref name="T"/> with its hash value.
		/// </summary>
		/// <remarks>
		/// The hash value is calculated using <paramref name="algorithm"/>.
		/// </remarks>
		/// <typeparam name="T">The type of data to decorate.</typeparam>
		/// <param name="plainData">The instance to decorate.</param>
		/// <param name="algorithm">The algorithm to be used for hashing.</param>
		/// <returns><paramref name="plainData"/>, decorated with its hash value.</returns>
		public static IHashed<T> WithHash<T>(this T plainData, IAlgorithm<T> algorithm)
		{
			plainData.ThrowIfDefault(nameof(plainData));
			algorithm.ThrowIfDefault(nameof(algorithm));

			IHash<T> hash = algorithm.Hash(plainData);
			IHashed<T> result = plainData.AsHashed(hash);

			return result;
		}
		/// <summary>
		/// Decorates an instance of <typeparamref name="T"/> with its hash value.
		/// </summary>
		/// <remarks>
		/// <paramref name="hash"/> is assumed to be based on <paramref name="plainData"/>.
		/// </remarks>
		/// <typeparam name="T">The type of data to decorate.</typeparam>
		/// <param name="plainData">The instance to decorate.</param>
		/// <param name="hash">A hash value of <paramref name="plainData"/>.</param>
		/// <returns><paramref name="plainData"/>, decorated with its hash value.</returns>
		public static IHashed<T> AsHashed<T>(this T plainData, IHash<T> hash)
		{
			plainData.ThrowIfDefault(nameof(plainData));
			hash.ThrowIfDefault(nameof(hash));

			var result = new Hashed<T>(plainData, hash);

			return result;
		}

		/// <summary>
		/// Calculates a hash value for an instance of <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of data to hash.</typeparam>
		/// <param name="plainData">The instance to hash.</param>
		/// <param name="algorithm">The algorithm to be used for hashing.</param>
		/// <returns>A hash value of <paramref name="plainData"/>.</returns>
		public static IHash<T> Hash<T>(this T plainData, IAlgorithm<T> algorithm)
		{
			plainData.ThrowIfDefault(nameof(plainData));
			algorithm.ThrowIfDefault(nameof(algorithm));

			IHash<T> hash = algorithm.Hash(plainData);

			return hash;
		}
		/// <summary>
		/// Creates a hash value for an unknown instance of <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of data to hash.</typeparam>
		/// <param name="hashValue">The hash value.</param>
		/// <param name="algorithm">The algorithm to be used for hashing.</param>
		/// <returns>A hash containing <paramref name="hashValue"/>.</returns>
		public static IHash<T> AsHash<T>(this Byte[] hashValue, IAlgorithm<T> algorithm)
		{
			hashValue.ThrowIfDefaultOrEmpty(nameof(hashValue));
			algorithm.ThrowIfDefault(nameof(algorithm));

			var hash = new Hash<T>(hashValue, algorithm);

			return hash;
		}

		/// <summary>
		/// Throws an exception if <paramref name="hashedData"/> is invalid.
		/// </summary>
		/// <typeparam name="T">The type of data decorated by <paramref name="hashedData"/>.</typeparam>
		/// <param name="hashedData">The instance to check.</param>
		/// <param name="dataName">The name of <paramref name="hashedData"/>, to be used in the exception message.</param>
		public static void ThrowIfInvalid<T>(this IHashed<T> hashedData, String dataName = null)
		{
			hashedData.ThrowIfDefault(nameof(hashedData));

			if (!hashedData.Verify())
			{
				throw new Exception($"Unable to verify {dataName ?? hashedData.ToString()}.");
			}
		}

		/// <summary>
		/// Verifies <see cref="WithHash"/>.
		/// </summary>
		/// <typeparam name="T">The type of data to decorate.</typeparam>
		/// <returns><see langword="true"/> when <see cref="WithHash"/> matches the hash value calculated for <see cref="IHashed{T}.PlainData"/>; otherwise, <see langword="false"/>.</returns>
		public static Boolean Verify<T>(this IHashed<T> hashedData)
		{
			hashedData.ThrowIfDefault(nameof(hashedData));

			Boolean result = hashedData.PlainData != null && (hashedData.Hash?.Algorithm.Hash(hashedData.PlainData).Equals(hashedData.Hash) ?? false);

			return result;
		}
	}
}
