namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("aa80e7f4-2021-11d2-93e0-0060b067b86e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfDocumentMgr
{
	[PreserveSig]
	int CreateContext(
		TFClientID tidOwner,
		uint dwFlags,
		[MarshalAs(UnmanagedType.IUnknown)] object? punk,
		out ITfContext ppic,
		out TFEditCookie pecTextStore);

	[PreserveSig]
	int Push(
		ITfContext? pic);

	[PreserveSig]
	int Pop(
		uint dwFlags);

	[PreserveSig]
	int GetTop(
		out ITfContext ppic);

	[PreserveSig]
	int GetBase(
		out ITfContext ppic);

	[PreserveSig]
	int EnumContexts(
		out IEnumTfContexts ppEnum);
}
