using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.System.Threading
{
	internal sealed class TransactionalTaskConsumer
	{
		private readonly struct TransactionItem:IComparable<TransactionItem>
		{
			public TransactionItem(Task task) : this()
			{
				Task = task ?? throw new ArgumentNullException(nameof(task));
				_createdAt = DateTimeOffset.UtcNow.Ticks;
			}

			public Task Task { get; }
			private readonly Int64 _createdAt;

			public Int32 CompareTo(TransactionItem other)
			{
				var result = _createdAt.CompareTo(other._createdAt);

				return result;
			}
		}

	}
}
