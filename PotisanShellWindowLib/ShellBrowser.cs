using System.ComponentModel;

using Potisan.Windows.Shell.Window.ComTypes;

namespace Potisan.Windows.Shell.Window;

public class ShellBrowser(object? o) : OleWindow(o)
{
	private new readonly IShellBrowser _obj = o != null ? (IShellBrowser)o : null!;

#pragma warning disable CA1816
	public override void Dispose()
	{
		Marshal.FinalReleaseComObject(_obj);
		base.Dispose();
	}
#pragma warning restore CA1816

	public ComResult InsertMenusNoThrow(nint sharedMenuHandle, int width1, int width2, int width3, int width4, int width5, int width6)
		=> new(_obj.InsertMenusSB(sharedMenuHandle, new()
		{
			width1 = width1,
			width2 = width2,
			width3 = width3,
			width4 = width4,
			width5 = width5,
			width6 = width6
		}));

	public void InsertMenus(nint sharedMenuHandle, int width1, int width2, int width3, int width4, int width5, int width6)
		=> InsertMenusNoThrow(sharedMenuHandle, width1, width2, width3, width4, width5, width6);

	public ComResult SetMenuNoThrow(nint sharedMenuHandle, nint oleMenuResHandle, nint activeObjectWindowHandle)
		=> new(_obj.SetMenu(sharedMenuHandle, oleMenuResHandle, activeObjectWindowHandle));

	public void SetMenu(nint sharedMenuHandle, nint oleMenuResHandle, nint activeObjectWindowHandle)
		=> SetMenuNoThrow(sharedMenuHandle, oleMenuResHandle, activeObjectWindowHandle).ThrowIfError();

	public ComResult RemoveMenusNoThrow(nint sharedMenuHandle)
		=> new(_obj.RemoveMenusSB(sharedMenuHandle));

	public void RemoveMenus(nint sharedMenuHandle)
		=> RemoveMenusNoThrow(sharedMenuHandle).ThrowIfError();

	public ComResult SetStatusTextNoThrow(string? statusText)
		=> new(_obj.SetStatusTextSB(statusText));

	public void SetStatusText(string? statusText)
		=> SetStatusTextNoThrow(statusText).ThrowIfError();

	public ComResult EnableModelessNoThrow(bool enable)
		=> new(_obj.EnableModelessSB(enable));

	public void EnableModeless(bool enable)
		=> EnableModelessNoThrow(enable).ThrowIfError();

	public ComResult TranslateAcceleratorNoThrow(in Msg msg, ushort id)
		=> new(_obj.TranslateAcceleratorSB(msg, id));

	public void TranslateAccelerator(in Msg msg, ushort id)
		=> TranslateAcceleratorNoThrow(msg, id).ThrowIfError();

	public ComResult BrowseObjectNoThrow(SafeHandle pidl, ShellBrowserBrowseFlag flags)
		=> new(_obj.BrowseObject(pidl.DangerousGetHandle(), (uint)flags));

	public void BrowseObject(SafeHandle pidl, ShellBrowserBrowseFlag flags)
		=> BrowseObjectNoThrow(pidl, flags).ThrowIfError();

	public ComResult<ComStream> GetViewStateStreamNoThrow(ComStorageMode mode)
		=> new(_obj.GetViewStateStream((uint)mode, out var x), new(x));

	public ComStream GetViewStateStream(ComStorageMode mode)
		=> GetViewStateStreamNoThrow(mode).Value;

	private const uint FCW_STATUS = 0x0001;
	private const uint FCW_TOOLBAR = 0x0002;
	private const uint FCW_TREE = 0x0003;
	private const uint FCW_INTERNETBAR = 0x0006;
	private const uint FCW_PROGRESS = 0x0008;

	private ComResult<nint> GetControlWindowNoThrow(uint id)
		=> new(_obj.GetControlWindow(id, out var x), x);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> StatusWindowHandleNoThrow => GetControlWindowNoThrow(FCW_STATUS);
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> ToolBarWindowHandleNoThrow => GetControlWindowNoThrow(FCW_TOOLBAR);
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> TreeViewWindowHandleNoThrow => GetControlWindowNoThrow(FCW_TREE);
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> InternetBarWindowHandleNoThrow => GetControlWindowNoThrow(FCW_INTERNETBAR);
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> ProgressBarWindowHandleNoThrow => GetControlWindowNoThrow(FCW_PROGRESS);

	public nint StatusWindowHandle => StatusWindowHandleNoThrow.Value;
	public nint ToolBarWindowHandle => ToolBarWindowHandleNoThrow.Value;
	public nint TreeViewWindowHandle => TreeViewWindowHandleNoThrow.Value;
	public nint InternetBarWindowHandle => InternetBarWindowHandleNoThrow.Value;
	public nint ProgressBarWindowHandle => ProgressBarWindowHandleNoThrow.Value;

