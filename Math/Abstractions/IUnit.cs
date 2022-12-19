using System;

namespace RhoMicro.Common.Math.Abstractions
{
	/// <summary>
	/// Represents a unit of measurement.
	/// </summary>
	public interface IUnit
	{
		/// <summary>
		/// The name of the unit.
		/// </summary>
		String Name { get; }
	}
}