using System;
using System.Collections.Generic;
using System.Linq;

namespace RhoMicro.Common.System.Monads
{
	public readonly struct Carry<T, TCarry> : IEquatable<Carry<T, TCarry>>
	{
		public Carry(T value, IEnumerable<TCarry> carried)
		{
			Value = value;
			Carried = carried ?? Array.Empty<TCarry>();
		}
		public Carry(T value, TCarry carry) : this(value, new[] { carry }) { }
		public Carry(T value) : this(value, Array.Empty<TCarry>()) { }

		public static Carry<T, TCarry> Empty = default;

		public readonly T Value;
		public readonly IEnumerable<TCarry> Carried;

		public static Carry<T, TCarry> Unit(T value, TCarry carry) => new Carry<T, TCarry>(value, carry);
		public static Carry<T, TCarry> Unit(T value, IEnumerable<TCarry> carried) => new Carry<T, TCarry>(value, carried);
		public static Carry<T, TCarry> Unit(T value) => new Carry<T, TCarry>(value);

		public Carry<TResult, TCarry> Bind<TResult>(Func<T, Carry<TResult, TCarry>> function) => function.Invoke(Value).Concat(Carried);
		public Carry<T, TCarry> Concat(IEnumerable<TCarry> carried) => Unit(Value, (carried ?? Array.Empty<TCarry>()).Concat(Carried ?? Array.Empty<TCarry>()));

		public override Boolean Equals(Object obj)
		{
			return obj is Carry<T, TCarry> carry && Equals(carry);
		}
		public Boolean Equals(Carry<T, TCarry> other)
		{
			return EqualityComparer<T>.Default.Equals(Value, other.Value) &&
				(Carried == other.Carried ||
				((Carried?.SequenceEqual(other.Carried ?? Array.Empty<TCarry>())) ??
				other.Carried?.SequenceEqual(Array.Empty<TCarry>()) ?? true));
		}

		public override Int32 GetHashCode()
		{
			return HashCode.Combine(Value, Carried);
		}

		public static Boolean operator ==(Carry<T, TCarry> left, Carry<T, TCarry> right)
		{
			return left.Equals(right);
		}
		public static Boolean operator !=(Carry<T, TCarry> left, Carry<T, TCarry> right)
		{
			return !(left == right);
		}

		public override String ToString()
		{
			return $"{{Value={Value}, Carried=[{String.Join(", ", Carried)}]}}";
		}
	}
}