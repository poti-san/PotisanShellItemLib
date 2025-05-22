using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// クリップボードフォーマットと追加情報。
/// </summary>
/// <remarks>
/// <c>FORMATETC</c>ネイティブ構造体のラッパーです。
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public class ComFormatEtc
{
	public ClipboardFormat ClipboardFormat;
	/// <summary>
	/// <c>DVTARGETDEVICE*</c>
	/// </summary>
	public nint TargetDevicePointer;
	public DVAspect Aspect;
	public int Index;

	/// <summary>
	/// サポートするデータ形式の組み合わせ。
	/// </summary>
	public MediumType Tymed;
}