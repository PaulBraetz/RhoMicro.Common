using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Adapts the <see cref="IReadOnlyCollection{T}"/> interface to interfaces found in the <c>RhoMicro.Common.System.Collections.Generic.Abstractions</c> namespace.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IReadOnlyCollectionAdapter<out T> : IReadOnlyCollection<T>, IHasSize<Int32>
	{
	}
}
