using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// <c>FORMATETC</c>
/// </summary>
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