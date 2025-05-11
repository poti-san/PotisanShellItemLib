using Potisan.Windows.MediaFoundation.DXGI.ComTypes;

namespace Potisan.Windows.MediaFoundation.DXGI;

public sealed class MFDXGIDeviceManager(object? o) : ComUnknownWrapperBase<IMFDXGIDeviceManager>(o)
{
	public sealed class MFDXGIDeviceManagerDeviceHandle(MFDXGIDeviceManager manager, nint handle) : IDisposable
	{
		public MFDXGIDeviceManager Manager { get; } = manager;
		public nint Handle { get; private set; } = handle;

		public void Dispose()
		{
			Close();
		}

		public ComResult CloseNoThrow()
		{
			var cr = new ComResult(Manager._obj.CloseDeviceHandle(Handle));
			if (cr)
				Handle = 0;
			return cr;
		}

		public void Close()
			=> CloseNoThrow().ThrowIfError();

		/// <summary>
		///  ビデオサービスを開きます。対応するインターフェイスはID3D11DeviceとID3D11VideoDeviceです。
		/// </summary>
		public ComResult<object> GetVideoServiceNoThrow(in Guid iid)
			=> new(Manager._obj.GetVideoService(Handle, iid, out var x), x!);

		public object GetVideoService(in Guid iid)
			=> GetVideoServiceNoThrow(iid).Value;

		public ComResult<TWrapper> GetVideoServiceNoThrow<TWrapper, TInterface>()
			where TWrapper : IComUnknownWrapper
			=> IComUnknownWrapper.Wrap<TWrapper>(Manager._obj.GetVideoService(Handle, typeof(TInterface).GUID, out var x), x);

		public TWrapper GetVideoService<TWrapper, TInterface>()
			where TWrapper : IComUnknownWrapper
			=> GetVideoServiceNoThrow<TWrapper, TInterface>().Value;

		public ComResult<object> LockDeviceNoThrow(in Guid iid, bool block)
			=> new(Manager._obj.LockDevice(Handle, iid, out var x, block), x!);

		public object LockDevice(in Guid iid, bool block)
			=> LockDeviceNoThrow(iid, block).Value;

		public ComResult<TWrapper> LockDeviceNoThrow<TWrapper, TInterface>(bool block)
			where TWrapper : IComUnknownWrapper
			=> IComUnknownWrapper.Wrap<TWrapper>(Manager._obj.LockDevice(Handle, typeof(TInterface).GUID, out var x, block), x);

		public TWrapper LockDevice<TWrapper, TInterface>(bool block)
			where TWrapper : IComUnknownWrapper
			=> LockDeviceNoThrow<TWrapper, TInterface>(block).Value;

		public ComResult TestDeviceNoThrow()
			=> new(Manager._obj.TestDevice(Handle));

		public void TestDevice()
			=> TestDeviceNoThrow().ThrowIfError();

		public ComResult UnlockDeviceNoThrow(bool saveState)
			=> new(Manager._obj.UnlockDevice(Handle, saveState));

		public void UnlockDevice(bool saveState)
			=> UnlockDeviceNoThrow(saveState).ThrowIfError();
	}

	public ComResult<MFDXGIDeviceManagerDeviceHandle> DeviceHandleNoThrow
		=> new(_obj.OpenDeviceHandle(out var x), new(this, x));

	public MFDXGIDeviceManagerDeviceHandle DeviceHandle
		=> DeviceHandleNoThrow.Value;

	public ComResult ResetDeviceNoThrow(object device, uint resetToken)
		=> new(_obj.ResetDevice(device, resetToken));

	public void ResetDevice(object device, uint resetToken)
		=> ResetDeviceNoThrow(device, resetToken).ThrowIfError();

	public ComResult ResetDeviceNoThrow(IComUnknownWrapper device, uint resetToken)
		=> new(_obj.ResetDevice(device.WrappedObject!, resetToken));

	public void ResetDevice(IComUnknownWrapper device, uint resetToken)
		=> ResetDeviceNoThrow(device.WrappedObject!, resetToken).ThrowIfError();
}

//enum MF_DXGI_DEVICE_MANAGER_MODE
//{
//	MF_DXGI_DEVICE_MANAGER_MODE_INVALID = 0,
//	MF_DXGI_DEVICE_MANAGER_MODE_D3D11 = (MF_DXGI_DEVICE_MANAGER_MODE_INVALID + 1),
//	MF_DXGI_DEVICE_MANAGER_MODE_D3D12 = (MF_DXGI_DEVICE_MANAGER_MODE_D3D11 + 1)
//}
//MF_DXGI_DEVICE_MANAGER_MODE;
