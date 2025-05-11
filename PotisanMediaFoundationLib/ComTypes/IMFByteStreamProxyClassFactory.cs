namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("a6b43f84-5c0a-42e8-a44d-b1857a76992f")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFByteStreamProxyClassFactory
{
	[PreserveSig]
	int CreateByteStreamProxy(
		IMFByteStream pByteStream,
		IMFAttributes? pAttributes,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvObject);
}