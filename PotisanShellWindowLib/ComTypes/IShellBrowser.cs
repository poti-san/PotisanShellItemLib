using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Shell.Window.ComTypes;

[ComImport]
[Guid("000214E2-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellBrowser // IOleWindow
{
	#region IOleWindow

	[PreserveSig]
	int GetWindow(
		out nint phwnd);

	[PreserveSig]
	int ContextSensitiveHelp(
		[MarshalAs(UnmanagedType.Bool)] bool fEnterMode);

	#endregion IOleWindow

	[PreserveSig]
	int InsertMenusSB(
		nint hmenuShared,
		in OLEMENUGROUPWIDTHS lpMenuWidths);

	[PreserveSig]
	int SetMenu(
		nint hmenuShared,
		nint holemenuRes,
		nint hwndActiveObject);

	[PreserveSig]
	int RemoveMenusSB(
		nint hmenuShared);

	[PreserveSig]
	int SetStatusTextSB(
		[MarshalAs(UnmanagedType.LPWStr)] string? pszStatusText);

	[PreserveSig]
	int EnableModelessSB(
		[MarshalAs(UnmanagedType.Bool)] bool fEnable);

	[PreserveSig]
	int TranslateAcceleratorSB(
		in Msg pmsg,
		ushort wID);

	[PreserveSig]
	int BrowseObject(
		nint pidl,
		uint wFlags);

	[PreserveSig]
	int GetViewStateStream(
		uint grfMode,
		out IStream ppStrm);

	[PreserveSig]
	int GetControlWindow(
		uint id,
		out nint phwnd);

	[PreserveSig]
	int SendControlMsg(
		uint id,
		uint uMsg,
		nint wParam,
		nint lParam,
		out nint pret);

	[PreserveSig]
	int QueryActiveShellView(
		out IShellView ppshv);

	[PreserveSig]
	int OnViewWindowActive(
		IShellView pshv);

	[PreserveSig]
	int SetToolbarItems(
		in nint lpButtons,
		uint nButtons,
		uint uFlags);
}

public struct OLEMENUGROUPWIDTHS
{
	public int width1;
	public int width2;
	public int width3;
	public int width4;
	public int width5;
	public int width6;
}