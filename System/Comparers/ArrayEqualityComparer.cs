using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Comparers
{
	/// <summary>
	/// Defines methods to support the comparison of arrays for equality, taking into account their elements.
	/// </summary>
	/// <typeparam name="T">The type of element in arrays to compare.</typeparam>
	public sealed class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
	{
		private ArrayEqualityComparer() { }

		/// <summary>
		/// Instance of <see cref="ArrayEqualityComparer{T}"/>.
		/// </summary>
		public static readonly ArrayEqualityComparer<T> Instance = new ArrayEqualityComparer<T>();

		/// <inheritdoc/>
		public Boolean Equals(T[] x, T[] y)
		{
			if (Object.ReferenceEquals(x, y))
			{
				return true;
			}
			else if (x == null || y == null)
			{
				return false;
			}

			Boolean result = x.Length == y.Length;
			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			for (Int32 i = 0; result && i < x.Length; i++)
			{
				if (!comparer.Equals(x[i], y[i]))
				{
					result = false;
				}
			}

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(T[] obj)
		{
			if (obj is null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			var hashCode = new HashCode();
			for (Int32 i = 0; i < obj.Length; i++)
			{
				hashCode.Add(obj[i]);
			}

			Int32 result = hashCode.ToHashCode();

			return result;
		}
	}
}
