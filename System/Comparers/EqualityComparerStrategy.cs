using Fort;
using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Comparers
{
	/// <summary>
	/// Strategy-based equality comparer.
	/// </summary>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	public sealed class EqualityComparerStrategy<T> : IEqualityComparer<T>
	{
		private readonly Func<T, T, Boolean> _equalsStrategy;
		private readonly Func<T, Int32> _hashCodeStrategy;

		private EqualityComparerStrategy(Func<T, T, Boolean> equalsStrategy, Func<T, Int32> hashCodeStrategy)
		{
			_equalsStrategy = equalsStrategy;
			_hashCodeStrategy = hashCodeStrategy;
		}

		/// <summary>
		/// Creates a new instance with the strategies provided.
		/// </summary>
		/// <param name="equalsStrategy">The strategy to use when comparing objects for equality.</param>
		/// <param name="hashCodeStrategy">The strategy to use when determining a hashcode.</param>
		public static IEqualityComparer<T> Create(Func<T, T, Boolean> equalsStrategy, Func<T, Int32> hashCodeStrategy)
		{
			equalsStrategy.ThrowIfNull(nameof(equalsStrategy));
			hashCodeStrategy.ThrowIfNull(nameof(hashCodeStrategy));

			var result = new EqualityComparerStrategy<T>(equalsStrategy, hashCodeStrategy);

			return result;
		}
		/// <summary>
		/// Creates a new instance with the strategies provided. Objects are compared based on a selected identifying feature.
		/// </summary>
		/// <param name="keySelector">Selects an objects identifying feature for equality comparison.</param>
		/// <param name="keyComparer">The comparer used when comparing identifying features.</param>
		public static IEqualityComparer<T> Create<TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
		{
			keySelector.ThrowIfNull(nameof(keySelector));
			keyComparer.ThrowIfNull(nameof(keyComparer));

			var result = new EqualityComparerStrategy<T>(
				(x, y) => keyComparer.Equals(keySelector.Invoke(x), keySelector.Invoke(y)),
				(obj) => keyComparer.GetHashCode(keySelector.Invoke(obj)));

			return result;
		}
		/// <summary>
		/// Creates a new instance with the strategies provided. Objects are compared based on a selected identifying feature.
		/// </summary>
		/// <remarks>
		/// The default equality comparer for <typeparamref name="TKey"/> will be used.
		/// </remarks>
		/// <param name="keySelector">Selects an objects identifying feature for equality comparison.</param>
		public static IEqualityComparer<T> Create<TKey>(Func<T, TKey> keySelector)
		{
			keySelector.ThrowIfNull(nameof(keySelector));

			var result = Create(keySelector, EqualityComparer<TKey>.Default);

			return result;
		}

		/// <inheritdoc/>
		public Boolean Equals(T x, T y)
		{
			var result = _equalsStrategy.Invoke(x, y);

			return result;
		}

		/// <inheritdoc/>
		public Int32 GetHashCode(T obj)
		{
			var result = _hashCodeStrategy.Invoke(obj);

			return result;
		}
	}
}
