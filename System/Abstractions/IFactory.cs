using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Represents an implementation of the factory pattern.
	/// </summary>
	/// <typeparam name="T">The type of object this factory can construct.</typeparam>
	public interface IFactory<T>
	{
		/// <summary>
		/// Creates a new instance of <typeparamref name="T"/>.
		/// </summary>
		/// <returns>A new instance of <typeparamref name="T"/>.</returns>
		T Create();
	}
}
