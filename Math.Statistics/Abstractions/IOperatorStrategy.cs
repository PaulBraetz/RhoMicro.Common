using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.Math.Statistics.Abstractions
{
	internal interface IOperatorStrategies<T>
	{
		IComparer<T> Comparer { get; }
		Func<T, T, T> AdditionFunction { get; }
		Func<T, T, T> SubstractionFunction { get; }
		Func<T, T, T> ProductFunction { get; }
		Func<T, Int32, T> DivisionFunction1 { get; }
		Func<Int32, T, T> DivisionFunction2 { get; }
		Func<T, T, T> DivisionFunction3 { get; }
		Func<T, Int32, T> RootFunction { get; }
		Func<T, T, Boolean> EqualityFunction { get; }
	}
}
