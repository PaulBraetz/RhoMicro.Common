using Fort;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Comparers
{
	internal sealed class ComparerStrategy<T> : IComparer<T>
	{
		public ComparerStrategy(Func<T, T, Int32> strategy)
		{
			_strategy = strategy;
		}

		private readonly Func<T, T, Int32> _strategy;

		public Int32 Compare(T x, T y)
		{
			x.ThrowIfNull(nameof(x));
			y.ThrowIfNull(nameof(y));

			var result = _strategy.Invoke(x, y);

			return result;
		}
	}
}
