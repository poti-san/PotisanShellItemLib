namespace Potisan.Windows.Com;

/// <summary>
/// 一般的なHRESULTの値。
/// </summary>
public static class CommonHResults
{
	public const int SOK = 0;
	public const int SFalse = 1;
	public const int ENotImpl = unchecked((int)0x80004001);
	public const int ENoInterface = unchecked((int)0x80004002);
	public const int EPointer = unchecked((int)0x80004003);
	public const int EAbort = unchecked((int)0x80004004);
	public const int EFail = unchecked((int)0x80004005);
	public const int EUnexpected = unchecked((int)0x8000FFFF);
	public const int EAccessDenied = unchecked((int)0x80070005);
	public const int EHandle = unchecked((int)0x80070006);
	public const int EOutOfMemory = unchecked((int)0x8007000E);
	public const int EInvalidArg = unchecked((int)0x80070057);
}
