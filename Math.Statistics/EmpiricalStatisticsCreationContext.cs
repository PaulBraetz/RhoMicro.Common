using RhoMicro.Common.System.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RhoMicro.Common.Math.Statistics
{
	internal sealed class EmpiricalStatisticsCreationContext<T>
	{
		private sealed class FrequencyDatumInitializer : IFrequencyDatum<T>
		{
			private readonly struct ValueFrequencyDatum : IFrequencyDatum<T>, IEquatable<ValueFrequencyDatum>
			{
				public Int32 AbsoluteFrequency { get; init; }
				public Double RelativeFrequency { get; init; }
				public T Sample { get; init; }

				public override Boolean Equals(Object obj)
				{
					return obj is ValueFrequencyDatum datum && Equals(datum);
				}

				public Boolean Equals(ValueFrequencyDatum other)
				{
					return EqualityComparer<T>.Default.Equals(Sample, other.Sample);
				}

				public override Int32 GetHashCode()
				{
					return HashCode.Combine(Sample);
				}

				public static Boolean operator ==(ValueFrequencyDatum left, ValueFrequencyDatum right)
				{
					return left.Equals(right);
				}

				public static Boolean operator !=(ValueFrequencyDatum left, ValueFrequencyDatum right)
				{
					return !(left == right);
				}
			}

			public FrequencyDatumInitializer(T sample)
			{
				Sample = sample;
			}

			public T Sample { get; }
			public Int32 AbsoluteFrequency { get; private set; }
			public Double RelativeFrequency { get; private set; }

			public void OnSampleAdded(Int32 newSampleSize, T sampleAdded)
			{
				if (EqualityComparer<T>.Default.Equals(Sample, sampleAdded))
				{
					AbsoluteFrequency++;
				}
				RelativeFrequency = (Double)AbsoluteFrequency / newSampleSize;
			}

			public IFrequencyDatum<T> ToImmutable()
			{
				var result = new ValueFrequencyDatum()
				{
					Sample = Sample,
					AbsoluteFrequency = AbsoluteFrequency,
					RelativeFrequency = RelativeFrequency
				};

				return result;
			}
		}

		private readonly Dictionary<T, FrequencyDatumInitializer> _frequencies = new Dictionary<T, FrequencyDatumInitializer>();

		public Int32 SampleSize { get; private set; }
		public IFrequencyDatum<T> Modal { get; private set; }

		private readonly Object _syncRoot = new object();

		public void AddSample(T sample)
		{
			lock (_syncRoot)
			{
				if (!_frequencies.TryGetValue(sample, out var value))
				{
					value = new FrequencyDatumInitializer(sample);
					_frequencies.Add(sample, value);
				}

				SampleSize++;

				value.OnSampleAdded(SampleSize, sample);

				if (value.AbsoluteFrequency > (Modal?.AbsoluteFrequency ?? 0))
				{
					Modal = value.ToImmutable();
				}
			}
		}
		public IDictionary<T, IFrequencyDatum<T>> GetFrequencies()
		{
			return _frequencies.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToImmutable());
		}
	}
}
