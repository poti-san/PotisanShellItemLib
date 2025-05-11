namespace Potisan.Windows.MediaFoundation.DXGI.ComTypes;

[ComImport]
[Guid("eb533d5d-2db6-40f8-97a9-494692014f07")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFDXGIDeviceManager
{
	[PreserveSig]
	int CloseDeviceHandle(
		nint hDevice);

	[PreserveSig]
	int GetVideoService(
		nint hDevice,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppService);

	[PreserveSig]
	int LockDevice(
		nint hDevice,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppUnkDevice,
		[MarshalAs(UnmanagedType.Bool)] bool fBlock);

	[PreserveSig]
	int OpenDeviceHandle(
		out nint phDevice);

	[PreserveSig]
	int ResetDevice(
		[MarshalAs(UnmanagedType.IUnknown)] object pUnkDevice,
		uint resetToken);

	[PreserveSig]
	int TestDevice(
		nint hDevice);

	[PreserveSig]
	int UnlockDevice(
		nint hDevice,
		[MarshalAs(UnmanagedType.Bool)] bool fSaveState);
}
