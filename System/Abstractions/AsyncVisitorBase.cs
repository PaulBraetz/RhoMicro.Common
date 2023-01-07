using Fort;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Base class for types implementing <see cref="IAsyncVisitor{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of objects to visit.</typeparam>
	public abstract class AsyncVisitorBase<T> : IAsyncVisitor<T>
	{
		/// <summary>
		/// Evaluates wether or not a given object may be received by the visitor.
		/// </summary>
		/// <param name="obj">The object to check.</param>
		/// <param name="cancellationToken">Token to signalize the visit to exit.</param>
		/// <returns>A task that upon completion will contain <see langword="true"/> if <paramref name="obj"/> may be received; otherwise, <see langword="false"/>.</returns>
		protected virtual Task<Boolean> CanReceive(T obj, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(true);
		}
		/// <summary>
		/// Receives an object.
		/// </summary>
		/// <param name="obj">The object to receive.</param>
		/// <param name="cancellationToken">Token to signalize the visit to exit.</param>
		/// <returns>A task that will complete upon the visit ending.</returns>
		protected abstract Task Receive(T obj, CancellationToken cancellationToken = default);

		/// <inheritdoc/>
		public virtual async Task<Boolean> VisitAsync(T obj, CancellationToken cancellationToken = default)
		{
			var result = await CanReceive(obj, cancellationToken);
			if (result)
			{
				await Receive(obj, cancellationToken);
			}

			return result;
		}

		/// <summary>
		/// Creates a new strategy-based asynchronous visitor for objects of type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="receiveStrategy">The strategy to invoke when visiting instances of <typeparamref name="T"/>.</param>
		/// <param name="canReceiveStrategy">The strategy to invoke when checking wether or not an instance of <typeparamref name="T"/> may be visited.</param>
		/// <returns>A new instance of <see cref="IAsyncVisitor{T}"/>, based on the strategies provided.</returns>
		public static IAsyncVisitor<T> Create(Func<T, CancellationToken, Task> receiveStrategy, Func<T, CancellationToken, Task<Boolean>> canReceiveStrategy = null)
		{
			receiveStrategy.ThrowIfDefault(nameof(receiveStrategy));
			var result = new AsyncVisitorStrategy<T>(receiveStrategy, canReceiveStrategy);

			return result;
		}
		/// <summary>
		/// Creates a new strategy-based asynchronous visitor for objects of type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="visitStrategy">The strategy to invoke when visiting instances of <typeparamref name="T"/>.</param>
		/// <returns>A new instance of <see cref="IAsyncVisitor{T}"/>, based on the strategy provided.</returns>
		public static IAsyncVisitor<T> Create(Func<T, CancellationToken, Task<Boolean>> visitStrategy)
		{
			visitStrategy.ThrowIfDefault(nameof(visitStrategy));
			var result = new AsyncVisitorStrategy<T>(visitStrategy);

			return result;
		}
	}
}
