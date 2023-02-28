using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides information on the capacity of the collection.
	/// </summary>
	/// <typeparam name="TSize">
	/// The type of size supported by the collection.
	/// </typeparam>
	public interface IHasCapacity<TSize>
	{
		/// <summary>
		/// Gets the resizeable capacity of the collection.
		/// </summary>
		TSize Capacity { get; }
	}

	/// <summary>
	/// Represents abuffered collection.
	/// </summary>
	/// <typeparam name="TSize">
	/// The type of size supported by the collection.
	/// </typeparam>
	public interface IBuffered<TSize>:IHasSize<TSize>, IHasCapacity<TSize>, IHasEnsureBuffer<TSize> { }
}
