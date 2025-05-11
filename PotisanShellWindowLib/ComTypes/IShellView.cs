namespace Potisan.Windows.Shell.Window.ComTypes;

[ComImport]
[Guid("000214E3-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellView // IOleWindow
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
	int TranslateAccelerator(
		in Msg pmsg);

	[PreserveSig]
	int EnableModeless(
		[MarshalAs(UnmanagedType.Bool)] bool fEnable);

	[PreserveSig]
	int UIActivate(
		uint uState);

	[PreserveSig]
	int Refresh();

	[PreserveSig]
	int CreateViewWindow(
		IShellView? psvPrevious,
		in FOLDERSETTINGS pfs,
		IShellBrowser psb,
		in RECT prcView,
		out nint phWnd);

	[PreserveSig]
	int DestroyViewWindow();

	[PreserveSig]
	int GetCurrentInfo(
		out FOLDERSETTINGS pfs);

	[PreserveSig]
	int AddPropertySheetPages(
		uint dwReserved,
		AddPropSheetPage pfn,
		nint lparam);

	[PreserveSig]
	int SaveViewState();

	[PreserveSig]
	int SelectItem(
		nint pidlItem,
		ShellViewSelectItemFlag uFlags);

	[PreserveSig]
	int GetItemObject(
		uint uItem,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);
}

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
[return: MarshalAs(UnmanagedType.Bool)]
public delegate bool AddPropSheetPage(nint hpspage, nint lParam);