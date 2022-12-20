using Fort;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Comparers;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	/// <summary>
	/// Default implementation of <see cref="IHashed{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of data to decorate.</typeparam>
	public readonly struct Hashed<T> : IHashed<T>, IEquatable<IHashed<T>>
	{
		/// <summary>
		/// Initializes a new instance with the data and hash value provided.
		/// </summary>
		/// <param name="plainData">The plain data.</param>
		/// <param name="hash">The hash value of <paramref name="plainData"/>.</param>
		public Hashed(T plainData, IHash<T> hash)
		{
			plainData.ThrowIfDefault(nameof(plainData));
			hash.ThrowIfDefault(nameof(hash));

			PlainData = plainData;
			Hash = hash;
		}

		/// <inheritdoc/>
		public T PlainData { get; }
		/// <inheritdoc/>
		public IHash<T> Hash { get; }

		/// <inheritdoc/>
		public override Boolean Equals(Object obj)
		{
			return obj is IHashed<T> data && Equals(data);
		}

		/// <inheritdoc/>
		public Boolean Equals(IHashed<T> other)
		{
			return HashedDataEqualityComparer<T>.Instance.Equals(this, other);
		}

		/// <inheritdoc/>
		public override Int32 GetHashCode()
		{
			return HashedDataEqualityComparer<T>.Instance.GetHashCode(this);
		}

		/// <inheritdoc/>
		public static Boolean operator ==(Hashed<T> left, Hashed<T> right)
		{
			return left.Equals(right);
		}

		/// <inheritdoc/>
		public static Boolean operator !=(Hashed<T> left, Hashed<T> right)
		{
			return !(left == right);
		}
	}
}
