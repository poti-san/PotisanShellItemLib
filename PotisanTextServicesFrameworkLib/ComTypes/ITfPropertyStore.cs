using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("6834b120-88cb-11d2-bf45-00105a2799b5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfPropertyStore
{
	[PreserveSig]
	int GetType(
		out Guid pguid);

	[PreserveSig]
	int GetDataType(
		out uint pdwReserved);

	[PreserveSig]
	int GetData(
		[MarshalAs(UnmanagedType.Struct)] out object? pvarValue);

	[PreserveSig]
	int OnTextUpdated(
		uint dwFlags,
		ITfRange? pRangeNew,
		[MarshalAs(UnmanagedType.Bool)] out bool pfAccept);

	[PreserveSig]
	int Shrink(
		ITfRange? pRangeNew,
		[MarshalAs(UnmanagedType.Bool)] out bool pfFree);

	[PreserveSig]
	int Divide(
		ITfRange? pRangeThis,
		ITfRange? pRangeNew,
		out ITfPropertyStore ppPropStore);

	[PreserveSig]
	int Clone(
		out ITfPropertyStore pPropStore);

	[PreserveSig]
	int GetPropertyRangeCreator(
		out Guid pclsid);

	[PreserveSig]
	int Serialize(
		IStream pStream,
		out uint pcb);
}