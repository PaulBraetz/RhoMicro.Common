using System.Collections.Generic;
using System.Net;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Adapts the <see cref="ICollection{T}"/> interface onto interfaces found in the <c>RhoMicro.Common.System.Collections.Generic.Abstractions</c> namespace.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements in the collection.
	/// </typeparam>
	public interface ICollectionAdapter<T> : ICollection<T>, IHasClear<T>, IHasAdd<T>, IHasTryRemoveFirst<T>, IHasContains<T>, IHasLength<T>
	{

	}

	public interface IAdapter:global::System.Collections.Generic.IReadOnlyCollection
}
