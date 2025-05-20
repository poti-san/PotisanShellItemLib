namespace Potisan.Windows.Dxgi.ComTypes;

[ComImport]
[Guid("310d36a0-d2e7-4c0a-aa04-6a9d23b8886a")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXGISwapChain // IDXGIDeviceSubObject
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
	int Present(
		uint SyncInterval,
		uint Flags);

	[PreserveSig]
	int GetBuffer(
		uint Buffer,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppSurface);

	[PreserveSig]
	int SetFullscreenState(
		[MarshalAs(UnmanagedType.Bool)] bool Fullscreen,
		IDXGIOutput? pTarget);

	[PreserveSig]
	int GetFullscreenState(
		[MarshalAs(UnmanagedType.Bool)] out bool pFullscreen,
		out IDXGIOutput ppTarget);

	[PreserveSig]
	int GetDesc(
		out DxgiSwapChainDescription pDesc);

	[PreserveSig]
	int ResizeBuffers(
		uint BufferCount,
		uint Width,
		uint Height,
		DxgiFormat NewFormat,
		uint SwapChainFlags);

	[PreserveSig]
	int ResizeTarget(
		in DxgiModeDesc pNewTargetParameters);

	[PreserveSig]
	int GetContainingOutput(
		out IDXGIOutput? ppOutput);

	[PreserveSig]
	int GetFrameStatistics(
		out DxgiFrameStatistics pStats);

	[PreserveSig]
	int GetLastPresentCount(
		out uint pLastPresentCount);
}