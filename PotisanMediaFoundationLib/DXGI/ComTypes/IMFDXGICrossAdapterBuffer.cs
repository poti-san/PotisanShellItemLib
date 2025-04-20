namespace Potisan.Windows.MediaFoundation.DXGI.ComTypes;

[ComImport]
[Guid("B25D03FB-D148-45EF-BFED-F778B7566C07")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFDXGICrossAdapterBuffer
{
	[PreserveSig]
	int GetResourceForDevice(
		[MarshalAs(UnmanagedType.IUnknown)] object pUnkDevice,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppvObject);

	[PreserveSig]
	int GetSubresourceIndexForDevice(
		[MarshalAs(UnmanagedType.IUnknown)] object pUnkDevice,
		out uint puSubresource);

	[PreserveSig]
	int GetUnknownForDevice(
		[MarshalAs(UnmanagedType.IUnknown)] object pUnkDevice,
		in Guid guid,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppvObject);

	[PreserveSig]
	int SetUnknownForDevice(
		[MarshalAs(UnmanagedType.IUnknown)] object pUnkDevice,
		in Guid guid,
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnkData);
}
