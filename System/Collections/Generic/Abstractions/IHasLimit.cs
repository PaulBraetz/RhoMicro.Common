using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides information on the limit of the collection.
	/// </summary>
	/// <typeparam name="TSize">
	/// The type of size supported by the collection.
	/// </typeparam>
	public interface IHasLimit<out TSize>
	{
		/// <summary>
		/// Gets the non-resizable limit of the collection.
		/// </summary>
		TSize Limit { get; }
	}
	/// <summary>
	/// Provides information on the limit of the collection.
	/// </summary>
	public interface IHasLimit: IHasLimit<Int32>
	{
	}
}
