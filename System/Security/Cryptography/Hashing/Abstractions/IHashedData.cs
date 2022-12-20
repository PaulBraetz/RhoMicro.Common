namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions
{
	/// <summary>
	/// Decorates data with its representative hash value.
	/// </summary>
	/// <typeparam name="T">The type of data to decorate.</typeparam>
	public interface IHashed<T>
	{
		/// <summary>
		/// The underlying plain data.
		/// </summary>
		T PlainData { get; }
		/// <summary>
		/// The hash value representing <see cref="PlainData"/>.
		/// </summary>
		IHash<T> Hash { get; }
	}
}
