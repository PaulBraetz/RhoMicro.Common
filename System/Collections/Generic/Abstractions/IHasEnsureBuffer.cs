namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides method for ensuring the buffer underlying the collection provide empty space of a specified size.
	/// </summary>
	/// <typeparam name="TSize">
	/// The type of size supported by the collection.
	/// </typeparam>
	public interface IHasEnsureBuffer<TSize>
	{
		/// <summary>
		/// Ensures the underlying buffer provide empty space of the size specified.
		/// </summary>
		/// <param name="capacity">
		/// The size of the empty space to be provided by the buffer.
		/// </param>
		void EnsureBuffer(TSize capacity);
	}
}
