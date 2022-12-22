using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions
{
	internal interface IArgonHashingContext
	{
		IArgonSettings Settings { get; }
		Func<Byte[], Byte[]> HashingStrategy { get; set; }
	}
}
