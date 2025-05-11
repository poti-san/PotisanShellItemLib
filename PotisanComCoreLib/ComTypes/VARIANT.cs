namespace Potisan.Windows.Com.ComTypes;

/// <summary>
/// COMの<c>VARIANT</c>構造体。
/// </summary>
/// <remarks>
/// 定義する目的は実用ではなく<c>VARIANT</c>構造体のサイズ計算です。
/// </remarks>
public struct VARIANT
{
	/// <summary>
	/// 値の型。
	/// </summary>
	public VarType vt;
	/// <summary>
	/// 予約値。通常は0です。
	/// </summary>
	public ushort wReserved1;
	/// <summary>
	/// 予約値。通常は0です。
	/// </summary>
	public ushort wReserved2;
	/// <summary>
	/// 予約値。通常は0です。
	/// </summary>
	public ushort wReserved3;
	/// <summary>
	/// 保持データの1つめ。
	/// </summary>
	public int _dummy1;
	/// <summary>
	/// 保持データの2つめ。
	/// </summary>
	public nint _dummy2;
}
