namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("e2449660-9542-11d2-bf46-00105a2799b5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfProperty // ITfReadOnlyProperty
{
	#region ITfReadOnlyProperty

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

	#endregion ITfReadOnlyProperty

	[PreserveSig]
	int FindRange(
			TFEditCookie ec,
			ITfRange? pRange,
			out ITfRange ppRange,
			TFAnchor aPos);

	[PreserveSig]
	int SetValueStore(
		TFEditCookie ec,
		ITfRange? pRange,
		ITfPropertyStore pPropStore);

	[PreserveSig]
	int SetValue(
		TFEditCookie ec,
		ITfRange? pRange,
		[MarshalAs(UnmanagedType.Struct)] in object pvarValue);

	[PreserveSig]
	int Clear(
		TFEditCookie ec,
		ITfRange? pRange);
}