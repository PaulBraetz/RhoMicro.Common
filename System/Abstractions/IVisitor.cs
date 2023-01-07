using System;
using System.Threading.Tasks;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Represents an implementation of the visitor pattern.
	/// </summary>
	/// <typeparam name="T">The type of objects to visit.</typeparam>
	public interface IVisitor<in T>
	{
		/// <summary>
		/// Visits an instance of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="obj">The instance to visit.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> was succesfully visited; otherwise, <see langword="false"/>.</returns>
		Boolean Visit(T obj);
	}
}
