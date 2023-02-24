using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Adapts the <see cref="IList{T}"/> interface onto interfaces found in the <c>RhoMicro.Common.System.Collections.Generic.Abstractions</c> namespace.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface IListAdapter<T> : IList<T>, ICollectionAdapter<T>, IHasIndexedGet<T>, IHasIndexedSet<T>, IHasIndexOf<T>, IHasRemoveAt<T>
	{

	}
}
