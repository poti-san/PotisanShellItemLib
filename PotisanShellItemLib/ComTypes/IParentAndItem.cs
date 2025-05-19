namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("b3a4b685-b685-4805-99d9-5dead2873236")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IParentAndItem
{
	[PreserveSig]
	int SetParentAndItem(
		nint pidlParent,
		[MarshalAs(UnmanagedType.IUnknown)] object/*IShellFolder*/ psf,
		nint pidlChild);

	[PreserveSig]
	int GetParentAndItem(
		out nint ppidlParent,
		[MarshalAs(UnmanagedType.IUnknown)] out object/*IShellFolder*/ ppsf,
		out nint ppidlChild);
}