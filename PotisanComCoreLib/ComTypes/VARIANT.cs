namespace Potisan.Windows.Com.ComTypes;

/// <summary>
/// COMの<c>VARIANT</c>構造体。
/// </summary>
/// <remarks>
/// 定義する目的は実用ではなく<c>VARIANT</c>構造体のサイズ計算です。
/// </remarks>
public struct VARIANT
{
	public VarType vt;
	public ushort wReserved1;
	public ushort wReserved2;
	public ushort wReserved3;
	public int _dummy1;
	public nint _dummy2;
}
