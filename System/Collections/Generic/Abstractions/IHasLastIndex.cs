﻿using System;
using System.Collections.Generic;

namespace RhoMicro.Common.System.Collections.Generic.Abstractions
{
	/// <summary>
	/// Provides information on the last index in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	/// <typeparam name="TIndex">
	/// The type of index supported by the collection.
	/// </typeparam>
	public interface IHasLastIndex<out T, out TIndex> 
	{
		/// <summary>
		/// Gets the last index in the collection.
		/// </summary>
		TIndex LastIndex { get; }
	}
	/// <summary>
	/// Provides information on the last index in the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The type of elements contained in the collection.
	/// </typeparam>
	public interface IHasLastIndex<out T> : IHasLastIndex<T, Int32>
	{
	}
}
