namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("fa993888-4383-415a-a930-dd472a8cf6f7")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFGetService
{
	[PreserveSig]
	int GetService(
		in Guid guidService,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvObject);
}
