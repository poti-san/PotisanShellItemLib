namespace Potisan.Windows.Dxgi.ComTypes;

[ComImport]
[Guid("ae02eedb-c735-4690-8d52-5a8dc20213aa")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXGIOutput // IDXGIObject
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
	int GetDesc(
		out DxgiOutputDesc pDesc);

	[PreserveSig]
	int GetDisplayModeList(
		DxgiFormat EnumFormat,
		uint Flags,
		ref uint pNumModes,
		[Out][MarshalAs(UnmanagedType.LPArray)] DxgiModeDesc[]? pDesc);

	[PreserveSig]
	int FindClosestMatchingMode(
		in DxgiModeDesc pModeToMatch,
		out DxgiModeDesc pClosestMatch,
		[MarshalAs(UnmanagedType.IUnknown)] object? pConcernedDevice);

	[PreserveSig]
	int WaitForVBlank();

	[PreserveSig]
	int TakeOwnership(
		[MarshalAs(UnmanagedType.IUnknown)] object pDevice,
		[MarshalAs(UnmanagedType.Bool)] bool Exclusive);

	[PreserveSig]
	void ReleaseOwnership();

	[PreserveSig]
	int GetGammaControlCapabilities(
		out DxgiGammaControlCapabilities pGammaCaps);

	[PreserveSig]
	int SetGammaControl(
		in DxgiGammaControl pArray);

	[PreserveSig]
	int GetGammaControl(
		out DxgiGammaControl pArray);

	[PreserveSig]
	int SetDisplaySurface(
		IDXGISurface pScanoutSurface);

	[PreserveSig]
	int GetDisplaySurfaceData(
		IDXGISurface pDestination);

	[PreserveSig]
	int GetFrameStatistics(
		out DxgiFrameStatistics pStats);
}
