using Fort;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Comparers
{
	internal sealed class DefaultComparer<T> : IComparer<T>
	where T : IComparable<T>
	{
		private DefaultComparer() { }

		public static readonly IComparer<T> Instance = new DefaultComparer<T>();

		public Int32 Compare(T x, T y)
		{
			x.ThrowIfNull(nameof(x));
			y.ThrowIfNull(nameof(y));

			var result = x.CompareTo(y);

			return result;
		}
	}
}
