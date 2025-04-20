using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

// TODO 実装
public class MFMediaEventQueue(object? o) : ComUnknownWrapperBase<IMFMediaEventQueue>(o)
{
	//[PreserveSig]
	//int GetEvent(
	//	uint dwFlags,
	//	out IMFMediaEvent ppEvent);

	//[PreserveSig]
	//int BeginGetEvent(
	//	IMFAsyncCallback pCallback,
	//	[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	//[PreserveSig]
	//int EndGetEvent(
	//	IMFAsyncResult pResult,
	//	out IMFMediaEvent ppEvent);

	//[PreserveSig]
	//int QueueEvent(
	//	IMFMediaEvent pEvent);

	//[PreserveSig]
	//int QueueEventParamVar(
	//	MediaEventType met,
	//	in Guid guidExtendedType,
	//	int hrStatus,
	//	PropVariant pvValue);

	//[PreserveSig]
	//int QueueEventParamUnk(
	//	MediaEventType met,
	//	in Guid guidExtendedType,
	//	int hrStatus,
	//	[MarshalAs(UnmanagedType.IUnknown)] object? pUnk);

	//[PreserveSig]
	//int Shutdown();
}
