using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions
{
	/// <summary>
	/// Settings to be used when hashing using the Argon hashing algorithm.
	/// </summary>
	public interface IArgonSettings
	{
		/// <summary>
		/// Determines the version of Argon to be used.
		/// </summary>
		ArgonVersion Version { get; set; }
		/// <summary>
		///  The number of iterations to apply to the hash.
		/// </summary>
		Int32 Iterations { get; set; }
		/// <summary>
		/// Any extra associated data to use while hashing.
		/// </summary>
		Byte[] AssociatedData { get; set; }
		/// <summary>
		/// The number of lanes to use while processing the hash.
		/// </summary>
		Int32 DegreeOfParallelism { get; set; }
		/// <summary>
		/// The hashing salt.
		/// </summary>
		Byte[] Salt { get; set; }
		/// <summary>
		/// An optional secret to use while hashing.
		/// </summary>
		Byte[] KnownSecret { get; set; }
		/// <summary>
		/// The number of 1kB memory blocks to use while proessing the hash.
		/// </summary>
		Int32 MemorySize { get; set; }
		/// <summary>
		/// The size of the resulting array.
		/// </summary>
		Int32 ResultSize { get; set; }
	}
}
