using System;
using System.Collections.Generic;
using System.Net;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Adapts the <see cref="ICollection{T}"/> interface to interfaces found in the <c>RhoMicro.Common.System.Collections.Generic.Abstractions</c> namespace.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface ICollectionAdapter<T> : ICollection<T>, IHasSize<Int32>, IHasInsert<T>, IHasClear<T>, IHasContains<T>, IHasRemove<T>, IHasCopyTo<T>
	{

	}
}
