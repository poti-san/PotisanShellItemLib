using Potisan.Windows.Shell.ComTypes;
using Potisan.Windows.Shell.Window.ComTypes;

namespace Potisan.Windows.Shell.Window;

public class ShellView(object? o) : ComUnknownWrapperBase<IShellView>(o) // : OleWindow(o)
{
	// IShellViewからIOleWindowインターフェイスの取得は失敗します。
	#region IOleWindow

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> WindowHandleNoThrow
		=> new(_obj.GetWindow(out var x), x);

	public nint WindowHandle
		=> WindowHandleNoThrow.Value;

	public ComResult ContextSensitiveHelpNoThrow(bool enterMode)
		=> new(_obj.ContextSensitiveHelp(enterMode));

	public void ContextSensitiveHelp(bool enterMode)
		=> ContextSensitiveHelpNoThrow(enterMode).ThrowIfError();

	#endregion

	private new readonly IShellView _obj = o != null ? (IShellView)o : null!;

#pragma warning disable CA1816
	public override void Dispose()
	{
		Marshal.FinalReleaseComObject(_obj);
		base.Dispose();
	}
#pragma warning restore CA1816

	public ComResult TranslateAcceleratorNoThrow(in Msg msg)
		=> new(_obj.TranslateAccelerator(msg));

	public void TranslateAccelerator(in Msg msg)
		=> TranslateAcceleratorNoThrow(msg).ThrowIfError();

	public ComResult EnableModelessNoThrow(bool enable)
		=> new(_obj.EnableModeless(enable));

	public void EnableModeless(bool enable)
		=> EnableModelessNoThrow(enable).ThrowIfError();

	public ComResult UIActivateNoThrow(ShellViewUIActivateStatus status)
		=> new(_obj.UIActivate((uint)status));

	public void UIActivate(ShellViewUIActivateStatus status)
		=> UIActivateNoThrow(status).ThrowIfError();

	public ComResult RefreshNoThrow()
		=> new(_obj.Refresh());

	public void Refresh()
		=> RefreshNoThrow().ThrowIfError();

	public ComResult<nint> CreateViewWindowNoThrow(ShellView? previous, ShellBrowser browser, FolderViewMode viewMode, FolderFlag flags)
		=> new(_obj.CreateViewWindow((IShellView?)previous?.WrappedObject, new FOLDERSETTINGS { ViewMode = (uint)viewMode, fFlags = (uint)flags },
			(IShellBrowser)browser.WrappedObject!, new(), out var x), x);

	public nint CreateViewWindow(ShellView? previous, ShellBrowser browser, FolderViewMode viewMode, FolderFlag flags)
		=> CreateViewWindowNoThrow(previous, browser, viewMode, flags).Value;

	public ComResult DestroyViewWindowNoThrow()
		=> new(_obj.DestroyViewWindow());

	public void DestroyViewWindow()
		=> DestroyViewWindowNoThrow().ThrowIfError();

	public ComResult<(FolderViewMode ViewMode, FolderFlag Flags)> CurrentInfoNoThrow
		=> new(_obj.GetCurrentInfo(out var x), ((FolderViewMode)x.ViewMode, (FolderFlag)x.fFlags));

	public (FolderViewMode ViewMode, FolderFlag Flags) CurrentInfo
		=> CurrentInfoNoThrow.Value;

	public ComResult AddPropertySheetPagesNoThrow(AddPropSheetPage fn, nint lParam)
		=> new(_obj.AddPropertySheetPages(0, fn, lParam));

	public void AddPropertySheetPages(AddPropSheetPage fn, nint lParam)
		=> AddPropertySheetPagesNoThrow(fn, lParam).ThrowIfError();

	public ComResult SaveViewStateNoThrow()
		=> new(_obj.SaveViewState());

	public void SaveViewState()
		=> SaveViewStateNoThrow().ThrowIfError();

	public ComResult SelectItemNoThrow(SafeHandle pidlItem, ShellViewSelectItemFlag flags)
		=> new(_obj.SelectItem(pidlItem.DangerousGetHandle(), flags));

	public void SelectItem(SafeHandle pidlItem, ShellViewSelectItemFlag flags)
		=> SelectItemNoThrow(pidlItem, flags).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemArray> SelectedItemsNoThrow
	{
		get
		{
			const int SVGIO_SELECTION = 0x1;
			return new(_obj.GetItemObject(SVGIO_SELECTION, typeof(IShellItemArray).GUID, out var x), new(x));
		}
	}

	public ShellItemArray SelectedItems
		=> SelectedItemsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemArray> AllItemsNoThrow
	{
		get
		{
			const int SVGIO_ALLVIEW = 0x2;
			return new(_obj.GetItemObject(SVGIO_ALLVIEW, typeof(IShellItemArray).GUID, out var x), new(x));
		}
	}

	public ShellItemArray AllItems
		=> AllItemsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemArray> CheckedItemsNoThrow
	{
		get
		{
			const int SVGIO_CHECKED = 0x3;
			return new(_obj.GetItemObject(SVGIO_CHECKED, typeof(IShellItemArray).GUID, out var x), new(x));
		}
	}

	public ShellItemArray CheckedItems
		=> CheckedItemsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<FolderView> AsFolderViewNoThrow
		=> _obj is IFolderView x ? new(CommonHResults.SOK, new(x)) : new(CommonHResults.ENoInterface, new(null));

	public FolderView AsFolderView
		=> AsFolderViewNoThrow.Value;
}

/// <summary>
/// <c>SVUIA_STATUS</c>
/// </summary>
public enum ShellViewUIActivateStatus
{
	Deactivate = 0,
	ActivateNoFocus = 1,
	ActivateFocus = 2,
	InPlaceActivate = 3,
}

/// <summary>
/// <c>FOLDERFLAGS</c>
/// </summary>
[Flags]
public enum FolderFlag : uint
{
	None = 0,
	AutoArrange = 0x1,
	AbbreviatedNames = 0x2,
	SnapToGrid = 0x4,
	OwnerData = 0x8,
	BestFitWindow = 0x10,
	Desktop = 0x20,
	SingleSelection = 0x40,
	NoSubFolders = 0x80,
	Transparent = 0x100,
	NoClientEdge = 0x200,
	NoScroll = 0x400,
	AlignLeft = 0x800,
	NoIcons = 0x1000,
	ShowSelectionAlways = 0x2000,
	NoVisible = 0x4000,
	SingleClickActivate = 0x8000,
	NoWebView = 0x10000,
	HideFileNames = 0x20000,
	CheckSelect = 0x40000,
	NoEnumRefresh = 0x80000,
	NoGrouping = 0x100000,
	FullRowSelect = 0x200000,
	NoFilters = 0x400000,
	NoColumnHeader = 0x800000,
	NoHeaderInAllViews = 0x1000000,
	ExtendedTiles = 0x2000000,
	TriCheckSelect = 0x4000000,
	AutoCheckSelect = 0x8000000,
	NoBrowserViewState = 0x10000000,
	SubsetGroups = 0x20000000,
	UseSearchFolder = 0x40000000,
	AllowTrlReading = 0x80000000,
}
