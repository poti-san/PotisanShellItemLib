namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("bf94c121-5b05-4e6f-8000-ba598961414d")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFTransform
{
	[PreserveSig]
	int GetStreamLimits(
		out uint pdwInputMinimum,
		out uint pdwInputMaximum,
		out uint pdwOutputMinimum,
		out uint pdwOutputMaximum);

	[PreserveSig]
	int GetStreamCount(
		out uint pcInputStreams,
		out uint pcOutputStreams);

	[PreserveSig]
	int GetStreamIDs(
		uint dwInputIDArraySize,
		[MarshalAs(UnmanagedType.LPArray)] uint[] pdwInputIDs,
		uint dwOutputIDArraySize,
		[MarshalAs(UnmanagedType.LPArray)] uint[] pdwOutputIDs);

	[PreserveSig]
	int GetInputStreamInfo(
		uint dwInputStreamID,
		MFTInputStreamInfo pStreamInfo);

	[PreserveSig]
	int GetOutputStreamInfo(
		uint dwOutputStreamID,
		MFTOutputStreamInfo pStreamInfo);

	[PreserveSig]
	int GetAttributes(
		out IMFAttributes? pAttributes);

	[PreserveSig]
	int GetInputStreamAttributes(
		uint dwInputStreamID,
		out IMFAttributes? pAttributes);

	[PreserveSig]
	int GetOutputStreamAttributes(
		uint dwOutputStreamID,
		out IMFAttributes? pAttributes);

	[PreserveSig]
	int DeleteInputStream(
		uint dwStreamID);

	[PreserveSig]
	int AddInputStreams(
		uint cStreams,
		in uint adwStreamIDs);

	[PreserveSig]
	int GetInputAvailableType(
		uint dwInputStreamID,
		uint dwTypeIndex,
		out IMFMediaType? ppType);

	[PreserveSig]
	int GetOutputAvailableType(
		uint dwOutputStreamID,
		uint dwTypeIndex,
		out IMFMediaType? ppType);

	[PreserveSig]
	int SetInputType(
		uint dwInputStreamID,
		IMFMediaType pType,
		uint dwFlags);

	[PreserveSig]
	int SetOutputType(
		uint dwOutputStreamID,
		IMFMediaType pType,
		uint dwFlags);

	[PreserveSig]
	int GetInputCurrentType(
		uint dwInputStreamID,
		out IMFMediaType? ppType);

	[PreserveSig]
	int GetOutputCurrentType(
		uint dwOutputStreamID,
		out IMFMediaType? ppType);

	[PreserveSig]
	int GetInputStatus(
		uint dwInputStreamID,
		out uint pdwFlags);

	[PreserveSig]
	int GetOutputStatus(
		out uint pdwFlags);

	[PreserveSig]
	int SetOutputBounds(
		long hnsLowerBound,
		long hnsUpperBound);

	[PreserveSig]
	int ProcessEvent(
		uint dwInputStreamID,
		IMFMediaEvent pEvent);

	[PreserveSig]
	int ProcessMessage(
		MFTMessageType eMessage,
		nuint ulParam);

	[PreserveSig]
	int ProcessInput(
		uint dwInputStreamID,
		IMFSample pSample,
		uint dwFlags);

	[PreserveSig]
	int ProcessOutput(
		uint dwFlags,
		uint cOutputBufferCount,
		[MarshalAs(UnmanagedType.LPArray)][In][Out] MFTOutputDataBuffer[] pOutputSamples,
		out uint pdwStatus);
}
