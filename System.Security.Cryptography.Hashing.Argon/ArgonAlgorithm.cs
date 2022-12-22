using Fort;
using RhoMicro.Common.System.Abstractions;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	/// <summary>
	/// Factory for instances of <see cref="IAlgorithm{T}"/> implementing the Argon hashing algorithm.
	/// </summary>
	/// <typeparam name="T">The type of object this algorithm is able to calculate hash values for.</typeparam>
	public static class ArgonAlgorithm<T>
	{
		/// <summary>
		/// Creates a new instance of <see cref="IAlgorithm{T}"/>, based on the settings and conversion strategy received.
		/// </summary>
		/// <param name="convertStrategy">The strategy to be used when converting instances of <typeparamref name="T"/> to an array of Bytes.</param>
		/// <param name="settings">The settings used for hashing using the Argon algorithm.</param>
		/// <returns>A new instance of <see cref="IAlgorithm{T}"/>, implementing the Argon hashing algorithm.</returns>
		public static IAlgorithm<T> Create(Func<T, Byte[]> convertStrategy, IArgonSettings settings)
		{
			convertStrategy.ThrowIfDefault(nameof(convertStrategy));
			settings.ThrowIfDefault(nameof(settings));

			settings = ArgonSettings.Clone(settings);

			var strategy = GetHashingStrategy(convertStrategy, settings);
			var result = DefaultAlgorithmBase<T>.Create(strategy);

			return result;
		}

		private static Func<T, Byte[]> GetHashingStrategy(Func<T, Byte[]> convertStrategy, IArgonSettings settings)
		{
			var hashingStrategy = GetHashingStrategy(settings);

			return hash;

			Byte[] hash(T data)
			{
				Byte[] plainBytes = convertStrategy.Invoke(data);
				Byte[] result = hashingStrategy.Invoke(plainBytes);

				return result;
			}
		}

		private static Func<Byte[], Byte[]> GetHashingStrategy(IArgonSettings settings)
		{
			var context = new ArgonHashingContext(settings);
			var visitors = new IVisitor<IArgonHashingContext>[]
			{
				new Argon2dVisitor(),
				new Argon2iVisitor(),
				new Argon2idVisitor()
			};
			foreach (var visitor in visitors)
			{
				visitor.Visit(context);
			}

			var result = context.HashingStrategy;

			return result;
		}
	}
}
