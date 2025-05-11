namespace Potisan.Windows.MediaFoundation.Async.ComTypes;

[ComImport]
[Guid("a27003cf-2354-4f2a-8d6a-ab7cff15437e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFAsyncCallback
{
	[PreserveSig]
	int GetParameters(
		out uint pdwFlags,
		out uint pdwQueue);

	[PreserveSig]
	int Invoke(
		IMFAsyncResult pAsyncResult);
}