﻿using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Adapts the <see cref="IList{T}"/> interface to interfaces found in the <c>RhoMicro.Common.System.Collections.Generic.Abstractions</c> namespace.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IListAdapter<T> : IList<T>, ICollectionAdapter<T>, IHasIndexedGetter<T, Int32>, IHasIndexedSetter<T, Int32>, IHasGetIndexOf<T, Int32>, IHasInsertAt<T, Int32>, IHasRemoveAt<T, Int32>
	{

	}
}
