namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Represents a FIFO data structure containing elements of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of elements contained in the collection.</typeparam>
	public interface IQueue<T> : IReadOnlyCollectionAdapter<T>, IHasInsertLast<T>, IHasClear<T>, IHasContains<T>, IHasGetFirst<T>, IHasRemoveFirst<T>, IHasToArray<T>, IHasCopyTo<T>
	{

	}
}
