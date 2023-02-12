using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Supports cloning, which creates a new instance of a class with the same value
	/// as an existing instance.
	/// </summary>
	/// <typeparam name="TSelf">This type.</typeparam>
	public interface ICloneable<TSelf> : ICloneable
	{
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		new TSelf Clone();
	}
}
