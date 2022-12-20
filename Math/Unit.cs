using Fort;
using RhoMicro.Common.Math.Abstractions;
using RhoMicro.Common.Math.Comparers;
using System;

namespace RhoMicro.Common.Math
{
	/// <summary>
	/// Represents a unit of measurement.
	/// </summary>
	public readonly struct Unit : IEquatable<IUnit>, IUnit
	{
		/// <summary>
		/// Initializes a new instance with the name provided.
		/// </summary>
		/// <param name="name">The name of the unit.</param>
		public Unit(String name)
		{
			name.ThrowIfDefaultOrEmpty(nameof(name));

			Name = String.Intern(name);
		}

		/// <summary>
		/// The name of the unit.
		/// </summary>
		public String Name { get; }

		/// <inheritdoc/>
		public override Boolean Equals(Object obj)
		{
			return obj is IUnit unit && Equals(unit);
		}
		/// <inheritdoc/>
		public Boolean Equals(IUnit other)
		{
			return UnitEqualityComparer.Instance.Equals(this, other);
		}

		/// <inheritdoc/>
		public override Int32 GetHashCode()
		{
			return UnitEqualityComparer.Instance.GetHashCode(this);
		}
		/// <inheritdoc/>
		public static Boolean operator ==(Unit left, Unit right)
		{
			return left.Equals(right);
		}
		/// <inheritdoc/>
		public static Boolean operator !=(Unit left, Unit right)
		{
			return !(left == right);
		}
	}
}