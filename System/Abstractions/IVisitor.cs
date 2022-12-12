using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Represents an implementation of the visitor pattern.
	/// </summary>
	/// <typeparam name="T">The type of objects to visit.</typeparam>
	public interface IVisitor<T>
	{
		/// <summary>
		/// Visits an instance of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="obj">The instance to visit.</param>
		void Visit(T obj);
	}
}
