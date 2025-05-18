namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("e01402d5-f8da-43ba-a012-38894bd048f1")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IInstallationProgressChangedCallback
{
	[PreserveSig]
	int Invoke(
		IInstallationJob installationJob,
		IInstallationProgressChangedCallbackArgs callbackArgs);
}