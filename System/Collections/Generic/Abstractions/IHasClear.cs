using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a method for clearing all elements from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasClear<out T>:IEnumerable<T>
	{
		/// <summary>
		/// Clears all elements from the collection.
		/// </summary>
		void Clear();
	}
}
