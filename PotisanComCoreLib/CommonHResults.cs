namespace Potisan.Windows.Com;

/// <summary>
/// 一般的なHRESULTの値。
/// </summary>
public static class CommonHResults
{
	/// <summary>
	/// 成功。
	/// </summary>
	public const int SOK = 0;
	/// <summary>
	/// エラーではないが失敗。
	/// </summary>
	public const int SFalse = 1;
	/// <summary>
	/// 未実装エラー。
	/// </summary>
	public const int ENotImpl = unchecked((int)0x80004001);
	/// <summary>
	/// インターフェイスキャストエラー。
	/// </summary>
	public const int ENoInterface = unchecked((int)0x80004002);
	/// <summary>
	/// ポインタエラー。
	/// </summary>
	public const int EPointer = unchecked((int)0x80004003);
	/// <summary>
	/// 中断エラー。
	/// </summary>
	public const int EAbort = unchecked((int)0x80004004);
	/// <summary>
	/// エラーとしての失敗。
	/// </summary>
	public const int EFail = unchecked((int)0x80004005);
	/// <summary>
	/// 予期しないエラー。
	/// </summary>
	public const int EUnexpected = unchecked((int)0x8000FFFF);
	/// <summary>
	/// アクセス拒否エラー。
	/// </summary>
	public const int EAccessDenied = unchecked((int)0x80070005);
	/// <summary>
	/// ハンドルエラー。
	/// </summary>
	public const int EHandle = unchecked((int)0x80070006);
	/// <summary>
	/// メモリ不足エラー。
	/// </summary>
	public const int EOutOfMemory = unchecked((int)0x8007000E);
	/// <summary>
	/// 不正引数エラー。
	/// </summary>
	public const int EInvalidArg = unchecked((int)0x80070057);
}
