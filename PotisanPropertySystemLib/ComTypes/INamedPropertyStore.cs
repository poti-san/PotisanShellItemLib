namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("71604b0f-97b0-4764-8577-2f13e98a1422")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface INamedPropertyStore
{
	[PreserveSig]
	int GetNamedValue(
		[MarshalAs(UnmanagedType.LPWStr)] string pszName,
		[Out] PropVariant ppropvar);

	[PreserveSig]
	int SetNamedValue(
		[MarshalAs(UnmanagedType.LPWStr)] string pszName,
		PropVariant propvar);

	[PreserveSig]
	int GetNameCount(
		out uint pdwCount);

	[PreserveSig]
	int GetNameAt(
		uint iProp,
		[MarshalAs(UnmanagedType.BStr)] out string pbstrName);
}