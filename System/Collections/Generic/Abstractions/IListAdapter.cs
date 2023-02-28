using System;
using System.Collections.Generic;
using static RhoMicro.Common.System.Collections.Generic.Abstractions.At;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Adapts the <see cref="IList{T}"/> interface to interfaces found in the <c>RhoMicro.Common.System.Collections.Generic.Abstractions</c> namespace.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IListAdapter<T> : IList<T>, ICollectionAdapter<T>, IHasIndexedGetter<T>, IHasIndexedSetter<T>, IHasGetIndexOf<T, Int32>, IHasInsertAt<T, Int32>, IHasRemoveAt<T, Int32>
	{

	}
}
