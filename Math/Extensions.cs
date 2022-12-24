using Fort;
using RhoMicro.Common.Math.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Xsl;

namespace RhoMicro.Common.Math
{
	/// <summary>
	/// Provides extension methods for types found in the Math namespace.
	/// </summary>
	public static partial class Extensions
	{
		/// <summary>
		/// Returns wether or not a bounded interval is empty, using <see cref="EqualityComparer{T}.Default"/>.
		/// </summary>
		/// <remarks>
		/// A given bounded interval is defined as empty when its left and right bound are equal and it is not left- and right-closed.
		/// </remarks>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> is empty; otherwise, <see langword="false"/>.</returns>
		public static Boolean IsEmpty<T>(this IBoundedInterval<T> interval)
		{
			interval.ThrowIfDefault(nameof(interval));

			var comparer = EqualityComparer<T>.Default;
			var result = interval.IsEmpty(comparer);

			return result;
		}
		/// <summary>
		/// Returns wether or not a bounded interval is empty.
		/// </summary>
		/// <remarks>
		/// A given bounded interval is defined as empty when its left and right bound are equal and it is not left- and right-closed.
		/// </remarks>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <param name="comparer">The comparer to use when comparing the left and right bound of <paramref name="interval"/>.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> is empty; otherwise, <see langword="false"/>.</returns>
		public static Boolean IsEmpty<T>(this IBoundedInterval<T> interval, IEqualityComparer<T> comparer)
		{
			comparer.ThrowIfDefault(nameof(comparer));
			interval.ThrowIfDefault(nameof(interval));

			var result = comparer.Equals(interval.LeftBound, interval.RightBound) && !(interval.LeftClosed && interval.RightClosed);

			return result;
		}

		/// <summary>
		/// Returns wether or not a bounded interval is degenerate, that is, walking it would yield exactly one element, using <see cref="EqualityComparer{T}.Default"/>.
		/// </summary>
		/// <remarks>
		/// A given bounded interval is defined as degenerate when its left and right bound are equal and it is left- and right-closed.
		/// </remarks>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> is degenerate; otherwise, <see langword="false"/>.</returns>
		public static Boolean IsDegenerate<T>(this IBoundedInterval<T> interval)
		{
			interval.ThrowIfDefault(nameof(interval));

			var comparer = EqualityComparer<T>.Default;
			var result = interval.IsDegenerate(comparer);

			return result;
		}
		/// <summary>
		/// Returns wether or not a bounded interval is degenerate, that is, walking it would yield exactly one element.
		/// </summary>
		/// <remarks>
		/// A given bounded interval is defined as degenerate when its left and right bound are equal and it is left- and right-closed.
		/// </remarks>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <param name="comparer">The comparer to use when comparing the left and right bound of <paramref name="interval"/>.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> is degenerate; otherwise, <see langword="false"/>.</returns>
		public static Boolean IsDegenerate<T>(this IBoundedInterval<T> interval, IEqualityComparer<T> comparer)
		{
			comparer.ThrowIfDefault(nameof(comparer));
			interval.ThrowIfDefault(nameof(interval));

			var result = comparer.Equals(interval.LeftBound, interval.RightBound) && interval.LeftClosed && interval.RightClosed;

			return result;
		}

		/// <summary>
		/// Binds a right-bounded interval to a left bound.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to bind</param>
		/// <param name="leftBound">The intervals left (usually lower) bound.</param>
		/// <param name="leftClosed">Indicates wether or not <paramref name="leftBound"/> is to be included in the interval. The default is <see langword="true"/>.</param>
		/// <returns>A new left- and right-bounded interval.</returns>
		public static IBoundedInterval<T> BindLeft<T>(this IRightBoundedInterval<T> interval, T leftBound, Boolean leftClosed = true)
		{
			interval.ThrowIfDefault(nameof(interval));

			var result = BoundedInterval<T>.Create(leftBound, interval.RightBound, leftClosed, interval.RightClosed);

			return result;
		}
		/// <summary>
		/// Unbinds a bounded interval from its left bound.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to unbind.</param>
		/// <returns>A new left-unbounded and right-bounded interval.</returns>
		public static IRightBoundedInterval<T> UnbindLeft<T>(this IBoundedInterval<T> interval)
		{
			interval.ThrowIfDefault(nameof(interval));

			var result = RightBoundedInterval<T>.Create(interval.RightBound, interval.RightClosed);

			return result;
		}

		/// <summary>
		/// Binds a left-bounded interval to a right bound.
		/// </summary>
		/// <param name="interval">The interval to bind</param>
		/// <param name="rightBound">The intervals right (usually upper) bound.</param>
		/// <param name="rightClosed">Indicates wether or not <paramref name="rightBound"/> is to be included in the interval. The default is <see langword="false"/>.</param>
		/// <returns>A new left- and right-bounded interval.</returns>
		public static IBoundedInterval<T> BindRight<T>(this ILeftBoundedInterval<T> interval, T rightBound, Boolean rightClosed = false)
		{
			interval.ThrowIfDefault(nameof(interval));

			var result = BoundedInterval<T>.Create(interval.LeftBound, rightBound, interval.LeftClosed, rightClosed);

			return result;
		}
		/// <summary>
		/// Unbinds a bounded interval from its right bound.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to unbind.</param>
		/// <returns>A new left-bounded and right-unbounded interval.</returns>
		public static ILeftBoundedInterval<T> UnbindRight<T>(this IBoundedInterval<T> interval)
		{
			interval.ThrowIfDefault(nameof(interval));

			var result = LeftBoundedInterval<T>.Create(interval.LeftBound, interval.LeftClosed);

			return result;
		}

