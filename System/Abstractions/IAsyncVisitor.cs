using System.Threading;
using System.Threading.Tasks;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Represents an asynchronous implementation of the visitor pattern.
	/// </summary>
	/// <typeparam name="T">The type of objects to visit.</typeparam>
	public interface IAsyncVisitor<in T>
	{
		/// <summary>
		/// Visits an instance of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="obj">The instance to visit.</param>
		/// <param name="cancellationToken">Token to signalize the visit to exit.</param>
		/// <returns>A task that will complete upon the visit ending.</returns>
		Task VisitAsync(T obj, CancellationToken cancellationToken= default);
	}
}
