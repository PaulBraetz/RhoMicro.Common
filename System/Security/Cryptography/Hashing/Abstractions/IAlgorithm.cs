namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions
{
	/// <summary>
	/// Represents a hashing algorithm for hashing instances of <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The type of object this algorithm is able to calculate hash values for.
	/// </typeparam>
	public interface IAlgorithm<T>
	{
		/// <summary>
		/// Calculates the hash value of an instance of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="plainData">The instance to hash.</param>
		/// <returns>A hash value of <paramref name="plainData"/>.</returns>
		IHash<T> Hash(T plainData);
	}
}
