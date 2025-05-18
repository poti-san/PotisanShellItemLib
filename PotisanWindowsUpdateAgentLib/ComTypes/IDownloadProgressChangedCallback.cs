namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("8c3f1cdd-6173-4591-aebd-a56a53ca77c1")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDownloadProgressChangedCallback
{
	[PreserveSig]
	int Invoke(
		IDownloadJob downloadJob,
		IDownloadProgressChangedCallbackArgs? callbackArgs);
}
