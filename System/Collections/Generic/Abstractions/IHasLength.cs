using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides information on the length of the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	/// <typeparam name="TLength">
	/// The type of length supported by the collection.
	/// </typeparam>
	public interface IHasLength<out T, out TLength> : IEnumerable<T>
	{
		/// <summary>
		/// Gets length of the collection.
		/// </summary>
		TLength Length { get; }
	}
	/// <summary>
	/// Provides information on the length of the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IHasLength<out T> : IHasLength<T,Int32>
	{
	}
}
