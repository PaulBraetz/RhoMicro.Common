using Fort;
using RhoMicro.Common.System.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RhoMicro.Common.System
{
	internal sealed class AsyncVisitorStrategy<T> : AsyncVisitorBase<T>
	{
		public AsyncVisitorStrategy(Func<T, CancellationToken, Task> receiveStrategy, Func<T, CancellationToken, Task<Boolean>> canReceiveStrategy = null)
		{
			receiveStrategy.ThrowIfDefault(nameof(receiveStrategy));

			_canReceiveStrategy = canReceiveStrategy;
			_receiveStrategy = receiveStrategy;
		}
		public AsyncVisitorStrategy(Func<T, CancellationToken, Task<Boolean>> visitStrategy)
		{
			visitStrategy.ThrowIfDefault(nameof(visitStrategy));

			_visitStrategy = visitStrategy;
		}

		private readonly Func<T, CancellationToken, Task<Boolean>> _canReceiveStrategy;
		private readonly Func<T, CancellationToken, Task> _receiveStrategy;
		private readonly Func<T, CancellationToken, Task<Boolean>> _visitStrategy;

		public override Task<Boolean> VisitAsync(T obj, CancellationToken cancellationToken)
		{
			var result = _visitStrategy?.Invoke(obj, cancellationToken) ?? base.VisitAsync(obj, cancellationToken);

			return result;
		}
		protected override Task<Boolean> CanReceive(T obj, CancellationToken cancellationToken = default)
		{
			return _canReceiveStrategy?.Invoke(obj, cancellationToken) ?? base.CanReceive(obj, cancellationToken);
		}
		protected override Task Receive(T obj, CancellationToken cancellationToken = default)
		{
			return _receiveStrategy.Invoke(obj, cancellationToken);
		}
	}
}
