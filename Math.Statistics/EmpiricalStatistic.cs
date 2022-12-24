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

		internal EmpiricalStatistic(IDictionary<T, IFrequencyDatum<T>> frequencies)
		{
			_frequencies = frequencies;
		}

		private readonly IDictionary<T, IFrequencyDatum<T>> _frequencies;

		public IEnumerable<T> Samples
		{
			get
			{
				foreach (var sample in _frequencies.Keys)
				{
					yield return sample;
				}
			}
		}
		public IEnumerable<T> DistinctSamples
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

		public T Range { get; internal init; }
		public T Maximum { get; internal init; }
		public T Minimum { get; internal init; }

		public Int32 SampleSize { get; internal init; }
		public Int32 DistinctSampleSize => _frequencies.Count;

		public T ArithmeticMean { get; internal init; }
		public T GeometricMean { get; internal init; }
		public T HarmonicMean { get; internal init; }
		public T QuadraticMean { get; internal init; }

		public T Modal { get; internal init; }
		public T Median { get; internal init; }

		public T Variance { get; internal init; }
		public T StandardDeviation { get; internal init; }
		public T VariationCoefficient { get; internal init; }

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
