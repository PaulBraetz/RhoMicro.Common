using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides a function for attempting to clear all elements from the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasTryClear<out T> 
	{
		/// <summary>
		/// Attempts to clear all elements from the collection.
		/// </summary>
		Boolean TryClear();
	}
}
