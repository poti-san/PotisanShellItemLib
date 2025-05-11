namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("a27003d0-2354-4f2a-8d6a-ab7cff15437e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFRemoteAsyncCallback
{
	[PreserveSig]
	int Invoke(
		int hr,	[MarshalAs(UnmanagedType.IUnknown)] object? pRemoteResult);
}