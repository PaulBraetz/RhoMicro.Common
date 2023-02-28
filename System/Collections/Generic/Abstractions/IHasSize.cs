using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides information on the size of the collection.
	/// </summary>
	/// <typeparam name="TSize">
	/// The type of size supported by the collection.
	/// </typeparam>
	public interface IHasSize<out TSize>
	{
		/// <summary>
		/// Gets current size of the collection.
		/// </summary>
		TSize Size { get; }
	}
	/// <summary>
	/// Provides information on the size of the collection.
	/// </summary>
	public interface IHasSize : IHasSize<Int32>
	{
	}
}
