using Potisan.Windows.Com;
using Potisan.Windows.Dxgi.ComTypes;

namespace Potisan.Windows.Dxgi;

public class DxgiDeviceSubObject(object? o) : DxgiObject(o)
{
	private new readonly IDXGIDeviceSubObject _obj = o != null ? (IDXGIDeviceSubObject)o : null!;

#pragma warning disable CA1816 // Dispose メソッドは、SuppressFinalize を呼び出す必要があります
	public override void Dispose()
	{
		base.Dispose();
		Marshal.FinalReleaseComObject(_obj);
	}
#pragma warning restore CA1816

	public ComResult<object> GetDeviceNoThrow(in Guid iid)
		=> new(_obj.GetDevice(iid, out var x), x!);

	public object GetDevice(in Guid iid)
		=> GetDeviceNoThrow(iid).Value;

	public ComResult<TWrapper> GetDeviceNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(GetDeviceNoThrow(typeof(TInterface).GUID));

	public TWrapper GetDevice<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetDeviceNoThrow<TWrapper, TInterface>().Value;
}
