using Fort;
using RhoMicro.Common.Math.Statistics.Abstractions;
using RhoMicro.Common.System.Abstractions;
using RhoMicro.Common.System.Monads;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RhoMicro.Common.Math.Statistics
{
	internal sealed class EmpiricalStatisticInitializerBuilder<T>
	{
		private sealed class FrequencyDatum : IFrequencyDatum<T>
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

			public FrequencyDatum(T sample)
			{
				Sample = sample;
			}

			public T Sample { get; }
			public Int32 AbsoluteFrequency { get; private set; }
			public Double RelativeFrequency { get; private set; }

			public void Increment()
			{
				AbsoluteFrequency++;
			}

			public IFrequencyDatum<T> ToImmutable(Int32 sampleSize)
			{
				var result = new ValueFrequencyDatum()
				{
					Sample = Sample,
					AbsoluteFrequency = AbsoluteFrequency,
					RelativeFrequency = (Double)AbsoluteFrequency / sampleSize
				};

				return result;
			}
		}

		public EmpiricalStatisticInitializerBuilder(IOperatorStrategies<T> operators)
		{
			operators.ThrowIfDefaultOrNot(o =>
			{
				Boolean result = o is
				{
					AdditionFunction: not null,
					Comparer: not null,
					DivisionFunction1: not null,
					DivisionFunction2: not null,
					DivisionFunction3: not null,
					EqualityFunction: not null,
					ProductFunction: not null,
					RootFunction: not null,
					SubstractionFunction: not null
				};

				return result;
			});

			_operators = operators;
		}

		private IDictionary<T, FrequencyDatum> Frequencies { get; set; } = new Dictionary<T, FrequencyDatum>();
		private readonly Object _syncRoot = new();

		private readonly IOperatorStrategies<T> _operators;

		private Int32 SampleSize { get; set; }
		private FrequencyDatum Modal { get; set; }

		private Maybe<T> Minimum { get; set; }
		private Maybe<T> Maximum { get; set; }

		private Maybe<T> Sum { get; set; } = default;
		private Maybe<T> Product { get; set; } = default;
		private Maybe<T> InverseSum { get; set; } = default;
		private Maybe<T> QuadraticSum { get; set; } = default;

		public void AddSample(T sample)
		{
			lock (_syncRoot)
			{
				SampleSize++;
				if (!Frequencies.TryGetValue(sample, out var value))
				{
					value = GetNewDatum(sample);
					Frequencies.Add(sample, value);
				}

				value.Increment();
				OnSampleAdded(value);
			}
		}

		private void OnSampleAdded(FrequencyDatum datum)
		{
			UpdateModal(datum);
			UpdateSum(datum);
			UpdateProduct(datum);
			UpdateInverseSum(datum);
			UpdateQuadraticSum(datum);
		}
		private void UpdateQuadraticSum(FrequencyDatum datum)
		{
			var square = _operators.ProductFunction.Invoke(datum.Sample, datum.Sample);
			if (QuadraticSum.HasValue)
			{
				var quadraticSum = _operators.AdditionFunction.Invoke(QuadraticSum.Value, square);
				QuadraticSum = Maybe.Just(quadraticSum);
			}
			else
			{
				QuadraticSum = Maybe.Just(square);
			}
		}
		private void UpdateInverseSum(FrequencyDatum datum)
		{
			var inverse = _operators.DivisionFunction2.Invoke(1, datum.Sample);
			if (InverseSum.HasValue)
			{
				var inverseSum = _operators.AdditionFunction.Invoke(InverseSum.Value, inverse);
				InverseSum = Maybe.Just(inverseSum);
			}
			else
			{
				InverseSum = Maybe.Just(inverse);
			}
		}
		private void UpdateProduct(FrequencyDatum datum)
		{
			var square = _operators.ProductFunction.Invoke(datum.Sample, datum.Sample);
			var root = _operators.RootFunction.Invoke(square, 2);
			if (_operators.EqualityFunction.Invoke(root, datum.Sample))
			{
				if (Product.HasValue)
				{

					var product = _operators.ProductFunction.Invoke(Product.Value, datum.Sample);
					Product = Maybe.Just(product);
				}
				else
				{
					Product = Maybe.Just(datum.Sample);
				}
			}
		}
		private void UpdateSum(FrequencyDatum datum)
		{
			if (Sum.HasValue)
			{
				var sum = _operators.AdditionFunction.Invoke(Sum.Value, datum.Sample);
				Sum = Maybe.Just(sum);
			}
			else
			{
				Sum = Maybe.Just(datum.Sample);
			}
		}
		private void UpdateModal(FrequencyDatum datum)
		{
			if (datum.AbsoluteFrequency > (Modal?.AbsoluteFrequency ?? 0))
			{
				Modal = datum;
			}
		}

		private FrequencyDatum GetNewDatum(T sample)
		{
			var result = new FrequencyDatum(sample);

			UpdateMinMax(result);

			return result;
		}
		private void UpdateMinMax(FrequencyDatum datum)
		{
			if (!Minimum.HasValue || _operators.Comparer.Compare(datum.Sample, Minimum.Value) < 0)
			{
				Minimum = Maybe.Just(datum.Sample);
			}
			if (!Maximum.HasValue || _operators.Comparer.Compare(datum.Sample, Maximum.Value) > 0)
			{
				Maximum = Maybe.Just(datum.Sample);
			}
		}

		public EmpiricalStatisticInitializer<T> Build()
		{
			lock (_syncRoot)
			{
				if (SampleSize == 0)
				{
					throw new InvalidOperationException();
				}

				var frequencies = Frequencies.OrderBy(kvp => kvp.Key, _operators.Comparer).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToImmutable(SampleSize));

				var result = EmpiricalStatisticInitializer<T>.Create(frequencies,
																	 _operators,
																	 Maximum.Value,
																	 Minimum.Value,
																	 Sum.Value,
																	 Product.Value,
																	 InverseSum.Value,
																	 QuadraticSum.Value,
																	 Modal.Sample,
																	 SampleSize);

				return result;
			}
		}
	}
}
