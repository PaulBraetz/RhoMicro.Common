using Fort;
using RhoMicro.Common.System.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RhoMicro.Common.System
{
	internal sealed class VisitorStrategy<T> : VisitorBase<T>
	{
		public VisitorStrategy(Action<T> receiveStrategy, Func<T, Boolean> canReceiveStrategy = null)
		{
			receiveStrategy.ThrowIfDefault(nameof(receiveStrategy));

			_canReceiveStrategy = canReceiveStrategy;
			_receiveStrategy = receiveStrategy;
		}

		private readonly Func<T, Boolean> _canReceiveStrategy;
		private readonly Action<T> _receiveStrategy;

		protected override Boolean CanReceive(T obj)
		{
			return _canReceiveStrategy?.Invoke(obj) ?? base.CanReceive(obj);
		}
		protected override void Receive(T obj)
		{
			_receiveStrategy.Invoke(obj);
		}
	}
}
