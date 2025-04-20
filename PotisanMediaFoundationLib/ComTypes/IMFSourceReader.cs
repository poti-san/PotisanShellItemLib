namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("70ae66f2-c809-4e4f-8915-bdcb406b7993")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFSourceReader
{
	[PreserveSig]
	int GetStreamSelection(
		uint dwStreamIndex,
		[MarshalAs(UnmanagedType.Bool)] out bool pfSelected);

	[PreserveSig]
	int SetStreamSelection(
		uint dwStreamIndex,
		[MarshalAs(UnmanagedType.Bool)] bool fSelected);

	[PreserveSig]
	int GetNativeMediaType(
		uint dwStreamIndex,
		uint dwMediaTypeIndex,
		out IMFMediaType? ppMediaType);

	[PreserveSig]
	int GetCurrentMediaType(
		uint dwStreamIndex,
		out IMFMediaType? ppMediaType);

	[PreserveSig]
	int SetCurrentMediaType(
		uint dwStreamIndex,
		nint pdwReserved,
		IMFMediaType pMediaType);

	[PreserveSig]
	int SetCurrentPosition(
		in Guid guidTimeFormat,
		PropVariant varPosition);

	[PreserveSig]
	int ReadSample(
		uint dwStreamIndex,
		uint dwControlFlags,
		out uint pdwActualStreamIndex,
		out uint pdwStreamFlags,
		out long pllTimestamp,
		out IMFSample? ppSample);

	[PreserveSig]
	int Flush(
		uint dwStreamIndex);

	[PreserveSig]
	int GetServiceForStream(
		uint dwStreamIndex,
		in Guid guidService,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppvObject);

	[PreserveSig]
	int GetPresentationAttribute(
		uint dwStreamIndex,
		in Guid guidAttribute,
		[Out] PropVariant pvarAttribute);
}