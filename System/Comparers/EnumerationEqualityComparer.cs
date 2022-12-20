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
			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			IEnumerator<T> xEnumerator = x.GetEnumerator();
			IEnumerator<T> yEnumerator = y.GetEnumerator();
			Boolean andMoved = true;
			while (andMoved)
			{
				Boolean xMoved = xEnumerator.MoveNext();
				Boolean yMoved = yEnumerator.MoveNext();
				andMoved = xMoved && yMoved;
				Boolean xnorMoved = !(xMoved ^ yMoved);

				result = xnorMoved && (!andMoved || comparer.Equals(xEnumerator.Current, yEnumerator.Current));
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
