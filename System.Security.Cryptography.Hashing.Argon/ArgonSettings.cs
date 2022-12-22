using Fort;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	/// <summary>
	/// Default implementation of <see cref="IArgonSettings"/>.
	/// </summary>
	public sealed class ArgonSettings : IArgonSettings
	{
		/// <inheritdoc/>
		public ArgonVersion Version { get; set; }
		/// <inheritdoc/>
		public Int32 Iterations { get; set; }
		/// <inheritdoc/>
		public Byte[] AssociatedData { get; set; }
		/// <inheritdoc/>
		public Int32 DegreeOfParallelism { get; set; }
		/// <inheritdoc/>
		public Byte[] Salt { get; set; }
		/// <inheritdoc/>
		public Byte[] KnownSecret { get; set; }
		/// <inheritdoc/>
		public Int32 MemorySize { get; set; }
		/// <inheritdoc/>
		public Int32 ResultSize { get; set; }

		/// <summary>
		/// Creates a deep clone of an instance of <see cref="IArgonSettings"/>, meaning array properties will be cloned as well.
		/// </summary>
		/// <param name="settings">The instance to clone.</param>
		/// <returns>A new, cloned instance of <see cref="IArgonSettings"/>.</returns>
		internal static IArgonSettings Clone(IArgonSettings settings)
		{
			settings.ThrowIfDefault(nameof(settings));

			var result = new ArgonSettings()
			{
				Version = settings.Version,
				Iterations = settings.Iterations,
				DegreeOfParallelism = settings.DegreeOfParallelism,
				MemorySize = settings.MemorySize,
				ResultSize = settings.ResultSize,
				AssociatedData = settings.AssociatedData?.Clone() as Byte[],
				KnownSecret = settings.KnownSecret?.Clone() as Byte[],
				Salt = settings.Salt?.Clone() as Byte[]
			};

			return result;
		}
	}
}
