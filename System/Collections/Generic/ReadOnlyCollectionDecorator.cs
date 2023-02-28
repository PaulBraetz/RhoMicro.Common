using Fort;
using RhoMicro.Common.System.Collections.Generic.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic
{
	/// <summary>
	/// Default implementation of <see cref="IReadOnlyCollectionAdapter{T}"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TCollection">
	/// The type of collection to adapt.
	/// </typeparam>
	public class ReadOnlyCollectionAdapter<T, TCollection> : IReadOnlyCollectionAdapter<T>
		where TCollection : IReadOnlyCollection<T>
	{
		/// <summary>
		/// The underlying collection.
		/// </summary>
		protected TCollection BaseCollection;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public ReadOnlyCollectionAdapter(TCollection collection)
		{
			collection.ThrowIfNull(nameof(collection));

			BaseCollection = collection;
		}

		public Int32 Size => BaseCollection.Count;

		Int32 IReadOnlyCollection<T>.Count => BaseCollection.Count;
		
		public IEnumerator<T> GetEnumerator()
		{
			return BaseCollection.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)BaseCollection).GetEnumerator();
		}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
