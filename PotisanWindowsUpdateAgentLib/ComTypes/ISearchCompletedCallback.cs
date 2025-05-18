namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("88aee058-d4b0-4725-a2f1-814a67ae964c")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ISearchCompletedCallback
{
	[PreserveSig]
	int Invoke(
		ISearchJob searchJob,
		ISearchCompletedCallbackArgs? callbackArgs);
}