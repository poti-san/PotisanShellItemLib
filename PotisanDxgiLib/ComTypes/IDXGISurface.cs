using System.Runtime.InteropServices;

namespace Potisan.Windows.Dxgi.ComTypes;

[Guid("cafcb56c-6ac3-4889-bf47-9e23bbd260ec")]
public interface IDXGISurface // IDXGIDeviceSubObject
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
	int GetDesc(
		out DxgiSurfaceDesc pDesc);

	[PreserveSig]
	int Map(
		out DxgiMappedRect pLockedRect,
		uint MapFlags);

	[PreserveSig]
	int Unmap();
}