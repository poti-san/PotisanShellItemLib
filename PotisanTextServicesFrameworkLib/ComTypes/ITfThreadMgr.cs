namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("aa80e801-2021-11d2-93e0-0060b067b86e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfThreadMgr
{
	[PreserveSig]
	int Activate(
		out TFClientID ptid);

	[PreserveSig]
	int Deactivate();

	[PreserveSig]
	int CreateDocumentMgr(
		out ITfDocumentMgr ppdim);

	[PreserveSig]
	int EnumDocumentMgrs(
		out IEnumTfDocumentMgrs ppEnum);

	[PreserveSig]
	int GetFocus(
		out ITfDocumentMgr ppdimFocus);

	[PreserveSig]
	int SetFocus(
		ITfDocumentMgr pdimFocus);

	[PreserveSig]
	int AssociateFocus(
		nint hwnd,
		ITfDocumentMgr pdimNew,
		out ITfDocumentMgr ppdimPrev);

	[PreserveSig]
	int IsThreadFocus(
		[MarshalAs(UnmanagedType.Bool)] out bool pfThreadFocus);

	[PreserveSig]
	int GetFunctionProvider(
		in Guid clsid,
		out ITfFunctionProvider ppFuncProv);

	[PreserveSig]
	int EnumFunctionProviders(
		out IEnumTfFunctionProviders ppEnum);

	[PreserveSig]
	int GetGlobalCompartment(
		out ITfCompartmentMgr ppCompMgr);
}