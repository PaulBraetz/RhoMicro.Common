using RhoMicro.Common.System.Abstractions;
using System;

namespace RhoMicro.Common.System.Security.Cryptography.Hashing.Abstractions
{
	internal abstract class ArgonVisitor : VisitorBase<IArgonHashingContext>
	{
		private readonly ArgonVersion _version;

		protected ArgonVisitor(ArgonVersion version)
		{
			_version = version;
		}

		protected sealed override Boolean CanReceive(IArgonHashingContext obj)
		{
			var result = obj.Settings.Version == _version;

			return result;
		}
		protected sealed override void Receive(IArgonHashingContext obj)
		{
			obj.HashingStrategy = plainData => Hash(plainData, obj.Settings);
		}
		protected abstract Byte[] Hash(Byte[] plainData, IArgonSettings settings);
	}
}
