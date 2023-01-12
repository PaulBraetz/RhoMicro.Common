using Fort;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RhoMicro.Common.System.IO
{
	/// <summary>
	/// Represents a stream that will only open upon being operated upon.
	/// </summary>
	public sealed class LazyStream<TStream> : Stream
		where TStream : Stream
	{
		/// <summary>
		/// Initializes a new instance of <see cref="LazyStream{TStream}"/>. When lazy initialization
		/// occurs, the specified stream factory is used.
		/// </summary>
		/// <param name="streamFactory">The fstream factory to use upon initialization of the underlying stream.</param>
		public LazyStream(Func<TStream> streamFactory)
		{
			streamFactory.ThrowIfDefault(nameof(streamFactory));

			_baseStreamFactory = streamFactory;
		}

		private Func<TStream> _baseStreamFactory;
		private TStream _baseStream;
		private Boolean _disposedValue;
		private readonly Object _syncRoot = new Object();

		/// <summary>
		/// Indicates wether or not <see cref="BaseStream"/> has been initialized.
		/// </summary>
		public Boolean IsStreamCreated { get; private set; }
		/// <summary>
		/// Gets the lazily initialized underlying stream of the current <see cref="LazyStream{TStream}"/> instance.
		/// </summary>
		public TStream BaseStream
		{
			get
			{
				lock (_syncRoot)
				{
					if (_disposedValue)
					{
						throw new ObjectDisposedException(typeof(LazyStream<TStream>).Name);
					}

					if (!IsStreamCreated)
					{
						_baseStream = _baseStreamFactory.Invoke();
						IsStreamCreated = true;
					}
				}

				return _baseStream;
			}
		}
		/// <summary>
		/// If <see cref="IsStreamCreated"/> evaluates to <see langword="true"/>, disposes of <see cref="BaseStream"/> and 
		/// sets <see cref="IsStreamCreated"/> to <see langword="false"/>.
		/// </summary>
		public void Reset()
		{
			if (IsStreamCreated)
			{
				lock (_syncRoot)
				{
					if (IsStreamCreated)
					{
						_baseStream.Dispose();
						_baseStream = null;
						IsStreamCreated = false;
					}
				}
			}
		}

		/// <inheritdoc/>
		protected override void Dispose(Boolean disposing)
		{
			if (!_disposedValue)
			{
				lock (_syncRoot)
				{
					if (!_disposedValue)
					{
						if (disposing && IsStreamCreated)
						{
							_baseStream.Dispose();
						}

						_disposedValue = true;
					}
				}
			}
		}

		/// <inheritdoc/>
		public override Int32 WriteTimeout { get => BaseStream.WriteTimeout; set => BaseStream.WriteTimeout = value; }

		/// <inheritdoc/>
		public override void WriteByte(Byte value)
		{
			BaseStream.WriteByte(value);
		}

		/// <inheritdoc/>
		public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
		{
			return BaseStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		/// <inheritdoc/>
		public override String ToString()
		{
			return BaseStream.ToString();
		}

		/// <inheritdoc/>
		public override Int32 ReadTimeout { get => BaseStream.ReadTimeout; set => BaseStream.ReadTimeout = value; }

		/// <inheritdoc/>
		public override Int32 ReadByte()
		{
			return BaseStream.ReadByte();
		}

		/// <inheritdoc/>
		public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
		{
			return BaseStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		/// <inheritdoc/>
		public override Object InitializeLifetimeService()
		{
			return BaseStream.InitializeLifetimeService();
		}

		/// <inheritdoc/>
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return BaseStream.FlushAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public override void EndWrite(IAsyncResult asyncResult)
		{
			BaseStream.EndWrite(asyncResult);
		}

		/// <inheritdoc/>
		public override Int32 EndRead(IAsyncResult asyncResult)
		{
			return BaseStream.EndRead(asyncResult);
		}

		/// <inheritdoc/>
		public override Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken cancellationToken)
		{
			return BaseStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		/// <inheritdoc/>
		public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback callback, Object state)
		{
			return BaseStream.BeginRead(buffer, offset, count, callback, state);
		}

		/// <inheritdoc/>
		public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback callback, Object state)
		{
			return BaseStream.BeginWrite(buffer, offset, count, callback, state);
		}

		/// <inheritdoc/>
		public override void Flush()
		{
			BaseStream.Flush();
		}

		/// <inheritdoc/>
		public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
		{
			return BaseStream.Read(buffer, offset, count);
		}

		/// <inheritdoc/>
		public override Int64 Seek(Int64 offset, SeekOrigin origin)
		{
			return BaseStream.Seek(offset, origin);
		}

		/// <inheritdoc/>
		public override void SetLength(Int64 value)
		{
			BaseStream.SetLength(value);
		}

		/// <inheritdoc/>
		public override void Write(Byte[] buffer, Int32 offset, Int32 count)
		{
			BaseStream.Write(buffer, offset, count);
		}

		/// <inheritdoc/>
		public override Boolean CanRead => BaseStream.CanRead;

		/// <inheritdoc/>
		public override Boolean CanSeek => BaseStream.CanSeek;

		/// <inheritdoc/>
		public override Boolean CanWrite => BaseStream.CanWrite;

		/// <inheritdoc/>
		public override Int64 Length => BaseStream.Length;

		/// <inheritdoc/>
		public override Int64 Position { get => BaseStream.Position; set => BaseStream.Position = value; }

		/// <inheritdoc/>
		public override Boolean CanTimeout => BaseStream.CanTimeout;
	}
}
