using RhoMicro.Common.Math.Abstractions;
using RhoMicro.Common.System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Concurrent
{
	internal sealed class SynchronizedEnumeratorContext : DisposableBase
	{
		public SynchronizedEnumeratorContext(Int32 lastTurn, Boolean ordered = false)
		{
			Gate = new QueuedSemaphore(lastTurn, ordered);
		}

		private QueuedSemaphore Gate { get; }
		public IBoundedInterval<Int32> ValidTurns => Gate.ValidTurns;
		public event EventHandler ResetRequested;
		private Boolean Exit { get; set; }

		public void RequestReset()
		{
			ResetRequested.Invoke(this, EventArgs.Empty);
		}
		public Boolean MoveNext(IEnumerator enumerator, Int32 turn)
		{
			if (Exit)
			{
				return false;
			}

			Gate.Take(turn);
			try
			{
				var result = enumerator.MoveNext();
				Exit = !result;
				Gate.Release(turn);
				return result;
			}
			catch
			{
				Exit = true;
				throw;
			}
		}

		protected override void DisposeManaged(Boolean disposing)
		{
			Gate.Dispose();
			base.DisposeManaged(disposing);
		}
	}
}
