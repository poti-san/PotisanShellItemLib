namespace Potisan.Windows.MediaFoundation.Async.ComTypes;

[ComImport]
[Guid("ac6b7889-0740-4d51-8619-905994a55cc6")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFAsyncResult
{
	[PreserveSig]
	int GetState(
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppunkState);

	[PreserveSig]
	int GetStatus();

	[PreserveSig]
	int SetStatus(
		int hrStatus);

	[PreserveSig]
	int GetObject(
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppObject);

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.IUnknown)]
	object? GetStateNoAddRef();
}