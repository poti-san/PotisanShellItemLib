namespace Potisan.Windows.MediaFoundation.ComTypes;

//STDAPI MFCreateSimpleTypeHandler(
//	_Outptr_ IMFMediaTypeHandler ** ppHandler );
//typedef

[ComImport]
[Guid("e93dcf6c-4b07-4e1e-8123-aa16ed6eadf5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaTypeHandler
{
	[PreserveSig]
	int IsMediaTypeSupported(
		IMFMediaType pMediaType,
		out IMFMediaType? ppMediaType);

	[PreserveSig]
	int GetMediaTypeCount(
		out uint pdwTypeCount);

	[PreserveSig]
	int GetMediaTypeByIndex(
		uint dwIndex,
		out IMFMediaType? ppType);

	[PreserveSig]
	int SetCurrentMediaType(
		IMFMediaType pMediaType);

	[PreserveSig]
	int GetCurrentMediaType(
		out IMFMediaType? ppMediaType);

	[PreserveSig]
	int GetMajorType(
		out Guid pguidMajorType);
}