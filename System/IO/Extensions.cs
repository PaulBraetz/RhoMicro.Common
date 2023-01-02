using Fort;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
		/// Indicates wether or not to dispose <paramref name="next"/> after it has 
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
		/// Indicates wether or not to dispose <paramref name="next"/> after they have 
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
		/// Indicates wether or not to dispose <paramref name="next"/> after they have 
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
	}
}
