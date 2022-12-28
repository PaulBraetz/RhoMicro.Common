using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace RhoMicro.Common.System.IO
{
	public sealed class StreamQueue : Stream
	{
		public StreamQueue(params Stream[] streams)
		{
			foreach (var stream in streams)
			{
				if (stream == null || !stream.CanRead)
				{
					throw new ArgumentException($"{nameof(streams)} must only not-null, read-enabled streams.", nameof(streams));
				}

				_queue.Enqueue(stream);
				Length += stream.Length;
			}
		}
		private readonly ConcurrentQueue<Stream> _queue = new ConcurrentQueue<Stream>();
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
			
			//TODO: implement

			var read = item.Read(buffer, offset, count);
			while (read == 0)
			{
				if(_queue.TryDequeue(out var _))
				{
					item.Dispose();
				}
				if (_queue.Count == 0) return 0;

				read = streams.Peek().Read(buffer, offset, count);
			} // if

			position += read;

			return read;
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
