using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("36f846fc-2256-48b6-b58e-e2b638316581")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaEventQueue
{
	[PreserveSig]
	int GetEvent(
		uint dwFlags,
		out IMFMediaEvent ppEvent);

	[PreserveSig]
	int BeginGetEvent(
		IMFAsyncCallback pCallback,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	[PreserveSig]
	int EndGetEvent(
		IMFAsyncResult pResult,
		out IMFMediaEvent ppEvent);

	[PreserveSig]
	int QueueEvent(
		IMFMediaEvent pEvent);

	[PreserveSig]
	int QueueEventParamVar(
		MediaEventType met,
		in Guid guidExtendedType,
		int hrStatus,
		PropVariant pvValue);

	[PreserveSig]
	int QueueEventParamUnk(
		MediaEventType met,
		in Guid guidExtendedType,
		int hrStatus,
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnk);

	[PreserveSig]
	int Shutdown();
}
