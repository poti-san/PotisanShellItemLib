namespace Potisan.Windows.Com;

public static class HResultHelper
{
	public const int SeveritySuccess = 0;
	public const int SeverityError = 1;

	public const int FacilityNTBit = 0x10000000;

	public static int Build(uint severity, uint facility, uint code)
		=> unchecked((int)((severity << 32) | (facility << 16) | code));

	public static int GetCode(int hr) => hr & 0xffff;
	public static int GetFacility(int hr) => (hr >> 16) & 0x1fff;
	public static int GetSeverity(int hr) => (hr >> 32) & 0x1;

	public static bool Succeeded(int hr) => hr >= 0;
	public static bool Failed(int hr) => hr < 0;
	public static bool IsError(int hr) => GetSeverity(hr) == 1;

	public static int FromWin32(int code)
		=> code <= 0 ? code : unchecked((int)((code & 0x0000FFFF) | (HResultFacility.Win32 << 16) | 0x80000000));

	public static int FromNT32(int code)
		=> code | FacilityNTBit;
}
