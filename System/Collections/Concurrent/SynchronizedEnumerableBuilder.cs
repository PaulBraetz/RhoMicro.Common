using Fort;
using RhoMicro.Common.Math;
using RhoMicro.Common.System.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Collections.Concurrent
{
	public sealed class SynchronizedEnumerableBuilder<T> : IBuilder<IEnumerable<T>>
	{
		public SynchronizedEnumerableBuilder(Int32 lastTurn, Boolean ordered = false)
		{
			_context = new SynchronizedEnumeratorContext(lastTurn, ordered);
			Reset();
		}

		private readonly SynchronizedEnumeratorContext _context;

		public IEnumerable<T> Source { get; private set; }
		public SynchronizedEnumerableBuilder<T> SetSource(IEnumerable<T> source)
		{
			source.ThrowIfDefault(nameof(source));

			Source = source;

			return this;
		}

		public Int32 Turn { get; private set; }
		public SynchronizedEnumerableBuilder<T> SetTurn(Int32 turn)
		{
			turn.ThrowIfNot(_context.ValidTurns.Contains, $"{nameof(turn)} must be contained in {_context.ValidTurns}.", nameof(turn));

			Turn = turn;

			return this;
		}
		public SynchronizedEnumerableBuilder<T> IncrementTurn()
		{
			if (_context.ValidTurns.Contains(Turn + 1))
			{
				Turn++;
			}

			return this;
		}

		public IEnumerable<T> Build()
		{
			var result = new SynchronizedEnumerable<T>(Source, _context, Turn);

			return result;
		}

		public void Reset()
		{
			Source = Array.Empty<T>();
			Turn = 1;
		}
	}
}
