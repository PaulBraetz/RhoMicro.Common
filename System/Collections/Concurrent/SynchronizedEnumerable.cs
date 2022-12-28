using Fort;
using RhoMicro.Common.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RhoMicro.Common.System.Collections.Concurrent
{
	internal sealed class SynchronizedEnumerable<T> : IEnumerable<T>
	{
		private readonly struct Enumerator : IEnumerator<T>
		{
			public Enumerator(SynchronizedEnumerable<T> source)
			{
				source.ThrowIfDefault(nameof(source));

				_source = source;
				_wrappedEnumerator = source._wrappedEnumerable.GetEnumerator();

				Context.ResetRequested += OnResetRequested;
			}

			private readonly SynchronizedEnumerable<T> _source;
			private readonly IEnumerator<T> _wrappedEnumerator;

			private Int32 Turn => _source._turn;
			private SynchronizedEnumeratorContext Context => _source._context;

			/// <inheritdoc/>
			public T Current => _wrappedEnumerator.Current;
			Object IEnumerator.Current => _wrappedEnumerator.Current;

			/// <inheritdoc/>
			public Boolean MoveNext()
			{
				var result = Context.MoveNext(_wrappedEnumerator, Turn);

				return result;
			}

			/// <inheritdoc/>
			public void Reset()
			{
				Context.RequestReset();
			}

			private void OnResetRequested(Object sender, EventArgs args)
			{
				_wrappedEnumerator.Reset();
			}

			public void Dispose()
			{
				_wrappedEnumerator.Dispose();
			}
		}

		public SynchronizedEnumerable(IEnumerable<T> wrappedEnumerable, SynchronizedEnumeratorContext context, Int32 turn)
		{
			wrappedEnumerable.ThrowIfDefault(nameof(wrappedEnumerable));
			context.ThrowIfDefault(nameof(context));
			turn.ThrowIfNot(context.ValidTurns.Contains, $"{nameof(turn)} must be contained in {context.ValidTurns}.", nameof(turn));

			_wrappedEnumerable = wrappedEnumerable;
			_context = context;
			_turn = turn;
		}

		private readonly Int32 _turn;
		private readonly SynchronizedEnumeratorContext _context;
		private readonly IEnumerable<T> _wrappedEnumerable;

		public IEnumerator<T> GetEnumerator()
		{
			var result = new Enumerator(this);

			return result;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
