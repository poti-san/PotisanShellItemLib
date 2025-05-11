using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("2CD0BD52-BCD5-4B89-B62C-EADC0C031E7D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaEventGenerator
{
	[PreserveSig]
	int GetEvent(
		uint dwFlags,
		out IMFMediaEvent? ppEvent);

	[PreserveSig]
	int BeginGetEvent(
		IMFAsyncCallback pCallback,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	[PreserveSig]
	int EndGetEvent(
		IMFAsyncResult pResult,
		out IMFMediaEvent? ppEvent);

	[PreserveSig]
	int QueueEvent(
		MediaEventType met,
		in Guid guidExtendedType,
		int hrStatus,
		PropVariant pvValue);
}
