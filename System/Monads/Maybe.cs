using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Monads
{
	public static class Maybe
	{
		public static Maybe<T> Unit<T>(Func<T> valueFactory, Boolean hasValue) => Maybe<T>.Unit(valueFactory, hasValue);
		public static Maybe<T> Just<T>(T value) => Maybe<T>.Just(value);
		public static Maybe<T> Nothing<T>() => Maybe<T>.Nothing();
	}
	public readonly struct Maybe<T> : IEquatable<Maybe<T>>
	{
		public Maybe(T value)
		{
			Value = value;
			HasValue = true;
		}

		public static readonly Maybe<T> Empty = default;

		public readonly T Value;

		public readonly Boolean HasValue;

		public static Maybe<T> Unit(Func<T> valueFactory, Boolean hasValue) => hasValue ? Just(valueFactory.Invoke()) : Nothing();
		public static Maybe<T> Just(T value) => new Maybe<T>(value);
		public static Maybe<T> Nothing() => Empty;

		public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> function) => HasValue ? function.Invoke(Value) : Maybe<TResult>.Nothing();
		public Maybe<TResult> Bind<TResult>(Func<T, TResult> function) => Bind(v => Maybe<TResult>.Just(function.Invoke(v)));

		public override Boolean Equals(Object obj) => obj is Maybe<T> maybe && Equals(maybe);
		public Boolean Equals(Maybe<T> other) => HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
		public override Int32 GetHashCode() => HashCode.Combine(Value, HasValue);

		public static Boolean operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);
		public static Boolean operator !=(Maybe<T> left, Maybe<T> right) => !(left == right);

		public override String ToString()
		{
			var value = HasValue ? $"\"Just {Value}\"" : "\"Nothing\"";
			return $"{{Value={value}}}";
		}
	}
}