	public ComResult<nint> SendControlMsgNoThrow(uint id, uint msg, nint wParam, nint lParam)
		=> new(_obj.SendControlMsg(id, msg, wParam, lParam, out var x), x);

	public ComResult<nint> SendMessageToStatusWindowNoThrow(uint msg, nint wParam, nint lParam)
		=> SendControlMsgNoThrow(FCW_STATUS, msg, wParam, lParam);
	public ComResult<nint> SendMessageToToolBarWindowNoThrow(uint msg, nint wParam, nint lParam)
		=> SendControlMsgNoThrow(FCW_TOOLBAR, msg, wParam, lParam);
	public ComResult<nint> SendMessageToTreeViewWindowNoThrow(uint msg, nint wParam, nint lParam)
		=> SendControlMsgNoThrow(FCW_TREE, msg, wParam, lParam);
	public ComResult<nint> SendMessageToInternetBarWindowNoThrow(uint msg, nint wParam, nint lParam)
		=> SendControlMsgNoThrow(FCW_INTERNETBAR, msg, wParam, lParam);
	public ComResult<nint> SendMessageToProgressBarWindowNoThrow(uint msg, nint wParam, nint lParam)
		=> SendControlMsgNoThrow(FCW_PROGRESS, msg, wParam, lParam);

	public nint SendMessageToStatusWindow(uint msg, nint wParam, nint lParam)
		=> SendMessageToStatusWindowNoThrow(msg, wParam, lParam).Value;
	public nint SendMessageToToolBarWindow(uint msg, nint wParam, nint lParam)
		=> SendMessageToToolBarWindowNoThrow(msg, wParam, lParam).Value;
	public nint SendMessageToTreeViewWindow(uint msg, nint wParam, nint lParam)
		=> SendMessageToTreeViewWindowNoThrow(msg, wParam, lParam).Value;
	public nint SendMessageToInternetBarWindow(uint msg, nint wParam, nint lParam)
		=> SendMessageToInternetBarWindowNoThrow(msg, wParam, lParam).Value;
	public nint SendMessageToProgressBarWindow(uint msg, nint wParam, nint lParam)
		=> SendMessageToProgressBarWindowNoThrow(msg, wParam, lParam).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellView> ActiveShellViewNoThrow
		=> new(_obj.QueryActiveShellView(out var x), new(x));

	public ShellView ActiveShellView
		=> ActiveShellViewNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<FolderView> ActiveShellViewAsFolderViewNoThrow
		=> ActiveShellViewNoThrow switch
		{
			{ Succeeded: true, ValueUnchecked: var value } => value.AsFolderViewNoThrow,
			{ HResult: var hr } => new(hr, new(null!)),
		};

	public FolderView ActiveShellViewAsFolderView
		=> ActiveShellViewAsFolderViewNoThrow.Value;

	public ComResult OnViewWindowActiveNoThrow(ShellView view)
		=> new(_obj.OnViewWindowActive((IShellView)view.WrappedObject!));

	public void OnViewWindowActive(ShellView view)
		=> OnViewWindowActiveNoThrow(view).ThrowIfError();

	public ComResult SetToolbarItemsNoThrow(ReadOnlySpan<nint> buttons, ShellBrowserToolbarFlag flags)
		=> new(_obj.SetToolbarItems(in MemoryMarshal.GetReference(buttons), (uint)buttons.Length, (uint)flags));

	public void SetToolbarItems(ReadOnlySpan<nint> buttons, ShellBrowserToolbarFlag flags)
		=> SetToolbarItemsNoThrow(buttons, flags).ThrowIfError();
}

[Flags]
public enum ShellBrowserBrowseFlag : uint
{
	DefaultBrowser = 0x0000,
	SameBrowser = 0x0001,
	NewBrowser = 0x0002,
	DefaultMode = 0x0000,
	OpenMode = 0x0010,
	ExploreMode = 0x0020,
	HelpMode = 0x0040,
	NoTransferHistory = 0x0080,
	Absolute = 0x0000,
	Relative = 0x1000,
	Parent = 0x2000,
	NavigateBack = 0x4000,
	NavigateForward = 0x8000,
	AllowAutoNavigate = 0x00010000,
	KeepSameTemplate = 0x00020000,
	KeepWordSheelText = 0x00040000,
	ActivateNoFocus = 0x00080000,
	CreateNoHistory = 0x00100000,
	PlayNoSound = 0x00200000,
	CallerUntrusted = 0x00800000,
	TrustFirstDownload = 0x01000000,
	UntrustedForDownload = 0x02000000,
	NoAutoSelect = 0x04000000,
	WriteNoHistory = 0x08000000,
	TrustedForActiveX = 0x10000000,
	FeedNavigation = 0x20000000,
	Redirect = 0x40000000,
	InitiatedByHLinkFrame = 0x80000000,
}

[Flags]
public enum ShellBrowserToolbarFlag : uint
{
	Merge = 0x0001,
	/// <summary>
	/// 未使用。
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	Configable = 0x0002,
	AddToEnd = 0x0004,
}