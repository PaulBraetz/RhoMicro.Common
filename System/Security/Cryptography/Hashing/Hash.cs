using Fort;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Comparers;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	/// <summary>
	/// Default implementation of <see cref="IHash{T}"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The type of object this algorithm is able to calculate hash values for.
	/// </typeparam>
	public readonly struct Hash<T> : IHash<T>, IEquatable<IHash<T>>
	{
		/// <summary>
		/// Initializes a new instance with the value and algorithmn provided.
		/// </summary>
		/// <param name="value">The hash value.</param>
		/// <param name="algorithm">The algorithm used to calculate <paramref name="value"/>.</param>
		public Hash(Byte[] value, IAlgorithm<T> algorithm)
		{
			value.ThrowIfDefaultOrEmpty(nameof(value));
			algorithm.ThrowIfDefault(nameof(algorithm));

			Value = value;
			Algorithm = algorithm;
		}

		/// <summary>
		/// The hash value.
		/// </summary>
		public Byte[] Value { get; }
		/// <summary>
		/// The name of the algorithm used to calculate <see cref="Value"/>.
		/// </summary>
		public IAlgorithm<T> Algorithm { get; }

		/// <inheritdoc/>
		public override Boolean Equals(Object obj)
		{
			return obj is IHash<T> hash && Equals(hash);
		}

		/// <inheritdoc/>
		public Boolean Equals(IHash<T> other)
		{
			return HashEqualityComparer<T>.Instance.Equals(this, other);
		}

		/// <inheritdoc/>
		public override Int32 GetHashCode()
		{
			return HashEqualityComparer<T>.Instance.GetHashCode(this);
		}

		/// <inheritdoc/>
		public static Boolean operator ==(Hash<T> left, Hash<T> right)
		{
			return left.Equals(right);
		}

		/// <inheritdoc/>
		public static Boolean operator !=(Hash<T> left, Hash<T> right)
		{
			return !(left == right);
		}
	}
}
