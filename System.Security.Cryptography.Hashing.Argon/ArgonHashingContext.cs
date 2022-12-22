using Fort;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	internal sealed class ArgonHashingContext : IArgonHashingContext
	{
		public ArgonHashingContext(IArgonSettings settings)
		{
			settings.ThrowIfDefault(nameof(settings));

			Settings = settings;
		}

		public IArgonSettings Settings { get; }

		public Func<Byte[], Byte[]> HashingStrategy { get; set; }
	}
}
