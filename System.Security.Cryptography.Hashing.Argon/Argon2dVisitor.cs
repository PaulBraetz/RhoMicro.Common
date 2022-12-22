using Konscious.Security.Cryptography;
using RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing
{
	internal sealed class Argon2dVisitor : ArgonVisitor
	{
		public Argon2dVisitor() : base(ArgonVersion.Argon2d)
		{
		}

		protected override Byte[] Hash(Byte[] plainData, IArgonSettings settings)
		{
			var algorithm = new Argon2d(plainData)
			{
				AssociatedData = settings.AssociatedData,
				DegreeOfParallelism = settings.DegreeOfParallelism,
				Iterations = settings.Iterations,
				Salt = settings.Salt,
				KnownSecret = settings.KnownSecret,
				MemorySize = settings.MemorySize
			};
			var result = algorithm.GetBytes(settings.ResultSize);

			return result;
		}
	}
}
