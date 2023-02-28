using Fort;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RhoMicro.Common.System
{
	/// <summary>
	/// Base class for types implementing <see cref="IDisposable"/>.
	/// </summary>
	public abstract class DisposableBase : IDisposable, IAsyncDisposable
	{
		#region Disposed States
		private const Int32 NOT_DISPOSED = 0;
		private const Int32 DISPOSING = 1;
		private const Int32 DISPOSED = 2;
		#endregion

		private Int32 _disposedValue;

		/// <summary>
		/// Indicates whether or not the instance has been disposed of.
		/// </summary>
		protected Boolean IsDisposed => _disposedValue == DISPOSED;

		private void Dispose(Boolean disposing)
		{
			if (Interlocked.CompareExchange(ref _disposedValue, NOT_DISPOSED, DISPOSING) == NOT_DISPOSED)
			{
				try
				{
					OnDiposing();
				}
				catch
				{
					try
					{
						if (disposing)
						{
							DisposeManaged(disposing);
						}

						DisposeUnmanaged(disposing);

						_disposedValue = DISPOSED;
					}
					catch
					{
						_disposedValue = NOT_DISPOSED;

						throw;
					}
					
					throw;
				}

				OnDiposed();
			}
		}
		/// <summary>
		/// Invoked before the instance has been disposed successfully.
		/// </summary>
		/// <remarks>
		/// Exceptions thrown by this method will neither be handled nor affect the disposed state or the disposal process of the instance.
		/// </remarks>
		protected virtual void OnDiposing() { }
		/// <summary>
		/// Invoked after the instance has been disposed successfully.
		/// </summary>
		/// <remarks>
		/// Exceptions thrown by this method will neither be handled nor affect the disposed state or the disposal process of the instance.
		/// </remarks>
		protected virtual void OnDiposed() { }

		/// <inheritdoc/>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		/// <summary>
		/// Disposes the instance, calling <see cref="DisposeUnmanaged"/>, but not <see cref="DisposeManaged"/>.
		/// </summary>
		/// <remarks>
		/// Only call this method in the instance finalizer, as any exceptions thrown within will be swallowed.
		/// </remarks>
		protected void FinalizeDispose()
		{
			try
			{
				Dispose(disposing: false);
			}
			catch
			{
				//Disposal shall not throw in finalizer.
			}
		}

		/// <summary>
		/// Disposes managed state (managed objects).
		/// </summary>
		/// <remarks>
		/// Exceptions thrown by this method will not be handled and will cause the disposed state of the instance be reset to "<c>not disposed</c>".
		/// </remarks>
		/// <param name="disposing">Will set to <see langword="true"/> when disposing of the instance using <see cref="IDisposable.Dispose"/>; otherwise, <see langword="false"/>.</param>
		protected virtual void DisposeManaged(Boolean disposing) { }
		/// <summary>
		/// Disposes unmanaged state (unmanaged objects) and sets large fields to null.
		/// </summary>
		/// <remarks>
		/// When overriding this method, the finalizer should be overridden, calling <see cref="FinalizeDispose"/>. <br/>
		/// Exceptions thrown by this method will not be handled and will cause the disposed state of the instance be reset to "<c>not disposed</c>".
		/// </remarks>
		/// <param name="disposing">Will set to <see langword="true"/> when disposing of the instance using <see cref="IDisposable.Dispose"/>; otherwise, <see langword="false"/>.</param>
		protected virtual void DisposeUnmanaged(Boolean disposing) { }

		/// <summary>
		/// Throws an exception if <see cref="IsDisposed"/> evaluates to <see langword="true"/>.
		/// </summary>
		/// <param name="exception">The exception to throw.</param>
		protected void ThrowIfDisposed(Exception exception)
		{
			exception.ThrowIfDefault(nameof(exception));

			if (IsDisposed)
			{
				throw exception;
			}
		}
		/// <summary>
		/// Throws an <see cref="ObjectDisposedException"/> if <see cref="IsDisposed"/> evaluates to <see langword="true"/>.
		/// </summary>
		/// <param name="objectName">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception. If innerException is not null, 
		/// the current exception is raised in a catch block that handles the inner exception.
		/// </param>
		protected void ThrowIfDisposed(String objectName = null, Exception innerException = null)
		{
			var exception = new ObjectDisposedException(objectName, innerException);
			ThrowIfDisposed(exception);
		}
		/// <summary>
		/// Throws an <see cref="ObjectDisposedException"/> if <see cref="IsDisposed"/> evaluates to <see langword="true"/>.
		/// </summary>
		/// <param name="objectName">The name of the disposed object.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		protected void ThrowIfDisposed(String objectName = null, String message = null)
		{
			var exception = new ObjectDisposedException(objectName, message);
			ThrowIfDisposed(exception);
		}
		/// <summary>
		/// Throws an <see cref="ObjectDisposedException"/> if <see cref="IsDisposed"/> evaluates to <see langword="true"/>.
		/// </summary>
		/// <param name="objectName">The name of the disposed object.</param>
		protected void ThrowIfDisposed(String objectName = null)
		{
			var exception = new ObjectDisposedException(objectName);
			ThrowIfDisposed(exception);
		}

		/// <inheritdoc/>
		public virtual ValueTask DisposeAsync()
		{
			Dispose();

			return new ValueTask(Task.CompletedTask);
		}
	}
}