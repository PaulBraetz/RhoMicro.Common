using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Abstractions
{
	public interface IFactory<T>
	{
		T Create();
	}
}
