using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a method for copying the elements contained in the collection to an array.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasCopyTo<T> : IEnumerable<T>
	{
		/// <summary>
		/// Copies the elements contained in the collection to an existing one-dimensional array, starting at the specified index.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional array that is the destination of the elements copied
		/// from the collection. The array must have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">
		/// The zero-based index in array at which copying begins.
		/// </param>
		void CopyTo(T[] array, Int32 arrayIndex);
	}
}
