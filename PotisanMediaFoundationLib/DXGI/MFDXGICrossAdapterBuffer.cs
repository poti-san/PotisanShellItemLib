using Potisan.Windows.MediaFoundation.DXGI.ComTypes;

namespace Potisan.Windows.MediaFoundation.DXGI;

public class MFDXGICrossAdapterBuffer(object? o) : ComUnknownWrapperBase<IMFDXGICrossAdapterBuffer>(o)
{
	public ComResult<object> GetResourceForDeviceNoThrow(object device, in Guid iid)
		=> new(_obj.GetResourceForDevice(device, iid, out var x), x);

	public object GetResourceForDevice(object device, in Guid iid)
		=> GetResourceForDeviceNoThrow(device, iid).Value;

	public ComResult<TWrapper> GetResourceForDeviceNoThrow<TWrapper, TInterface>(object device)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetResourceForDevice(device, typeof(TInterface).GUID, out var x), x);

	public TWrapper GetResourceForDevice<TWrapper, TInterface>(object device)
		where TWrapper : IComUnknownWrapper
		=> GetResourceForDeviceNoThrow<TWrapper, TInterface>(device).Value;

	public ComResult<uint> GetSubresourceIndexForDeviceNoThrow(object device)
		=> new(_obj.GetSubresourceIndexForDevice(device, out var x), x);

	public uint GetSubresourceIndexForDevice(object device)
		=> GetSubresourceIndexForDeviceNoThrow(device).Value;

	public ComResult<uint> GetSubresourceIndexForDeviceNoThrow(IComUnknownWrapper device)
		=> new(_obj.GetSubresourceIndexForDevice(device.WrappedObject!, out var x), x);

	public uint GetSubresourceIndexForDevice(IComUnknownWrapper device)
		=> GetSubresourceIndexForDeviceNoThrow(device).Value;

	public ComResult<object> GetUnknownForDeviceNoThrow(object device, in Guid guid, in Guid iid)
		=> new(_obj.GetUnknownForDevice(device, guid, iid, out var x), x);

	public object GetUnknownForDevice(object device, in Guid guid, in Guid iid)
		=> GetUnknownForDeviceNoThrow(device, guid, iid).Value;

	public ComResult<TWrapper> GetUnknownForDeviceNoThrow<TWrapper, TInterface>(IComUnknownWrapper device, in Guid guid)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetUnknownForDevice(device.WrappedObject!, guid, typeof(TInterface).GUID, out var x), x);

	public object GetUnknownForDevice<TWrapper, TInterface>(IComUnknownWrapper device, in Guid guid)
		where TWrapper : IComUnknownWrapper
		=> GetUnknownForDeviceNoThrow<TWrapper, TInterface>(device, guid).Value;

	public ComResult SetUnknownForDeviceNoThrow(object device, in Guid guid, object data)
		=> new(_obj.SetUnknownForDevice(device, guid, data));

	public void SetUnknownForDevice(object device, in Guid guid, object data)
		=> SetUnknownForDeviceNoThrow(device, guid, data).ThrowIfError();

	public ComResult SetUnknownForDeviceNoThrow(IComUnknownWrapper device, in Guid guid, IComUnknownWrapper data)
		=> new(_obj.SetUnknownForDevice(device.WrappedObject!, guid, data.WrappedObject));

	public void SetUnknownForDevice(IComUnknownWrapper device, in Guid guid, IComUnknownWrapper data)
		=> SetUnknownForDeviceNoThrow(device, guid, data).ThrowIfError();
}
