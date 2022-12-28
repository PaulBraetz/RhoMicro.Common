using System.Threading.Tasks;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Represents an asynchronous implementation of the visitor pattern.
	/// </summary>
	/// <typeparam name="T">The type of objects to visit.</typeparam>
	public interface IAsyncVisitor<T>
	{
		/// <summary>
		/// Visits an instance of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="obj">The instance to visit.</param>
		/// <returns>A task that will complete upon the visit ending.</returns>
		Task Visit(T obj);
	}
}
