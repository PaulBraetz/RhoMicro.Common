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
		public static IEmpiricalStatistic<T> Create<T>(IEnumerable<T> samples,
													   Func<T, Int32, T> divisionFunction1,
													   Func<Int32, T, T> divisionFunction2)
			where T : INumber<T>, IRootFunctions<T>
		{
			var result = Create(samples,
								(t1, t2) => t1 + t2,
								(t1, t2) => t1 - t2,
								(t1, t2) => t1 * t2,
								divisionFunction1 ?? fallbackDivision1,
								divisionFunction2 ?? fallbackDivision2,
								(t1, t2) => t1 / t2,
								(t, i) => T.RootN(t, i),
								(t1, t2) => t1 == t2);

			return result;

			T fallbackDivision1(T t, Int32 i)
			{
				throw new NotSupportedException();
			}
			T fallbackDivision2(Int32 i, T t)
			{
				throw new NotSupportedException();
			}
		}
		private static IEmpiricalStatistic<T> Create<T>(IEnumerable<T> samples,
													   Func<T, T, T> additionFunction,
													   Func<T, T, T> substractionFunction,
													   Func<T, T, T> productFunction,
													   Func<T, Int32, T> divisionFunction1,
													   Func<Int32, T, T> divisionFunction2,
													   Func<T, T, T> divisionFunction3,
													   Func<T, Int32, T> rootFunction,
													   Func<T, T, Boolean> equalityFunction)
		{
			samples.ThrowIfDefaultOrEmpty(nameof(samples));

			var minimum = default(T);
			var maximum = default(T);

			T sum = default;
			T product = default;
			T inverseSum = default;
			T quadraticSum = default;

			var context = new EmpiricalStatisticsCreationContext<T>();

			foreach (var sample in samples)
			{
				context.AddSample(sample);
			}

			var frequencies = context.GetFrequencies();

			var range = substractionFunction.Invoke(maximum, minimum);

			var tt = frequencies.Sum(v => v.Value.RelativeFrequency);

			var arithmeticMean = divisionFunction1.Invoke(sum, context.SampleSize);
			var geometricMean = rootFunction.Invoke(product, context.SampleSize);
			var harmonicMean = divisionFunction2.Invoke(context.SampleSize, inverseSum);
			var quadraticMean = rootFunction.Invoke(divisionFunction1.Invoke(quadraticSum, context.SampleSize), 2);

			var median = frequencies.ElementAt(context.SampleSize / 2).Key;

			T viSquaredSum = default;
			foreach (var sample in frequencies)
			{
				var vi = substractionFunction.Invoke(sample.Key, arithmeticMean);
				var viSquared = productFunction.Invoke(vi, vi);
				plus(ref viSquaredSum, viSquared, default);
			}

			var variance = divisionFunction1.Invoke(viSquaredSum, context.SampleSize - 1);
			var standardDeviation = rootFunction.Invoke(variance, 2);

			var variationCoefficient = divisionFunction3.Invoke(standardDeviation, arithmeticMean);

			var result = new EmpiricalStatistic<T>(frequencies)
			{
				Range = range,
				Maximum = maximum,
				Minimum = minimum,

				SampleSize = context.SampleSize,

				ArithmeticMean = arithmeticMean,
				GeometricMean = geometricMean,
				HarmonicMean = harmonicMean,
				QuadraticMean = quadraticMean,

				Modal = context.Modal.Sample,
				Median = median,

				Variance = variance,
				StandardDeviation = standardDeviation,
				VariationCoefficient = variationCoefficient
			};

			return result;
			void plus(ref T sum, T value, T defaultValue)
			{
				if (equalityFunction.Invoke(sum, defaultValue))
				{
					sum = value;
				}
				else
				{
					sum = additionFunction.Invoke(sum, value);
				}
			}
			void multiply(ref T product, T factor, T defaultProduct)
			{
				if (equalityFunction.Invoke(product, defaultProduct))
				{
					product = factor;
				}
				else
				{
					product = productFunction.Invoke(product, factor);
				}
			}
		}
	}
}
