using Fort;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	internal sealed class DefaultAlgorithmStrategy<T> : DefaultAlgorithmBase<T>
	{
		public DefaultAlgorithmStrategy(Func<T, Byte[]> convertStrategy, DefaultAlgorithmBase<T>.BuiltinAlgorithm algorithm) : base(algorithm)
		{
			convertStrategy.ThrowIfDefault(nameof(convertStrategy));

			_convertStrategy = convertStrategy;
		}

		private readonly Func<T, Byte[]> _convertStrategy;

		protected override Byte[] Convert(T plainData)
		{
			Byte[] result = _convertStrategy.Invoke(plainData);

			return result;
		}
	}
}
