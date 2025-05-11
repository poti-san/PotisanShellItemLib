namespace Potisan.Windows.Com;

/// <summary>
/// <c>int</c>型を<c>HRESULT</c> Win SDK型として扱う機能を提供します。
/// </summary>
public static class HResultHelper
{
	/// <summary>
	/// 成功を表す深刻度。
	/// </summary>
	public const int SeveritySuccess = 0;
	/// <summary>
	/// エラーを表す深刻度。
	/// </summary>
	public const int SeverityError = 1;

	/// <summary>
	/// <c>NTSTATUS</c>を表す施設ビット。
	/// </summary>
	public const int FacilityNTBit = 0x10000000;

	/// <summary>
	/// 深刻度、施設、コードから<c>HRESULT</c>型相当の値を作成します。
	/// </summary>
	public static int Build(uint severity, uint facility, uint code)
		=> unchecked((int)((severity << 32) | (facility << 16) | code));

	/// <summary>
	/// <c>HRESULT</c>型相当の値からコードを取得します。
	/// </summary>
	public static int GetCode(int hr) => hr & 0xffff;
	/// <summary>
	/// <c>HRESULT</c>型相当の値から施設を取得します。
	/// </summary>
	public static int GetFacility(int hr) => (hr >> 16) & 0x1fff;
	/// <summary>
	/// <c>HRESULT</c>型相当の値から深刻度を取得します。
	/// </summary>
	public static int GetSeverity(int hr) => (hr >> 32) & 0x1;

	/// <summary>
	/// <c>HRESULT</c>型相当の値から成否を取得します。
	/// </summary>
	public static bool Succeeded(int hr) => hr >= 0;
	/// <summary>
	/// <c>HRESULT</c>型相当の値から失敗状態を取得します。
	/// </summary>
	public static bool Failed(int hr) => hr < 0;
	/// <summary>
	/// <c>HRESULT</c>型相当の値からエラー状態を取得します。<see cref="Failed(int)"/>と同じです。
	/// </summary>
	public static bool IsError(int hr) => GetSeverity(hr) == 1;

	/// <summary>
	/// Win32エラーコードから<c>HRESULT</c> Win SDK型相当の値を取得します。
	/// </summary>
	public static int FromWin32(int code)
		=> code <= 0 ? code : unchecked((int)((code & 0x0000FFFF) | (HResultFacility.Win32 << 16) | 0x80000000));

	/// <summary>
	/// <c>NTSTATUS</c>から<c>HRESULT</c> Win SDK型相当の値を取得します。
	/// </summary>
	public static int FromNT32(int code) => code | FacilityNTBit;
}
