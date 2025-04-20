namespace Potisan.Windows.MediaFoundation;

public record struct MFFrameRate(uint Numerator, uint Denominator)
{
	public MFFrameRate(ulong x)
		: this(0, 0)
	{
		(Numerator, Denominator) = InternalUtil.GetUSizeFromUInt64(x);
	}

	public readonly ulong ToUInt64() => (Numerator << 32) | Denominator;

	public ulong AverageTimePerFrame
	{
		readonly get
		{
			[DllImport("mfplat.dll")]
			static extern int MFFrameRateToAverageTimePerFrame(
				uint unNumerator, uint unDenominator, out ulong punAverageTimePerFrame);

			Marshal.ThrowExceptionForHR(MFFrameRateToAverageTimePerFrame(Numerator, Denominator, out var x));
			return x;
		}
		set
		{
			[DllImport("mfplat.dll")]
			static extern int MFAverageTimePerFrameToFrameRate(
				ulong unAverageTimePerFrame, out uint punNumerator, out uint punDenominator);

			Marshal.ThrowExceptionForHR(MFAverageTimePerFrameToFrameRate(value, out var a, out var b));
			Numerator = a;
			Denominator = b;
		}
	}
}
