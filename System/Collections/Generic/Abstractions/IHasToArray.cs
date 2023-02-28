using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a function for yielding the elements contained in the collection as an array.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasToArray<out T>:IEnumerable<T>
	{
		/// <summary>
		/// Yields the elements contained in the collection as an array.
		/// </summary>
		/// <returns>
		/// The elements contained in the collection as an array.
		/// </returns>
		T[] ToArray();
	}
}
