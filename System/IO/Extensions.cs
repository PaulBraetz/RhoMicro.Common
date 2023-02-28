using Fort;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace RhoMicro.Common.System.IO
{
	/// <summary>
	/// Extensions for the System namespace.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Appends a stream to another.
		/// </summary>
		/// <param name="stream">The stream to append to.</param>
		/// <param name="next">The stream to append to <paramref name="stream"/>.</param>
		/// <param name="manageLifetime">
		/// Indicates whether or not to dispose <paramref name="next"/> after it has 
		/// been fully read or when the instance returned is being disposed.
		/// </param>
		/// <returns>
		/// A new stream that will first provide the bytes read from <paramref name="stream"/>, 
		/// followed by those read from <paramref name="next"/>.
		/// </returns>
		public static Stream Append(this Stream stream, Stream next, Boolean manageLifetime = false)
		{
			stream.ThrowIfDefault(nameof(stream));
			next.ThrowIfDefault(nameof(next));

			var result = new StreamReadQueue(new[] { stream, next }, manageLifetime);

			return result;
		}
		/// <summary>
		/// Appends streams to an other.
		/// </summary>
		/// <param name="stream">The stream to append to.</param>
		/// <param name="next">The streams to append to <paramref name="stream"/>.</param>
		/// <param name="manageLifetime">
		/// Indicates whether or not to dispose <paramref name="next"/> after they have 
		/// been fully read or when the instance returned is being disposed.
		/// </param>
		/// <returns>
		/// A new stream that will first provide the bytes read from <paramref name="stream"/>, 
		/// followed by those read from each of the streams provided by <paramref name="next"/>.
		/// </returns>
		public static Stream AppendRange(this Stream stream, IEnumerable<Stream> next, Boolean manageLifetime = false)
		{
			stream.ThrowIfDefault(nameof(stream));
			next.ThrowIfDefault(nameof(next));

			var result = next.Any() ?
				new StreamReadQueue(next.Prepend(stream), manageLifetime) :
				stream;

			return result;
		}
		/// <summary>
		/// Appends streams to an other.
		/// </summary>
		/// <param name="stream">The stream to append to.</param>
		/// <param name="next">The streams to append to <paramref name="stream"/>.</param>
		/// <param name="manageLifetime">
		/// Indicates whether or not to dispose <paramref name="next"/> after they have 
		/// been fully read or when the instance returned is being disposed.
		/// </param>
		/// <returns>
		/// A new stream that will first provide the bytes read from <paramref name="stream"/>, 
		/// followed by those read from each of the streams provided by <paramref name="next"/>.
		/// </returns>
		public static Stream AppendRange(this Stream stream, Boolean manageLifetime = false, params Stream[] next)
		{
			stream.ThrowIfDefault(nameof(stream));
			next.ThrowIfDefault(nameof(next));

			var result = next.Length > 0 ?
				new StreamReadQueue(next.Prepend(stream), manageLifetime) :
				stream;

			return result;
		}
		/// <summary>
		/// Reads a line of characters, including line break characters, from the text reader and returns the data as a string.
		/// </summary>
		/// <param name="reader">The reader to read characters from.</param>
		/// <returns>The next line from the reader, including line break characters, or null if all characters have been read.</returns>
		public static String ReadFullLine(this TextReader reader)
		{
			var resultBuilder = new StringBuilder();

			Int32 symbol = reader.Peek();
			if (symbol == -1)
			{
				return null;
			}

			while ((symbol = reader.Read()) != -1)
			{
				resultBuilder.Append((Char)symbol);
				if (symbol == '\n')
				{
					break;
				}
			}

			return resultBuilder.ToString();
		}

		/// <summary>
		/// Yields bytes returned by reading a stream.
		/// </summary>
		/// <remarks>
		/// <paramref name="stream"/> will be disposed upon there being no more bytes to read.
		/// </remarks>
		/// <param name="stream">The stream whoise bytes to yield.</param>
		/// <param name="bufferSize">The size of the buffer used when reading bytes.</param>
		/// <param name="yieldIncomplete">When <see langword="true"/>, will yield buffers with length <paramref name="bufferSize"/>; otherwise, it is possible to infinitely attempt to read the stream.</param>
		/// <param name="cancellationToken">Should be used for instances in which the stream is fully enumerated, as only full buffers will be yielded while the stream can be read from.</param>
		/// <returns>An enumeration of bytes read from the stream.</returns>
		public static IEnumerable<Byte[]> AsEnumerable(this Stream stream,
												 Int32 bufferSize,
												 Boolean yieldIncomplete = true,
												 CancellationToken cancellationToken = default)
		{
			stream.ThrowIfDefault(nameof(stream));
			bufferSize.ThrowIfNot(s => s > 0, $"{nameof(bufferSize)} must be > 0.", nameof(bufferSize));

			using (stream)
			{
				Byte[] buffer = new Byte[bufferSize];
				Int32 read = 0;
				while (stream.CanRead)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						break;
					}
					read += stream.Read(buffer, read, bufferSize - read);
					if (read == bufferSize)
					{
						yield return buffer;
						
						buffer = new Byte[bufferSize];
						read = 0;
					}else if(yieldIncomplete)
					{
						break;
					}
				}

				if (!cancellationToken.IsCancellationRequested && yieldIncomplete && read > 0)
				{
					Byte[] shortBuffer = new Byte[read];
					for (var i =0; i < shortBuffer.Length; i++)
					{
						shortBuffer[i] = buffer[i];
					}

					yield return shortBuffer;
				}
			}
		}


		/// <summary>
		/// Yields bytes returned by reading a stream.
		/// </summary>
		/// <remarks>
		/// <paramref name="stream"/> will be disposed upon there being no more bytes to read.
		/// </remarks>
		/// <param name="stream">The stream whoise bytes to yield.</param>
		/// <param name="bufferSize">The size of the buffer used when reading bytes.</param>
		/// <param name="yieldIncomplete">When <see langword="true"/>, will yield buffers with length <paramref name="bufferSize"/>; otherwise, it is possible to infinitely attempt to read the stream.</param>
		/// <param name="cancellationToken">Should be used for instances in which the stream is fully enumerated, as only full buffers will be yielded while the stream can be read from.</param>
		/// <returns>An enumeration of bytes read from the stream.</returns>
		public static async IAsyncEnumerable<Byte[]> AsAsyncEnumerable(this Stream stream,
																 Int32 bufferSize,
																 Boolean yieldIncomplete = true,
																 [EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			stream.ThrowIfDefault(nameof(stream));
			bufferSize.ThrowIfNot(s => s > 0, $"{nameof(bufferSize)} must be > 0.", nameof(bufferSize));

			using (stream)
			{
				Byte[] buffer = new Byte[bufferSize];
				Int32 read = 0;
				while (stream.CanRead)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						break;
					}
					read += await stream.ReadAsync(buffer, read, bufferSize - read, cancellationToken);
					if (read == bufferSize)
					{
						yield return buffer;

						buffer = new Byte[bufferSize];
						read = 0;
					}
					else if (yieldIncomplete)
					{
						break;
					}
				}

				if (!cancellationToken.IsCancellationRequested && yieldIncomplete && read > 0)
				{
					Byte[] shortBuffer = new Byte[read];
					for (var i = 0; i < shortBuffer.Length; i++)
					{
						shortBuffer[i] = buffer[i];
					}

					yield return shortBuffer;
				}
			}
		}
	}
}
