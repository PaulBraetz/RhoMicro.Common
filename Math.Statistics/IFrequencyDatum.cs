namespace RhoMicro.Common.Math.Statistics
{
	internal interface IFrequencyDatum<T>
	{
		global::System.Int32 AbsoluteFrequency { get; }
		global::System.Double RelativeFrequency { get; }
		T Sample { get; }
	}
}