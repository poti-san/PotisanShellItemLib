#pragma warning disable CA1716 // 識別子はキーワードと同一にすることはできません

using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("90377834-21D0-4dee-8214-BA2E3E6C1127")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaSession // IMFMediaEventGenerator
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

	#endregion // IMFMediaEventGenerator

	[PreserveSig]
	int SetTopology(
		uint dwSetTopologyFlags,
		IMFTopology pTopology);

	[PreserveSig]
	int ClearTopologies();

	[PreserveSig]
	int Start(
		in Guid pguidTimeFormat,
		PropVariant pvarStartPosition);

	[PreserveSig]
	int Pause();

	[PreserveSig]
	int Stop();

	[PreserveSig]
	int Close();

	[PreserveSig]
	int Shutdown();

	[PreserveSig]
	int GetClock(
		out IMFClock ppClock);

	[PreserveSig]
	int GetSessionCapabilities(
		out uint pdwCaps);

	[PreserveSig]
	int GetFullTopology(
		uint dwGetFullTopologyFlags,
		ulong TopoId,
		out IMFTopology ppFullTopology);
}