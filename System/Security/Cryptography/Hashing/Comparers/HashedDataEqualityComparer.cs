using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Comparers
{
	/// <summary>
	/// Defines methods to support the comparison of instances of <see cref="IHashed{T}"/> for equality.
	/// </summary>
	/// <typeparam name="T">
	/// The type of hashable data.
	/// </typeparam>
	public sealed class HashedDataEqualityComparer<T> : IEqualityComparer<IHashed<T>>
	{
		private HashedDataEqualityComparer() { }

		/// <summary>
		/// Instance of <see cref="HashedDataEqualityComparer{T}"/>.
		/// </summary>
		public static readonly HashedDataEqualityComparer<T> Instance = new HashedDataEqualityComparer<T>();

		/// <inheritdoc/>
		public Boolean Equals(IHashed<T> x, IHashed<T> y)
		{
			if (Object.ReferenceEquals(x, y))
			{
				return true;
			}
			else if (x == null || y == null)
			{
				return false;
			}

			Boolean result = HashEqualityComparer<T>.Instance.Equals(x.Hash, y.Hash) &&
				EqualityComparer<T>.Default.Equals(x.PlainData, y.PlainData);

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(IHashed<T> obj)
		{
			if (obj is null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			var hashCode = new HashCode();
			hashCode.Add(obj.Hash, HashEqualityComparer<T>.Instance);
			hashCode.Add(obj.PlainData);
			Int32 result = hashCode.ToHashCode();

			return result;
		}
	}
}
