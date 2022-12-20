using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions
{
	/// <summary>
	/// Represents a hash value and the algorithm used to calculate it.
	/// </summary>
	/// <typeparam name="T">
	/// The type of object this algorithm is able to calculate hash values for.
	/// </typeparam>
	public interface IHash<T>
	{
		/// <summary>
		/// The hash value.
		/// </summary>
		Byte[] Value { get; }
		/// <summary>
		/// The algorithm used to calculate <see cref="Value"/>.
		/// </summary>
		IAlgorithm<T> Algorithm { get; }
	}
}
