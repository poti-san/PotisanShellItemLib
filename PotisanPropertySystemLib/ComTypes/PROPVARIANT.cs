using System.Security.Cryptography.X509Certificates;

namespace Potisan.Windows.PropertySystem.ComTypes;

/// <summary>
/// 構造体などに配置するための値型構造体。
/// </summary>
public struct PROPVARIANT : IDisposable
{
	public VarType VT;
	public ushort Reserved1;
	public ushort Reserved2;
	public ushort Reserved3;
	public int Dummy1;
	public nint Dummy2;

	/// <summary>
	/// 構造体の内容をそのまま移した<see cref="PropVariant"/>を作成します。
	/// </summary>
	/// <returns></returns>
	public PropVariant ToPropVariantAndFree()
	{
		[DllImport("ole32.dll")]
		static extern int PropVariantCopy([Out] PropVariant pvarDest, in PROPVARIANT pvarSrc);

		var pv = new PropVariant();
		Marshal.ThrowExceptionForHR(PropVariantCopy(pv, this));
		Dispose();
		return pv;
	}

	public void Dispose()
	{
		[DllImport("ole32.dll")]
		static extern int PropVariantClear(ref PROPVARIANT propvar);

		Marshal.ThrowExceptionForHR(PropVariantClear(ref this));
	}
}
