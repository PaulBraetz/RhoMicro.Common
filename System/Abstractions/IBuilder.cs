using System;

namespace RhoMicro.Common.System.Abstractions
{
	public interface IBuilder<T>
	{
		T Build();
		void Reset();
	}
}
