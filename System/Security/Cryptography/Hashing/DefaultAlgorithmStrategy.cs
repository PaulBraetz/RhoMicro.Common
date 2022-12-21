using Fort;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;
using System.IO;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	internal sealed class DefaultAlgorithmStrategy<T> : DefaultAlgorithmBase<T>
	{
		public DefaultAlgorithmStrategy(Func<T, Byte[]> convertStrategy, DefaultAlgorithmBase<T>.BuiltinAlgorithm algorithm) : base(algorithm)
		{
			convertStrategy.ThrowIfDefault(nameof(convertStrategy));

			_convertStrategy = convertStrategy;
		}
		public DefaultAlgorithmStrategy(Func<T, Stream> serializeStrategy, DefaultAlgorithmBase<T>.BuiltinAlgorithm algorithm) : base(algorithm)
		{
			serializeStrategy.ThrowIfDefault(nameof(serializeStrategy));

			_serializeStrategy = serializeStrategy;
		}

		private readonly Func<T, Byte[]> _convertStrategy;
		private readonly Func<T, Stream> _serializeStrategy;

		protected override Byte[] Convert(T plainData)
		{
			var result = _convertStrategy?.Invoke(plainData) ??
				throw new NotSupportedException("No strategy for converting plain data to bytes has been set.");

			return result;
		}
		protected override Stream Serialize(T plainData)
		{
			var result = _serializeStrategy?.Invoke(plainData) ?? base.Serialize(plainData);

			return result;
		}
	}
}