		/// <summary>
		/// Walks a bounded interval using a walker function until the intervals right bound has been reached.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The bounded interval to walk.</param>
		/// <param name="walker">The walker, which will be passed the last element and calculate from it the next element.</param>
		/// <param name="comparer">The comparer to be used for determining wether the intervals right bound has been reached.</param>
		/// <returns>An enumerable, enumerating the values yielded by <paramref name="walker"/> while walking <paramref name="interval"/>.</returns>
		public static IEnumerable<T> Walk<T>(this IBoundedInterval<T> interval, Func<T, T> walker, IEqualityComparer<T> comparer)
		{
			interval.ThrowIfDefault(nameof(interval));
			walker.ThrowIfDefault(nameof(walker));
			comparer.ThrowIfDefault(nameof(comparer));

			if (interval.IsDegenerate(comparer))
			{
				yield return interval.LeftBound;
			}
			else if (!interval.IsEmpty(comparer))
			{
				var result = interval.LeftBound;
				yield return result;
				do
				{
					result = walker.Invoke(result);

					yield return result;
				} while (!comparer.Equals(result, interval.RightBound));
			}
		}
		/// <summary>
		/// Walks a bounded interval using a walker function until the intervals right bound has been reached, using <see cref="EqualityComparer{T}.Default"/>.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The bounded interval to walk.</param>
		/// <param name="walker">The walker, which will be passed the last element and calculate from it the next element.</param>
		/// <returns>An enumerable, enumerating the values yielded by <paramref name="walker"/> while walking <paramref name="interval"/>.</returns>
		public static IEnumerable<T> Walk<T>(this IBoundedInterval<T> interval, Func<T, T> walker)
		{
			interval.ThrowIfDefault(nameof(interval));
			walker.ThrowIfDefault(nameof(walker));

			var result = interval.Walk(walker, EqualityComparer<T>.Default);

			return result;
		}

		/// <summary>
		/// Returns wether or not a given left-bounded interval contains a value.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <param name="value">The value to check for.</param>
		/// <param name="comparer">The comparer to use for determining the order of values represented by the interval.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> contains <paramref name="value"/>; otherwise, <see langword="false"/>.</returns>
		public static Boolean Contains<T>(this ILeftBoundedInterval<T> interval, T value, IComparer<T> comparer)
		{
			interval.ThrowIfDefault(nameof(interval));
			value.ThrowIfDefault(nameof(value));
			comparer.ThrowIfDefault(nameof(comparer));

			var result = comparer.Compare(value, interval.LeftBound) >= (interval.LeftClosed ? 0 : 1);

			return result;
		}
		/// <summary>
		/// Returns wether or not a given left-bounded interval contains a value.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <param name="value">The value to check for.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> contains <paramref name="value"/>; otherwise, <see langword="false"/>.</returns>
		public static Boolean Contains<T>(this ILeftBoundedInterval<T> interval, T value)
		{
			interval.ThrowIfDefault(nameof(interval));
			value.ThrowIfDefault(nameof(value));

			var result = interval.Contains(value, Comparer<T>.Default);

			return result;
		}

		/// <summary>
		/// Returns wether or not a given right-bounded interval contains a value.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <param name="value">The value to check for.</param>
		/// <param name="comparer">The comparer to use for determining the order of values represented by the interval.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> contains <paramref name="value"/>; otherwise, <see langword="false"/>.</returns>
		public static Boolean Contains<T>(this IRightBoundedInterval<T> interval, T value, IComparer<T> comparer)
		{
			interval.ThrowIfDefault(nameof(interval));
			value.ThrowIfDefault(nameof(value));
			comparer.ThrowIfDefault(nameof(comparer));

			var result = comparer.Compare(value, interval.RightBound) <= (interval.RightClosed ? 0 : 1);

			return result;
		}
		/// <summary>
		/// Returns wether or not a given right-bounded interval contains a value.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <param name="value">The value to check for.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> contains <paramref name="value"/>; otherwise, <see langword="false"/>.</returns>
		public static Boolean Contains<T>(this IRightBoundedInterval<T> interval, T value)
		{
			interval.ThrowIfDefault(nameof(interval));
			value.ThrowIfDefault(nameof(value));

			var result = interval.Contains(value, Comparer<T>.Default);

			return result;
		}

		/// <summary>
		/// Returns wether or not a given left- and right-bounded interval contains a value.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <param name="value">The value to check for.</param>
		/// <param name="comparer">The comparer to use for determining the order of values represented by the interval.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> contains <paramref name="value"/>; otherwise, <see langword="false"/>.</returns>
		public static Boolean Contains<T>(this IBoundedInterval<T> interval, T value, IComparer<T> comparer)
		{
			interval.ThrowIfDefault(nameof(interval));
			value.ThrowIfDefault(nameof(value));
			comparer.ThrowIfDefault(nameof(comparer));

			var result =
				comparer.Compare(value, interval.LeftBound) >= (interval.LeftClosed ? 0 : 1) ||
				comparer.Compare(value, interval.RightBound) <= (interval.RightClosed ? 0 : 1);

			return result;
		}
		/// <summary>
		/// Returns wether or not a given left- and right-bounded interval contains a value.
		/// </summary>
		/// <typeparam name="T">The type of values represented by the interval.</typeparam>
		/// <param name="interval">The interval to examine.</param>
		/// <param name="value">The value to check for.</param>
		/// <returns><see langword="true"/> if <paramref name="interval"/> contains <paramref name="value"/>; otherwise, <see langword="false"/>.</returns>
		public static Boolean Contains<T>(this IBoundedInterval<T> interval, T value)
		{
			interval.ThrowIfDefault(nameof(interval));
			value.ThrowIfDefault(nameof(value));

			var result = interval.Contains(value, Comparer<T>.Default);

			return result;
		}
	}
}
