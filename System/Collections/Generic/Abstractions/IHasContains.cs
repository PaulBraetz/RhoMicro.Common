using System;
using System.Collections;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a function for checking whether an element is contained in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasContains<T> 
	{
		/// <summary>
		/// Checks whether an element is contained in the collection.
		/// </summary>
		/// <param name="element">
		/// The element to check for.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="element"/> exists in the collection; otherwise, <see langword="false"/>.
		/// </returns>
		Boolean Contains(T element);
	}
}
