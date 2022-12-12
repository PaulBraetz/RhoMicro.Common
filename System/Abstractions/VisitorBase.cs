using System;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Base class for types implementing <see cref="IVisitor{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of objects to visit.</typeparam>
	public abstract class VisitorBase<T> : IVisitor<T>
	{
		/// <summary>
		/// Evaluates wether or not a given object may be received by the visitor.
		/// </summary>
		/// <param name="obj">The object to check.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> may be received; otherwise, <see langword="false"/>.</returns>
		protected virtual Boolean CanReceive(T obj)
		{
			return true;
		}
		/// <summary>
		/// Receives an object.
		/// </summary>
		/// <param name="obj">The object to receive.</param>
		protected abstract void Receive(T obj);

		/// <inheritdoc/>
		public void Visit(T obj)
		{
			if (CanReceive(obj))
			{
				Receive(obj);
			}
		}

		/// <summary>
		/// Creates a new strategy-based visitor for objects of type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="receiveStrategy">The strategy to invoke when visiting instances of <typeparamref name="T"/>.</param>
		/// <param name="canReceiveStrategy">The strategy to invoke when checking wether or not an instance of <typeparamref name="T"/> may be visited.</param>
		/// <returns>A new instance of <see cref="IVisitor{T}"/>, based on the strategies provided.</returns>
		public static IVisitor<T> Create(Action<T> receiveStrategy, Func<T, Boolean> canReceiveStrategy = null)
		{
			var result = new VisitorStrategy<T>(receiveStrategy, canReceiveStrategy);

			return result;
		}
	}
}
