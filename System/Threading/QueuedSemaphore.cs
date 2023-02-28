using Fort;
using RhoMicro.Common.Math;
using RhoMicro.Common.Math.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RhoMicro.Common.System.Threading
{
	/// <summary>
	/// A semaphore that enables processes to sequentially take turns at waiting.
	/// </summary>
	public sealed class QueuedSemaphore : DisposableBase
	{
		/// <summary>
		/// Initializes a new instance with the arguments provided.
		/// </summary>
		/// <param name="lastTurn">The last turn in a cycle, or the amount turns available.</param>
		/// <param name="ordered">Indicates whether or not turns are taken sequentially.</param>
		public QueuedSemaphore(Int32 lastTurn, Boolean ordered)
		{
			lastTurn.ThrowIfNot(t => t > 0, $"{nameof(lastTurn)} must be > 0.", nameof(lastTurn));

			LastTurn = lastTurn;
			Ordered = ordered;
			ValidTurns = BoundedInterval<Int32>.Create(0, lastTurn, false, true);
		}

		/// <summary>
		/// Indicates the maximum turn available.
		/// </summary>
		public Int32 LastTurn { get; }
		/// <summary>
		/// Indicates whether or not turns must be taken in order.
		/// </summary>
		public Boolean Ordered { get; }
		/// <summary>
		/// Defines the range of valid values for turns.
		/// </summary>
		public IBoundedInterval<Int32> ValidTurns { get; }

		private ISet<Int32> _cycle = new HashSet<Int32>();
		private Int32 _turnTaken = 0;
		private readonly SemaphoreSlim _gate = new SemaphoreSlim(1, 1);

		/// <summary>
		/// Signals the end of a turn.
		/// </summary>
		/// <param name="turn">The turn to end.</param>
		public void Release(Int32 turn)
		{
			if (_turnTaken == turn && _gate.CurrentCount == 0)
			{
				_gate.Release();
			}
		}

		/// <summary>
		/// Waits until a turn provided has been taken.
		/// </summary>
		/// <param name="turn">The turn to take.</param>
		public void Take(Int32 turn, CancellationToken token = default)
		{
			ThrowIfInvalid(turn);
			TakeValid(turn, token);
		}
		private void TakeValid(Int32 turn, CancellationToken token)
		{
			ThrowIfDisposed(nameof(QueuedSemaphore));

			try
			{
				_gate.Wait(token);
			}
			catch (OperationCanceledException ex)
			{
				_gate.Release();
				throw ex;
			}

			if (TryTake(turn))
			{
				return;
			}

			_gate.Release();
			TakeValid(turn, token);
		}

		/// <summary>
		/// Waits asynchronously until a turn provided has been taken.
		/// </summary>
		/// <param name="turn">The turn to take.</param>
		/// <returns>A task that will complete when the turn provided has been taken.</returns>
		public ValueTask TakeAsync(Int32 turn, CancellationToken token = default)
		{
			ThrowIfInvalid(turn);
			return TakeValidAsync(turn, token);
		}
		private async ValueTask TakeValidAsync(Int32 turn, CancellationToken token)
		{
			ThrowIfDisposed(nameof(QueuedSemaphore));

			try
			{
				await _gate.WaitAsync();
			}
			catch (OperationCanceledException ex)
			{
				_gate.Release();
				throw ex;
			}

			if (TryTake(turn))
			{
				return;
			}

			_gate.Release();
			await TakeValidAsync(turn, token);
		}

		private void ThrowIfInvalid(Int32 turn)
		{
			if (!ValidTurns.Contains(turn))
			{
				throw new ArgumentOutOfRangeException(nameof(turn), turn, $"{nameof(turn)} must be contained in {ValidTurns}.");
			}
		}
		private Boolean TryTake(Int32 turn)
		{
			var result = !_cycle.Contains(turn) && (!Ordered || turn == _turnTaken + 1);
			if (result)
			{
				_turnTaken = turn;
				_cycle.Add(turn);
			}
			else if (_cycle.Count == LastTurn)
			{
				_cycle.Clear();
				_turnTaken = 0;

				result = TryTake(turn);
			}

			return result;
		}

		/// <inheritdoc/>
		protected override void DisposeUnmanaged(Boolean disposing)
		{
			_cycle = null;
			base.DisposeUnmanaged(disposing);
		}
		/// <inheritdoc/>
		protected override void DisposeManaged(Boolean disposing)
		{
			_gate.Wait();
			_gate.Dispose();
			base.DisposeManaged(disposing);
		}
	}
}
