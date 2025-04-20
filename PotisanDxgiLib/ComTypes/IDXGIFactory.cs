using System.Runtime.InteropServices;

namespace Potisan.Windows.Dxgi.ComTypes;

[ComImport]
[Guid("7b7166ec-21c7-44ae-b21a-c9ae321ae369")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXGIFactory // IDXGIObject
{
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
	int EnumAdapters(
			uint Adapter,
			out IDXGIAdapter ppAdapter);

	[PreserveSig]
	int MakeWindowAssociation(
		nint WindowHandle,
		uint Flags);

	[PreserveSig]
	int GetWindowAssociation(
		out nint pWindowHandle);

	[PreserveSig]
	int CreateSwapChain(
		[MarshalAs(UnmanagedType.IUnknown)] object pDevice,
		in DxgiSwapChainDescription pDesc,
		out IDXGISwapChain ppSwapChain);

	[PreserveSig]
	int CreateSoftwareAdapter(
		nint ModuleHandle,
		out IDXGIAdapter ppAdapter);
}