using Fort;
using System.Threading;
using System;

namespace RhoMicro.Common.System.Threading
{
	/// <summary>
	/// Encapsulates exiting a <see cref="SemaphoreSlim"/> upon disposal.
	/// </summary>
	public readonly struct SemaphoreReleaseHandle : IDisposable
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public SemaphoreReleaseHandle(SemaphoreSlim gate, int releaseCount = 1)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
		{
			gate.ThrowIfNull(nameof(gate));
			releaseCount.ThrowIfNot(c => c > 0, $"{nameof(releaseCount)} must > 0.", nameof(releaseCount));

			_gate = gate;
			ReleaseCount = releaseCount;
		}

		private readonly SemaphoreSlim _gate;
		/// <summary>
		/// The number of times to exit the semaphore upon disposal.
		/// </summary>
		public int ReleaseCount { get; }

		/// <summary>
		/// Exits the underlying semaphore as often as specified in <see cref="ReleaseCount"/>.
		/// </summary>
		public void Dispose() => _gate.Release(ReleaseCount);
	}
}