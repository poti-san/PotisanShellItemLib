namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("a99400f4-3d84-4557-94ba-1242fb2cc9a6")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertyEnumTypeList
{
	[PreserveSig]
	int GetCount(
		out uint pctypes);

	[PreserveSig]
	int GetAt(
		uint itype,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int GetConditionAt(
		uint nIndex,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int FindMatchingIndex(
		PropVariant propvarCmp,
		out uint pnIndex);
}