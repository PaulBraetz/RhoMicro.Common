using Fort;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace RhoMicro.Common.System.IO
{
	/// <summary>
	/// Represents a read-only stream composed of queued streams.
	/// </summary>
	public sealed class StreamReadQueue : Stream
	{
		/// <summary>
		/// Initializes a new instance with the streams provided.
		/// </summary>
		/// <param name="streams">The streams to enqueue.</param>
		/// <param name="manageLifetime">Indicates whether or not to dispose <paramref name="streams"/> after they have been fully read or when this instance is being disposed.</param>
		public StreamReadQueue(IEnumerable<Stream> streams, Boolean manageLifetime = false)
		{
			streams.ThrowIfDefault(nameof(streams));

			foreach (var stream in streams)
			{
				stream.ThrowIfDefaultOrNot(s => s.CanRead, $"{nameof(stream)} must be read-enabled.", nameof(stream));
				Length += stream.Length;
				_streams.Enqueue(stream);
			}

			_manageLifetime = manageLifetime;
		}

		private readonly Boolean _manageLifetime;
		private readonly Queue<Stream> _streams = new Queue<Stream>();
		private readonly Object _syncLock = new Object();

		private Int64 position;

		/// <inheritdoc/>
		public override Boolean CanRead => true;
		/// <inheritdoc/>
		public override Boolean CanSeek => false;
		/// <inheritdoc/>
		public override Boolean CanWrite => false;
		/// <inheritdoc/>
		public override Int64 Length { get; }
		/// <inheritdoc/>
		public override Int64 Position { get => position; set => throw new NotSupportedException(); }

		/// <inheritdoc/>
		public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
		{
			lock (_syncLock)
			{
				if (_disposedValue)
				{
					throw new ObjectDisposedException(nameof(StreamReadQueue));
				}

				Int32 read = 0;
				while (peekRead())
				{
					if (_manageLifetime)
					{
						_streams.Dequeue().Dispose();
					}
					else
					{
						_streams.Dequeue();
					}
				}

				position += read;

				return read;

				Boolean peekRead()
				{
					if (_streams.Count == 0)
					{
						return false;
					}

					var newOffset = offset + read;
					var newCount = count - read;
					var thisRead = _streams.Peek().Read(buffer, newOffset, newCount);
					read += thisRead;

					return thisRead < newCount;
				}
			}
		}

		private Boolean _disposedValue;
		/// <inheritdoc/>
		protected override void Dispose(Boolean disposing)
		{
			if (_manageLifetime)
			{
				lock (_syncLock)
				{
					base.Dispose(disposing);
					if (disposing)
					{
						while (_streams.Count > 0)
						{
							_streams.Dequeue().Dispose();
						}

						_disposedValue = true;
					}
				}
			}
		}

		/// <inheritdoc/>
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc/>
		public override Int64 Seek(Int64 offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc/>
		public override void SetLength(Int64 value)
		{
			throw new NotSupportedException();
		}

		/// <inheritdoc/>
		public override void Write(Byte[] buffer, Int32 offset, Int32 count)
		{
			throw new NotSupportedException();
		}
	}
}
