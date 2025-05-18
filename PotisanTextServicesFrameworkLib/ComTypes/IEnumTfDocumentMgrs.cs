namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("aa80e808-2021-11d2-93e0-0060b067b86e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumTfDocumentMgrs
{
	[PreserveSig]
	int Clone(
		out IEnumTfDocumentMgrs ppEnum);

	[PreserveSig]
	int Next(
		uint ulCount,
		out ITfDocumentMgr rgDocumentMgr,
		out uint pcFetched);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Skip(
		uint ulCount);
}