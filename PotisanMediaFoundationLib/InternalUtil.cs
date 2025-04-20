namespace Potisan.Windows.MediaFoundation;

public static class InternalUtil
{
	public static (uint Width, uint Height) GetUSizeFromUInt64(ulong value)
	{
		unchecked
		{
			return new((uint)((value & 0xffffffff00000000u) >> 32), (uint)value);
		}
	}
}
