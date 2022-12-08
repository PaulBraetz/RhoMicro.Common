using System;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// An observable interface for types implementing <see cref="IDisposable"/>.
	/// </summary>
	public interface IObservableDisposable : IDisposable
	{
		/// <summary>
		/// Invoked after the instance has been disposed successfully.
		/// </summary>
		/// <remarks>
		/// Exceptions thrown by this event will not affect the disposed state or the disposal process of the instance.
		/// </remarks>
		event EventHandler Disposing;
		/// <summary>
		/// Invoked before the instance has been disposed successfully.
		/// </summary>
		/// <remarks>
		/// Exceptions thrown by this event will not affect the disposed state or the disposal process of the instance.
		/// </remarks>
		event EventHandler Disposed;
	}
}