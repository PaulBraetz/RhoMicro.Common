using RhoMicro.Common.System.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RhoMicro.Common.System
{
	/// <summary>
	/// Extensions for the System namespace.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Wraps an instance of <see cref="IVisitor{T}"/> as an <see cref="IAsyncVisitor{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of objects to visit.</typeparam>
		/// <param name="visitor">The visitor to wrap.</param>
		/// <param name="canVisitStrategy">The strategy to invoke when checking wether or not an instance of <typeparamref name="T"/> may be visited.</param>
		/// <returns>A new instance of <see cref="IAsyncVisitor{T}"/>.</returns>
		public static IAsyncVisitor<T> AsAsync<T>(this IVisitor<T> visitor, Func<T, CancellationToken, Task<Boolean>> canVisitStrategy = null)
		{
			var result = AsyncVisitorBase<T>.Create((obj, t) =>
			{
				t.ThrowIfCancellationRequested();
				visitor.Visit(obj);
				return Task.FromResult(true);
			}, canVisitStrategy);

			return result;
		}
	}
}
