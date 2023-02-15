using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhoMicro.Common.System.Threading
{
	/// <summary>
	/// A container for tasks to complete in.
	/// </summary>
	[DebuggerDisplay("Count = {Count}")]
	public sealed class TaskConsumer : ICollection<Task>
	{
		/// <inheritdoc/>
		public Boolean IsReadOnly => false;
		/// <inheritdoc/>
		public Int32 Count => _tasks.Count;

		private readonly ConcurrentDictionary<Task, Task> _tasks = new ConcurrentDictionary<Task, Task>();

		/// <summary>
		/// Adds a task to the container. A continuation is scheduled to remove the task from the container upon its completion.
		/// </summary>
		/// <param name="item">The task to add to the container.</param>
		public void Add(Task item)
		{
			_tasks.AddOrUpdate(item, k => valueFactory(item), (o, k) => valueFactory(item));

			Task valueFactory(Task key)
			{
				var result = key.ContinueWith(Remove);

				return result;
			}
		}
		/// <inheritdoc/>
		public void Clear()
		{
			_tasks.Clear();
		}
		/// <inheritdoc/>
		public Boolean Contains(Task item)
		{
			var result = _tasks.ContainsKey(item);

			return result;
		}
		/// <inheritdoc/>
		public void CopyTo(Task[] array, Int32 arrayIndex)
		{
			_tasks.Keys.CopyTo(array, arrayIndex);
		}
		/// <inheritdoc/>
		public Boolean Remove(Task item)
		{
			var result = _tasks.TryRemove(item, out _);

			return result;
		}
		/// <inheritdoc/>
		public IEnumerator<Task> GetEnumerator()
		{
			return _tasks.Keys.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
