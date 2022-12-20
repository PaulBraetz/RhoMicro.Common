using Fort;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	internal sealed class AlgorithmStrategy<T> : DisposableBase, IAlgorithm<T>
	{
		public AlgorithmStrategy(Func<T, Byte[]> hashingStrategy)
		{
			hashingStrategy.ThrowIfDefault(nameof(hashingStrategy));

			_hashingStrategy = hashingStrategy;
		}

		private readonly Func<T, Byte[]> _hashingStrategy;

		public IHash<T> Hash(T plainData)
		{
			Byte[] hashBytes = _hashingStrategy.Invoke(plainData);
			IHash<T> result = hashBytes.AsHash(this);

			return result;
		}
	}
}
