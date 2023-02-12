using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Base class for types implementing <see cref="ICloneable{TSelf}"/>.
	/// </summary>
	/// <typeparam name="TSelf">This type.</typeparam>
	public abstract class CloneableBase<TSelf> : ICloneable<TSelf>
		where TSelf : CloneableBase<TSelf>
	{
		/// <inheritdoc/>
		public abstract TSelf Clone();

		Object ICloneable.Clone()
		{
			return Clone();
		}
	}
}
