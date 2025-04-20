using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("e56e4cbd-8f70-49d8-a0f8-edb3d6ab9bf2")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFTimer
{
	[PreserveSig]
	int SetTimer(
		uint dwFlags,
		long llClockTime,
		IMFAsyncCallback pCallback,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkState,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppunkKey);

	[PreserveSig]
	int CancelTimer(
		[MarshalAs(UnmanagedType.IUnknown)] object? punkKey);
}
