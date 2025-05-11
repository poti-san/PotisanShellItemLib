using Potisan.Windows.Com;
using Potisan.Windows.DXCore.ComTypes;

namespace Potisan.Windows.DXCore;

/// <summary>
/// DXCoreのアダプタ情報（バージョン1.1）。<see cref="DXCoreAdapterList"/>から作成します。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class DXCoreAdapter1(object? o) : DXCoreAdapter(o)
{
	private new readonly IDXCoreAdapter1 _obj = o != null ? (IDXCoreAdapter1)o : null!;

#pragma warning disable CA1816 // Dispose メソッドは、SuppressFinalize を呼び出す必要があります
	public override void Dispose()
	{
		base.Dispose();
		Marshal.FinalReleaseComObject(_obj);
	}
#pragma warning restore CA1816

	public ComResult<byte[]> GetPropertyWithInputNoThrow(DXCoreAdapterProperty property, byte[] inputPropertyDetails)
	{
		var cr = GetPropertySizeNoThrow(property);
		if (!cr) return new(cr.HResult, null!);

		var buffer = GC.AllocateUninitializedArray<byte>(checked((int)cr.ValueUnchecked));
		return new(_obj.GetPropertyWithInput(property, (nuint)inputPropertyDetails.Length, inputPropertyDetails,
			cr.ValueUnchecked, ref MemoryMarshal.GetArrayDataReference(buffer)), buffer);
	}

	public byte[] GetPropertyWithInput(DXCoreAdapterProperty property, byte[] inputPropertyDetails)
		=> GetPropertyWithInputNoThrow(property, inputPropertyDetails).Value;

	public ComResult<byte[]> GetPropertyWithInputNoThrow(DXCoreAdapterProperty property, int outputSize, byte[] inputPropertyDetails)
	{
		var buffer = GC.AllocateUninitializedArray<byte>(checked(outputSize));
		return new(_obj.GetPropertyWithInput(property, (nuint)inputPropertyDetails.Length, inputPropertyDetails,
			(uint)outputSize, ref MemoryMarshal.GetArrayDataReference(buffer)), buffer);
	}

	public byte[] GetPropertyWithInput(DXCoreAdapterProperty property, int outputSize, byte[] inputPropertyDetails)
		=> GetPropertyWithInputNoThrow(property, outputSize, inputPropertyDetails).Value;
}
