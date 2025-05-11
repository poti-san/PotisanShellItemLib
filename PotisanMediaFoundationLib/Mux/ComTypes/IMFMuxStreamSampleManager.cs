using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation.Mux.ComTypes;

[ComImport]
[Guid("74ABBC19-B1CC-4E41-BB8B-9D9B86A8F6CA")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMuxStreamSampleManager
{
	[PreserveSig]
	int GetStreamCount(
		out uint pdwMuxStreamCount);

	[PreserveSig]
	int GetSample(
		uint dwMuxStreamIndex,
		out IMFSample ppSample);

	[PreserveSig]
	ulong GetStreamConfiguration();
}
