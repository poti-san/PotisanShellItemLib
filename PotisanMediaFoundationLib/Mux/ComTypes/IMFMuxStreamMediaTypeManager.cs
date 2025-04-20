using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation.Mux.ComTypes;

[ComImport]
[Guid("505A2C72-42F7-4690-AEAB-8F513D0FFDB8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMuxStreamMediaTypeManager
{
	[PreserveSig]
	int GetStreamCount(
		out uint pdwMuxStreamCount);

	[PreserveSig]
	int GetMediaType(
		uint dwMuxStreamIndex,
		out IMFMediaType ppMediaType);

	[PreserveSig]
	int GetStreamConfigurationCount(
		out uint pdwCount);

	[PreserveSig]
	int AddStreamConfiguration(
		ulong ullStreamMask);

	[PreserveSig]
	int RemoveStreamConfiguration(
		ulong ullStreamMask);

	[PreserveSig]
	int GetStreamConfiguration(
		uint ulIndex,
		out ulong pullStreamMask);
}