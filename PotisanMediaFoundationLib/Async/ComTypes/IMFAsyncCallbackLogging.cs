namespace Potisan.Windows.MediaFoundation.Async.ComTypes;

[ComImport]
[Guid("c7a4dca1-f5f0-47b6-b92b-bf0106d25791")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFAsyncCallbackLogging // IMFAsyncCallback
{
	#region IMFAsyncCallback

	[PreserveSig]
	int GetParameters(
		out uint pdwFlags,
		out uint pdwQueue);

	[PreserveSig]
	int Invoke(
		IMFAsyncResult pAsyncResult);

	#endregion // IMFAsyncCallback

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.IUnknown)]
	object? GetObjectPointer();

	[PreserveSig]
	int GetObjectTag();
}