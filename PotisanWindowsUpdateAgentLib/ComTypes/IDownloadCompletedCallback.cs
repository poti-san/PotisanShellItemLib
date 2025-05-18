namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("77254866-9f5b-4c8e-b9e2-c77a8530d64b")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDownloadCompletedCallback
{
	[PreserveSig]
	int Invoke(
		IDownloadJob downloadJob,
		IDownloadCompletedCallbackArgs callbackArgs);
}