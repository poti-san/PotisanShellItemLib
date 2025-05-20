namespace Potisan.Windows.Dxgi.ComTypes;

[Guid("3d3e0379-f9de-4d58-bb6c-18d62992f1a6")]

public interface IDXGIDeviceSubObject // IDXGIObject
{
	#region IDXGIDeviceSubObject

	#region IDXGIObject

	[PreserveSig]
	int SetPrivateData(
		in Guid Name,
		uint DataSize,
		in byte pData);

	[PreserveSig]
	int SetPrivateDataInterface(
		in Guid Name,
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnknown);

	[PreserveSig]
	int GetPrivateData(
		in Guid Name,
		ref uint pDataSize,
		ref byte pData);

	[PreserveSig]
	int GetParent(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppParent);

	#endregion IDXGIObject

	[PreserveSig]
	int GetDevice(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppDevice);

	#endregion IDXGIDeviceSubObject
}