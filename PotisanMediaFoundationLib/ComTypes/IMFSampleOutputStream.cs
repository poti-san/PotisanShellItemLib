using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("8feed468-6f7e-440d-869a-49bdd283ad0d")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFSampleOutputStream
{
	[PreserveSig]
	int BeginWriteSample(
		IMFSample pSample,
		IMFAsyncCallback? pCallback,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	[PreserveSig]
	int EndWriteSample(
		IMFAsyncResult pResult);

	[PreserveSig]
	int Close();
}