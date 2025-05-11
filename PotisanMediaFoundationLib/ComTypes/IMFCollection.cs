namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("5BC8A76B-869A-46a3-9B03-FA218A66AEBE")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFCollection
{
	[PreserveSig]
	int GetElementCount(
		out uint pcElements);

	[PreserveSig]
	int GetElement(
		uint dwElementIndex,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement);

	[PreserveSig]
	int AddElement(
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnkElement);

	[PreserveSig]
	int RemoveElement(
		uint dwElementIndex,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppUnkElement);

	[PreserveSig]
	int InsertElementAt(
		uint dwIndex,
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnknown);

	[PreserveSig]
	int RemoveAllElements();
}
