namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("aa80e7fd-2021-11d2-93e0-0060b067b86e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfContext
{
	[PreserveSig]
	int RequestEditSession(
		TFClientID tid,
		ITfEditSession? pes,
		uint dwFlags,
		out int phrSession);

	[PreserveSig]
	int InWriteSession(
		TFClientID tid,
		[MarshalAs(UnmanagedType.Bool)] out bool pfWriteSession);

	[PreserveSig]
	int GetSelection(
		TFEditCookie ec,
		uint ulIndex,
		uint ulCount,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] out TFSelection[] pSelection,
		out uint pcFetched);

	[PreserveSig]
	int SetSelection(
		TFEditCookie ec,
		uint ulCount,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] TFSelection[] pSelection);

	[PreserveSig]
	int GetStart(
		TFEditCookie ec,
		out ITfRange ppStart);

	[PreserveSig]
	int GetEnd(
		TFEditCookie ec,
		out ITfRange ppEnd);

	[PreserveSig]
	int GetActiveView(
		out ITfContextView ppView);

	[PreserveSig]
	int EnumViews(
		out IEnumTfContextViews ppEnum);

	[PreserveSig]
	int GetStatus(
		out TF_STATUS pdcs);

	[PreserveSig]
	int GetProperty(
		in Guid guidProp,
		out ITfProperty ppProp);

	[PreserveSig]
	int GetAppProperty(
		in Guid guidProp,
		out ITfReadOnlyProperty ppProp);

	[PreserveSig]
	int TrackProperties(
		[MarshalAs(UnmanagedType.LPArray)] Guid[] prgProp,
		uint cProp,
		[MarshalAs(UnmanagedType.LPArray)] Guid[] prgAppProp,
		uint cAppProp,
		out ITfReadOnlyProperty ppProperty);

	[PreserveSig]
	int EnumProperties(
		out IEnumTfProperties ppEnum);

	[PreserveSig]
	int GetDocumentMgr(
		out ITfDocumentMgr ppDm);

	[PreserveSig]
	int CreateRangeBackup(
		TFEditCookie ec,
		ITfRange? pRange,
		out ITfRangeBackup ppBackup);
}
