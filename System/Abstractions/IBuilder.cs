using System;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Represents an implementation of the builder pattern.
	/// </summary>
	/// <typeparam name="T">The type of object this builder can construct.</typeparam>
	public interface IBuilder<T>
	{
		/// <summary>
		/// Creates a new instance of <typeparamref name="T"/>.
		/// </summary>
		/// <returns>A new instance of <typeparamref name="T"/>.</returns>
		T Build();
		/// <summary>
		/// Resets the builder.
		/// </summary>
		void Reset();
	}
}
