using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System
{
	/// <summary>
	/// Represents a non-nullable string.
	/// </summary>
	public readonly struct ValueString : IEquatable<ValueString>, IEnumerable<Char>, IEnumerable, ICloneable, IComparable, IComparable<String>, IConvertible, IEquatable<String>
	{
		/// <summary>
		/// The underlying value. If a the underlying value is <see langword="null"/>, <see cref="String.Empty"/> is returned instead.
		/// </summary>
		public String Value => _value ?? String.Empty;
		private readonly String _value;

		/// <summary>
		/// Initializes a new instance with the value provided.
		/// </summary>
		/// <param name="value">The value used to initialize the instance.</param>
		public ValueString(String value)
		{
			_value = value;
		}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public override Boolean Equals(Object obj)
		{
			return obj is String @string && Equals(@string) ||
				obj is ValueString valueString && Equals(valueString);
		}

		public Boolean Equals(ValueString other)
		{
			return Equals(other.Value);
		}

		public override Int32 GetHashCode()
		{
			return HashCode.Combine(Value);
		}

		public override String ToString()
		{
			var result = Value;

			return result;
		}

		public static Boolean operator ==(ValueString left, ValueString right)
		{
			return left.Equals(right);
		}

		public static Boolean operator !=(ValueString left, ValueString right)
		{
			return !(left == right);
		}

		public static implicit operator ValueString(String value)
		{
			var result = new ValueString(value);

			return result;
		}
		public static implicit operator String(ValueString value)
		{
			var result = value.Value;

			return result;
		}

		public Boolean Equals(String other)
		{
			return Value.Equals(other);
		}

		public TypeCode GetTypeCode()
		{
			return Value.GetTypeCode();
		}

		public Boolean ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToBoolean(provider);
		}

		public Byte ToByte(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToByte(provider);
		}

		public Char ToChar(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToChar(provider);
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToDateTime(provider);
		}

		public Decimal ToDecimal(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToDecimal(provider);
		}

		public Double ToDouble(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToDouble(provider);
		}

		public Int16 ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToInt16(provider);
		}

		public Int32 ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToInt32(provider);
		}

		public Int64 ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToInt64(provider);
		}

		public SByte ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToSByte(provider);
		}

		public Single ToSingle(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToSingle(provider);
		}

		public String ToString(IFormatProvider provider)
		{
			return Value.ToString(provider);
		}

		public Object ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)Value).ToType(conversionType, provider);
		}

		public UInt16 ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToUInt16(provider);
		}

		public UInt32 ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToUInt32(provider);
		}

		public UInt64 ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)Value).ToUInt64(provider);
		}

		public Int32 CompareTo(String other)
		{
			return Value.CompareTo(other);
		}

		public Int32 CompareTo(Object obj)
		{
			return Value.CompareTo(obj);
		}

		public Object Clone()
		{
			return Value.Clone();
		}

		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)Value).GetEnumerator();
		}

		IEnumerator<Char> IEnumerable<Char>.GetEnumerator()
		{
			return ((IEnumerable<Char>)Value).GetEnumerator();
		}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
