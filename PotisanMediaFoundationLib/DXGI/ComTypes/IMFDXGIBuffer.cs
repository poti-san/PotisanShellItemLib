namespace Potisan.Windows.MediaFoundation.DXGI.ComTypes;

[ComImport]
[Guid("e7174cfa-1c9e-48b1-8866-626226bfc258")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFDXGIBuffer
{
	[PreserveSig]
	int GetResource(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvObject);

	[PreserveSig]
	int GetSubresourceIndex(
		out uint puSubresource);

	[PreserveSig]
	int GetUnknown(
		in Guid guid,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvObject);

	[PreserveSig]
	int SetUnknown(
		in Guid guid,
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnkData);
}
