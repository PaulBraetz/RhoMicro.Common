using System;
using System.Collections.Generic;
using System.Text;

namespace RhoMicro.Common.System.Abstractions
{
	/// <summary>
	/// Can convert instances of <typeparamref name="TFrom"/> to instances of <typeparamref name="TTo"/>.
	/// </summary>
	/// <typeparam name="TFrom">The type to convert from.</typeparam>
	/// <typeparam name="TTo">The type to convert to.</typeparam>
	public interface IConverter<in TFrom, out TTo>
	{
		/// <summary>
		/// Converts an instance of <typeparamref name="TFrom"/> to an instance of <typeparamref name="TTo"/>.
		/// </summary>
		/// <param name="from">The instance to convert to <typeparamref name="TTo"/>.</param>
		/// <returns>An instance of <typeparamref name="TTo"/>, converted from <paramref name="from"/>.</returns>
		TTo Convert(TFrom from);
	}
}
