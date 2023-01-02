using Fort;
using RhoMicro.Common.System.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System
{
	/// <summary>
	/// Strategy-based implementation of <see cref="IFactory{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of object this factory can construct.</typeparam>
	public sealed class FactoryStrategy<T> : IFactory<T>
	{
		private readonly Func<T> _strategy;

		private FactoryStrategy(Func<T> strategy)
		{
			_strategy = strategy;
		}

		/// <summary>
		/// Creates a new strategy-based factory.
		/// </summary>
		/// <param name="strategy">The strategy to use when creating instances of <typeparamref name="T"/> using the factory returned.</param>
		/// <returns>A new factory for instances of <typeparamref name="T"/>, based on <paramref name="strategy"/>.</returns>
		public static IFactory<T> Create(Func<T> strategy)
		{
			strategy.ThrowIfDefault(nameof(strategy));

			var result = new FactoryStrategy<T>(strategy);

			return result;
		}

		/// <inheritdoc/>
		public T Create()
		{
			var result = _strategy.Invoke();

			return result;
		}
	}
}
