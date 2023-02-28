using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Represents a LIFO data structure containing elements of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of elements contained in the collection.</typeparam>
	public interface IStack<T> : IReadOnlyCollection<T>, IHasInsertFirst<T>, IHasClear<T>, IHasContains<T>, IHasGetFirst<T>, IHasRemoveFirst<T>, IHasToArray<T>, IHasCopyTo<T>
	{

	}
}
