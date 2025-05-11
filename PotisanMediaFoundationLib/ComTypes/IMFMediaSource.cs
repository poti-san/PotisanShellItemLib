using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("279a808d-aec7-40c8-9c6b-a6b492c78a66")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaSource // IMFMediaEventGenerator
{
	#region IMFMediaEventGenerator

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

	#endregion

	[PreserveSig]
	int GetCharacteristics(
		out uint pdwCharacteristics);

	[PreserveSig]
	int CreatePresentationDescriptor(
		out IMFPresentationDescriptor ppPresentationDescriptor);

	[PreserveSig]
	int Start(
		IMFPresentationDescriptor pPresentationDescriptor,
		in Guid pguidTimeFormat,
		PropVariant pvarStartPosition);

#pragma warning disable CA1716 // 識別子はキーワードと同一にすることはできません
	[PreserveSig]
	int Stop();
#pragma warning restore CA1716 // 識別子はキーワードと同一にすることはできません

	[PreserveSig]
	int Pause();

	[PreserveSig]
	int Shutdown();
}