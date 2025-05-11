namespace Potisan.Windows.Shell.Window.ComTypes;

[ComImport]
[Guid("cde725b0-ccc9-4519-917e-325d72fab4ce")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IFolderView
{
	[PreserveSig]
	int GetCurrentViewMode(
		out uint pViewMode);

	[PreserveSig]
	int SetCurrentViewMode(
		uint ViewMode);

	[PreserveSig]
	int GetFolder(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int Item(
		int iItemIndex,
		out nint ppidl);

	[PreserveSig]
	int ItemCount(
		uint uFlags,
		out int pcItems);

	[PreserveSig]
	int Items(
		uint uFlags,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int GetSelectionMarkedItem(
		out int piItem);

	[PreserveSig]
	int GetFocusedItem(
		out int piItem);

	[PreserveSig]
	int GetItemPosition(
		nint pidl,
		out POINT ppt);

	[PreserveSig]
	int GetSpacing(
		out POINT ppt);

	[PreserveSig]
	int GetDefaultSpacing(
		out POINT ppt);

	[PreserveSig]
	int GetAutoArrange();

	[PreserveSig]
	int SelectItem(
		int iItem,
		uint dwFlags);

	[PreserveSig]
	int SelectAndPositionItems(
		uint cidl,
		[MarshalAs(UnmanagedType.LPArray)] nint[] apidl,
		[MarshalAs(UnmanagedType.LPArray)] POINT[] apt,
		uint dwFlags);
}