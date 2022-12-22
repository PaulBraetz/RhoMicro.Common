using RhoMicro.Common.System.Comparers;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Comparers
{
	/// <summary>
	/// Defines methods to support the comparison of instances of <see cref="IHash{T}"/> for equality.
	/// </summary>
	/// <typeparam name="T">
	/// The type of hashable data.
	/// </typeparam>
	public sealed class HashEqualityComparer<T> : IEqualityComparer<IHash<T>>
	{
		private HashEqualityComparer() { }

		/// <summary>
		/// Instance of <see cref="HashEqualityComparer{T}"/>.
		/// </summary>
		public static readonly HashEqualityComparer<T> Instance = new HashEqualityComparer<T>();

		/// <inheritdoc/>
		public Boolean Equals(IHash<T> x, IHash<T> y)
		{
			if (Object.ReferenceEquals(x, y))
			{
				return true;
			}
			else if (x == null || y == null)
			{
				return false;
			}

			Boolean result = ArrayEqualityComparer<Byte>.Instance.Equals(x.Value, y.Value) &&
						 EqualityComparer<IAlgorithm<T>>.Default.Equals(x.Algorithm, y.Algorithm);

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(IHash<T> obj)
		{
			if (obj is null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			var hashCode = new HashCode();
			hashCode.Add(obj.Value, ArrayEqualityComparer<Byte>.Instance);
			hashCode.Add(obj.Algorithm);
			Int32 result = hashCode.ToHashCode();

			return result;
		}
	}
}
