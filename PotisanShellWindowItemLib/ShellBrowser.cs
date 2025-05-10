using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

public class ShellBrowser(object? o) : OleWindow(o)
{
	private new readonly IShellBrowser _obj = o != null ? (IShellBrowser)o : null!;

	public override void Dispose()
	{
		Marshal.FinalReleaseComObject(_obj);
		base.Dispose();
	}

	//[PreserveSig]
	//int InsertMenusSB(
	//	nint hmenuShared,
	//	in OLEMENUGROUPWIDTHS lpMenuWidths);

	//[PreserveSig]
	//int SetMenuSB(
	//	nint hmenuShared,
	//	nint holemenuRes,
	//	nint hwndActiveObject);

	//[PreserveSig]
	//int RemoveMenusSB(
	//	nint hmenuShared);

	//[PreserveSig]
	//int SetStatusTextSB(
	//	[MarshalAs(UnmanagedType.LPWStr)] string? pszStatusText);

	//[PreserveSig]
	//int EnableModelessSB(
	//	[MarshalAs(UnmanagedType.Bool)] bool fEnable);

	//[PreserveSig]
	//int TranslateAcceleratorSB(
	//	in MSG pmsg,
	//	ushort wID);

	//[PreserveSig]
	//int BrowseObject(
	//	nint pidl,
	//	uint wFlags);

	//[PreserveSig]
	//int GetViewStateStream(
	//	uint grfMode,
	//	out IStream ppStrm);

	//[PreserveSig]
	//int GetControlWindow(
	//	uint id,
	//	out nint phwnd);

	//[PreserveSig]
	//int SendControlMsg(
	//	uint id,
	//	uint uMsg,
	//	nint wParam,
	//	nint lParam,
	//	out nint pret);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellView> ActiveShellViewNoThrow
		=> new(_obj.QueryActiveShellView(out var x), new(x));

	public ShellView ActiveShellView
		=> ActiveShellViewNoThrow.Value;

	//[PreserveSig]
	//int OnViewWindowActive(
	//	IShellView pshv);

	//[PreserveSig]
	//int SetToolbarItems(
	//	[MarshalAs(UnmanagedType.LPArray)] nint[] lpButtons,
	//	uint nButtons,
	//	uint uFlags);
}
