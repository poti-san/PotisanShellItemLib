namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("45f4f6f3-d602-4f98-9a8a-3efa152ad2d3")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IInstallationCompletedCallback
{
	[PreserveSig]
	int Invoke(
		IInstallationJob installationJob,
		IInstallationCompletedCallbackArgs callbackArgs);
}