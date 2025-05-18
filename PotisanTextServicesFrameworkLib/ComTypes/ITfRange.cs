using System.Runtime.InteropServices.ComTypes;

namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("aa80e7ff-2021-11d2-93e0-0060b067b86e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfRange
{
	[PreserveSig]
	int GetText(
		TFEditCookie ec,
		uint dwFlags,
		[MarshalAs(UnmanagedType.I2)] ref char pchText,
		uint cchMax,
		out uint pcch);

	[PreserveSig]
	int SetText(
		TFEditCookie ec,
		uint dwFlags,
		[MarshalAs(UnmanagedType.I2)] ref char pchText,
		int cch);

	[PreserveSig]
	int GetFormattedText(
		TFEditCookie ec,
		out IDataObject ppDataObject);

	[PreserveSig]
	int GetEmbedded(
		TFEditCookie ec,
		in Guid rguidService,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppunk);

	[PreserveSig]
	int InsertEmbedded(
		TFEditCookie ec,
		uint dwFlags,
		IDataObject? pDataObject);

	[PreserveSig]
	int ShiftStart(
		TFEditCookie ec,
		int cchReq,
		out int pcch,
		in TF_HALTCOND pHalt);

	[PreserveSig]
	int ShiftEnd(
		TFEditCookie ec,
		int cchReq,
		out int pcch,
		in TF_HALTCOND pHalt);

	[PreserveSig]
	int ShiftStartToRange(
		TFEditCookie ec,
		ITfRange? pRange,
		TFAnchor aPos);

	[PreserveSig]
	int ShiftEndToRange(
		TFEditCookie ec,
		ITfRange? pRange,
		TFAnchor aPos);

	[PreserveSig]
	int ShiftStartRegion(
		TFEditCookie ec,
		TFShiftDirection dir,
		[MarshalAs(UnmanagedType.Bool)] out bool pfNoRegion);

	[PreserveSig]
	int ShiftEndRegion(
		TFEditCookie ec,
		TFShiftDirection dir,
		[MarshalAs(UnmanagedType.Bool)] out bool pfNoRegion);

	[PreserveSig]
	int IsEmpty(
		TFEditCookie ec,
		[MarshalAs(UnmanagedType.Bool)] out bool pfEmpty);

	[PreserveSig]
	int Collapse(
		TFEditCookie ec,
		TFAnchor aPos);

	[PreserveSig]
	int IsEqualStart(
		TFEditCookie ec,
		ITfRange? pWith,
		TFAnchor aPos,
		[MarshalAs(UnmanagedType.Bool)] out bool pfEqual);

	[PreserveSig]
	int IsEqualEnd(
		TFEditCookie ec,
		ITfRange? pWith,
		TFAnchor aPos,
		[MarshalAs(UnmanagedType.Bool)] out bool pfEqual);

	[PreserveSig]
	int CompareStart(
		TFEditCookie ec,
		ITfRange? pWith,
		TFAnchor aPos,
		out int plResult);

	[PreserveSig]
	int CompareEnd(
		TFEditCookie ec,
		ITfRange? pWith,
		TFAnchor aPos,
		out int plResult);

	[PreserveSig]
	int AdjustForInsert(
		TFEditCookie ec,
		uint cchInsert,
		[MarshalAs(UnmanagedType.Bool)] out bool pfInsertOk);

	[PreserveSig]
	int GetGravity(
		out TFGravity pgStart,
		out TFGravity pgEnd);

	[PreserveSig]
	int SetGravity(
		TFEditCookie ec,
		TFGravity gStart,
		TFGravity gEnd);

	[PreserveSig]
	int Clone(
		out ITfRange ppClone);

	[PreserveSig]
	int GetContext(
		out ITfContext ppContext);

}