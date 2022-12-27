using Fort;
using RhoMicro.Common.Math.Statistics.Abstractions;
using RhoMicro.Common.System.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.Math.Statistics
{
	public static class Statistic
	{
		/// <summary>
		/// Creates a new empirical statistic from a dataset.
		/// </summary>
		/// <typeparam name="T">The type of sample analyzed.</typeparam>
		/// <param name="samples">The samples to analyze.</param>
		/// <param name="divisionFunction1"></param>
		/// <param name="divisionFunction2"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static IEmpiricalStatistic<T> Create<T>(IEnumerable<T> samples,
													   Func<T, Int32, T> divisionFunction1,
													   Func<Int32, T, T> divisionFunction2,
													   IComparer<T> comparer = null)
			where T : INumber<T>, IRootFunctions<T>
		{
			samples.ThrowIfDefaultOrEmpty(nameof(samples));
			divisionFunction1.ThrowIfDefault(nameof(divisionFunction1));
			divisionFunction2.ThrowIfDefault(nameof(divisionFunction2));

			var operators = new OperatorStrategies<T>()
			{
				Comparer = comparer ?? Comparer<T>.Default,
				AdditionFunction = (t1, t2) => t1 + t2,
				SubstractionFunction = (t1, t2) => t1 - t2,
				ProductFunction = (t1, t2) => t1 * t2,
				DivisionFunction1 = divisionFunction1,
				DivisionFunction2 = divisionFunction2,
				DivisionFunction3 = (t1, t2) => t1 / t2,
				RootFunction = (t, i) => T.RootN(t, i),
				EqualityFunction = (t1, t2) => t1 == t2
			};

			var result = Create(samples, operators);

			return result;
		}

		private static IEmpiricalStatistic<T> Create<T>(IEnumerable<T> samples,
														IOperatorStrategies<T> operators)
		{
			samples.ThrowIfDefaultOrEmpty(nameof(samples));
			operators.ThrowIfDefault(nameof(operators));

			var builder = new EmpiricalStatisticInitializerBuilder<T>(operators);

			foreach (var sample in samples)
			{
				builder.AddSample(sample);
			}

			var initializer = builder.Build();
			var result = new EmpiricalStatistic<T>(initializer);

			return result;
		}
	}
}
