namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IServiceProvider
{
	[PreserveSig]
	int QueryService(
		   in Guid guidService,
		   in Guid riid,
		   [MarshalAs(UnmanagedType.IUnknown)] out object? ppvObject);
}