namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00000001-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IClassFactory
{
	[PreserveSig]
	int CreateInstance(
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnkOuter,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvObject);

	[PreserveSig]
	int LockServer(
		[MarshalAs(UnmanagedType.Bool)] bool fLock);
}