namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("1f9fc1d0-c39b-4b26-817f-011967d3440e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertyDescriptionList
{
	[PreserveSig]
	int GetCount(
		out uint pcElem);

	[PreserveSig]
	int GetAt(
		uint iElem,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);
}