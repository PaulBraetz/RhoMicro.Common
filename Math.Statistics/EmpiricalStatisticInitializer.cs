using Fort;
using RhoMicro.Common.Math.Statistics.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.Math.Statistics
{
	internal sealed class EmpiricalStatisticInitializer<T>
	{
		private EmpiricalStatisticInitializer(IDictionary<T, IFrequencyDatum<T>> frequencies,
											  IOperatorStrategies<T> operators,
											  T maximum,
											  T minimum,
											  T modal,
											  Int32 sampleSize)
		{
			frequencies.ThrowIfDefaultOrEmpty(nameof(frequencies));
			operators.ThrowIfDefault(nameof(operators));

			Frequencies = frequencies;
			SampleSize = sampleSize;
			Modal = modal;

			Minimum = minimum;
			Maximum = maximum;
		}

		public static EmpiricalStatisticInitializer<T> Create(IDictionary<T, IFrequencyDatum<T>> frequencies,
															  IOperatorStrategies<T> operators,
															  T maximum,
															  T minimum,
															  T sum,
															  T product,
															  T inverseSum,
															  T quadraticSum,
															  T modal,
															  Int32 sampleSize)
		{
			var centerElementIndices = frequencies.Count % 2 == 0 ?
				new[] { (frequencies.Count / 2), (frequencies.Count / 2) - 1 } :
				new[] { (frequencies.Count / 2) };
			var centerElementsSum = centerElementIndices.Select(i => frequencies.ElementAt(i).Key).Aggregate(operators.AdditionFunction);
			var median = operators.DivisionFunction1.Invoke(centerElementsSum, centerElementIndices.Length);

			var arithmeticMean = operators.DivisionFunction1.Invoke(sum, sampleSize);
			var viSquaredSum = frequencies.Select(kvp => (sample: kvp.Value.Sample, absoluteFrequency: kvp.Value.AbsoluteFrequency))
				.SelectMany(t => Enumerable.Range(0, t.absoluteFrequency).Select(i => t.sample))
				.Select(xi => operators.SubstractionFunction.Invoke(xi, arithmeticMean))
				.Select(vi => operators.ProductFunction.Invoke(vi, vi))
				.Aggregate(operators.AdditionFunction);
			var variance = operators.DivisionFunction1.Invoke(viSquaredSum, sampleSize - 1);
			var standardDeviance = operators.RootFunction.Invoke(variance, 2);
			var variationCoefficient = operators.DivisionFunction3.Invoke(standardDeviance, arithmeticMean);

			var result = new EmpiricalStatisticInitializer<T>(frequencies, operators, maximum, minimum, modal, sampleSize)
			{
				Range = operators.SubstractionFunction.Invoke(maximum, minimum),

				ArithmeticMean = arithmeticMean,
				GeometricMean = operators.RootFunction.Invoke(product, sampleSize),
				HarmonicMean = operators.DivisionFunction2.Invoke(sampleSize, inverseSum),
				QuadraticMean = operators.RootFunction.Invoke(operators.DivisionFunction1(quadraticSum, sampleSize), 2),

				Median = median,

				Variance = variance,
				StandardDeviation = standardDeviance,
				VariationCoefficient = variationCoefficient
			};

			return result;
		}

		public IDictionary<T, IFrequencyDatum<T>> Frequencies { get; }

		public Int32 SampleSize { get; }
		public T Modal { get; }
		public T Median { get; private init; }

		public T Minimum { get; }
		public T Maximum { get; }

		public T Range { get; private init; }

		public T ArithmeticMean { get; private init; }
		public T GeometricMean { get; private init; }
		public T HarmonicMean { get; private init; }
		public T QuadraticMean { get; private init; }

		public T Variance { get; private init; }
		public T StandardDeviation { get; private init; }
		public T VariationCoefficient { get; private init; }
	}
}
