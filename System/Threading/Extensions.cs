using Fort;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RhoMicro.Common.System.Threading
{
	/// <summary>
	/// Extensions for the <c>RhoMicro.Common.System.Threading</c> namespace.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Returns a new release handle on a semaphore.
		/// </summary>
		/// <param name="gate">The semaphore for which to acquire a new release handle.</param>
		/// <param name="releaseCount">The release count of the release handle created.</param>
		/// <returns>A new instance of <see cref="SemaphoreReleaseHandle"/>, initialized to the semaphore and release count provided.</returns>
		public static SemaphoreReleaseHandle GetReleaseHandle(this SemaphoreSlim gate, Int32 releaseCount = 1)
		{
			gate.ThrowIfNull(nameof(gate));
			releaseCount.ThrowIfNot(c => c > 0, $"{nameof(releaseCount)} must > 0.", nameof(releaseCount));

			var result = new SemaphoreReleaseHandle(gate, releaseCount);

			return result;
		}
	}
}
