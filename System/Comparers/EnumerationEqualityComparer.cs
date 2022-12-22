using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Comparers
{
	/// <summary>
	/// Defines methods to support the comparison of enumerations for equality, taking into account their elements.
	/// </summary>
	/// <typeparam name="T">The type of element in enumerations to compare.</typeparam>
	public sealed class EnumerationEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
	{
		private EnumerationEqualityComparer() { }

		/// <summary>
		/// Instance of <see cref="EnumerationEqualityComparer{T}"/>.
		/// </summary>
		public static readonly EnumerationEqualityComparer<T> Instance = new EnumerationEqualityComparer<T>();

		/// <inheritdoc/>
		public Boolean Equals(IEnumerable<T> x, IEnumerable<T> y)
		{
			if (Object.ReferenceEquals(x, y))
			{
				return true;
			}
			else if (x == null || y == null)
			{
				return false;
			}

			Boolean result = true;
			var comparer = EqualityComparer<T>.Default;
			var xEnumerator = x.GetEnumerator();
			var yEnumerator = y.GetEnumerator();
			while (result)
			{
				Boolean xMoved = xEnumerator.MoveNext();
				Boolean yMoved = yEnumerator.MoveNext();

				if (xMoved && yMoved)
				{
					result = comparer.Equals(xEnumerator.Current, yEnumerator.Current);
				}
				else if (xMoved ^ yMoved)
				{
					result = false;
				}
				else
				{
					break;
				}
			}

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(IEnumerable<T> obj)
		{
			if (obj is null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			var hashCode = new HashCode();
			foreach (T item in obj)
			{
				hashCode.Add(item);
			}

			Int32 result = hashCode.ToHashCode();

			return result;
		}
	}
}
