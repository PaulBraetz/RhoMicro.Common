using RhoMicro.Common.Math.Abstractions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Xml.Schema;

namespace RhoMicro.Common.Math.Statistics.Abstractions
{
	public interface IEmpiricalStatistic<T>
	{
		IEnumerable<T> Samples { get; }
		IEnumerable<T> DistinctSamples { get; }

		T Range { get; }
		T Maximum { get; }
		T Minimum { get; }

		Int32 SampleSize { get; }
		Int32 DistinctSampleSize { get; }

		T ArithmeticMean { get; }
		T GeometricMean { get; }
		T HarmonicMean { get; }
		T QuadraticMean { get; }

		T Modal { get; }
		T Median { get; }

		T Variance { get; }
		T StandardDeviation { get; }
		T VariationCoefficient { get; }

		T GetEmpiricalQuantile(Double relativeProbability);

		Int32 GetAbsoluteFrequency(T x);
		Double GetRelativeFrequency(T x);
		T GetClosestSample(Double relativeFrequency);
		T GetClosestSample(Int32 absoluteFrequency);
	}
}
