namespace Potisan.Windows.Dxgi.ComTypes;

[Guid("035f3ab4-482e-4e50-b41f-8a7f8bd8960b")]
public interface IDXGIResource // IDXGIDeviceSubObject
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

	[PreserveSig]
	int GetSharedHandle(
		out nint pSharedHandle);

	[PreserveSig]
	int GetUsage(
		out DxgiUsage pUsage);

	[PreserveSig]
	int SetEvictionPriority(
		uint EvictionPriority);

	[PreserveSig]
	int GetEvictionPriority(
		out uint pEvictionPriority);
}