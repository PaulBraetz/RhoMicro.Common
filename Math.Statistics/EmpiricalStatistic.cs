using Fort;
using RhoMicro.Common.Math.Statistics.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RhoMicro.Common.System.Collections;

namespace RhoMicro.Common.Math.Statistics
{
	internal sealed class EmpiricalStatistic<T> : IEmpiricalStatistic<T>
	{
		public EmpiricalStatistic(EmpiricalStatisticInitializer<T> initializer)
		{
			initializer.ThrowIfDefault(nameof(initializer));

			_frequencies = initializer.Frequencies;

			Range = initializer.Range;
			Maximum = initializer.Maximum;
			Minimum = initializer.Minimum;

			SampleSize = initializer.SampleSize;

			ArithmeticMean = initializer.ArithmeticMean;
			GeometricMean = initializer.GeometricMean;
			QuadraticMean = initializer.QuadraticMean;
			HarmonicMean = initializer.HarmonicMean;

			Modal = initializer.Modal;
			Median = initializer.Median;

			Variance = initializer.Variance;
			StandardDeviation = initializer.StandardDeviation;
			VariationCoefficient = initializer.VariationCoefficient;
		}

		private readonly IDictionary<T, IFrequencyDatum<T>> _frequencies;

		public IEnumerable<T> DistinctSamples
		{
			get
			{
				foreach (var sample in _frequencies.Keys)
				{
					yield return sample;
				}
			}
		}
		public IEnumerable<T> Samples
		{
			get
			{
				foreach (var kvp in _frequencies)
				{
					for (var i = 0; i < kvp.Value.AbsoluteFrequency; i++)
					{
						yield return kvp.Key;
					}
				}
			}
		}

		public T Range { get; }
		public T Maximum { get; }
		public T Minimum { get; }

		public Int32 SampleSize { get; }
		public Int32 DistinctSampleSize => _frequencies.Count;

		public T ArithmeticMean { get; }
		public T GeometricMean { get; }
		public T HarmonicMean { get; }
		public T QuadraticMean { get; }

		public T Modal { get; }
		public T Median { get; }

		public T Variance { get; }
		public T StandardDeviation { get; }
		public T VariationCoefficient { get; }

		public T GetEmpiricalQuantile(Double relativeFrequency)
		{
			var collection = (ICollection<KeyValuePair<T, (Double relative, Int32 absolute)>>)_frequencies;
			var quantile = collection.ElementAt((Int32)(SampleSize * relativeFrequency) + 1).Key;

			return quantile;
		}

		public Int32 GetAbsoluteFrequency(T x)
		{
			var result = _frequencies.TryGetValue(x, out var r) ? r.AbsoluteFrequency : 0;

			return result;
		}
		public Double GetRelativeFrequency(T x)
		{
			var result = _frequencies.TryGetValue(x, out var r) ? r.RelativeFrequency : 0;

			return result;
		}
		public T GetClosestSample(Double relativeFrequency)
		{
			var result = _frequencies.BinaryFuzzySearch(kvp => (Int32)((kvp.Value.RelativeFrequency - relativeFrequency) * 1e6)).Key;

			return result;
		}
		public T GetClosestSample(Int32 absoluteFrequency)
		{
			var result = _frequencies.BinaryFuzzySearch(kvp => kvp.Value.AbsoluteFrequency - absoluteFrequency).Key;

			return result;
		}
	}
}
