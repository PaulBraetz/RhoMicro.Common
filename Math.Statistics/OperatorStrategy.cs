using RhoMicro.Common.Math.Statistics.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.Math.Statistics
{
	internal readonly struct OperatorStrategies<T> : IOperatorStrategies<T>
	{
		public IComparer<T> Comparer { get; init; }
		public Func<T, T, T> AdditionFunction { get; init; }
		public Func<T, T, T> SubstractionFunction { get; init; }
		public Func<T, T, T> ProductFunction { get; init; }
		public Func<T, Int32, T> DivisionFunction1 { get; init; }
		public Func<Int32, T, T> DivisionFunction2 { get; init; }
		public Func<T, T, T> DivisionFunction3 { get; init; }
		public Func<T, Int32, T> RootFunction { get; init; }
		public Func<T, T, Boolean> EqualityFunction { get; init; }
	}
}
