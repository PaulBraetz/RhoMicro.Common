using RhoMicro.Common.Math.Abstractions;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.Math.Comparers
{
	/// <summary>
	/// Equality comparer for instances of <see cref="IUnit"/>.
	/// </summary>
	public sealed class UnitEqualityComparer : IEqualityComparer<IUnit>
	{
		private UnitEqualityComparer() { }

		/// <summary>
		/// Instance of <see cref="IEqualityComparer{T}"/> where <c>T</c> is <see cref="IUnit"/>.
		/// </summary>
		public static readonly IEqualityComparer<IUnit> Instance = new UnitEqualityComparer();

		/// <inheritdoc/>
		public Boolean Equals(IUnit x, IUnit y)
		{
			if (x == null)
			{
				return y == null;
			}

			if (y == null)
			{
				return x == null;
			}

			Boolean result = x.Name == y.Name;

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(IUnit obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			Int32 result = 539060726 + EqualityComparer<String>.Default.GetHashCode(obj.Name);

			return result;
		}
	}
}
