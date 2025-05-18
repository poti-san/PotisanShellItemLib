namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("17d49a3d-f8b8-4b2f-b254-52319dd64c53")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfReadOnlyProperty
{
	[PreserveSig]
	int GetType(
		out Guid pguid);

	[PreserveSig]
	int EnumRanges(
		TFEditCookie ec,
		out IEnumTfRanges? ppEnum,
		ITfRange? pTargetRange);

	[PreserveSig]
	int GetValue(
		TFEditCookie ec,
		ITfRange? pRange,
		[MarshalAs(UnmanagedType.Struct)] out object pvarValue);

	[PreserveSig]
	int GetContext(
		out ITfContext ppContext);
}