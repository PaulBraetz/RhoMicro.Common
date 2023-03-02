using RhoMicro.Common.System.Abstractions;
using System;

namespace RhoMicro.Common.System
{
	/// <summary>
	/// An observable base class for types implementing <see cref="IObservableDisposable"/>.
	/// </summary>
	public class ObservableDisposable : DisposableBase, IObservableDisposable
	{
		/// <inheritdoc/>
		public event EventHandler Disposing;
		/// <inheritdoc/>
		public event EventHandler Disposed;

		/// <summary>
		/// Invokes the <see cref="Disposing"/> event.
		/// </summary>
		/// <remarks>
		/// <i>From <see cref="DisposableBase.OnDiposing"/>:</i><br/>
		/// <inheritdoc/>
		/// </remarks>
		protected override void OnDiposing()
		{
			Disposing?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Invokes the <see cref="Disposed"/> event.
		/// </summary>
		/// <remarks>
		/// <i>From <see cref="DisposableBase.OnDiposed"/>:</i><br/>
		/// <inheritdoc/>
		/// </remarks>
		protected override void OnDiposed()
		{
			Disposed?.Invoke(this, EventArgs.Empty);
		}
	}
}
