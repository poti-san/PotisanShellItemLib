using Potisan.Windows.Com.SafeHandles;
using Potisan.Windows.Shell.ComTypes;
using Potisan.Windows.Shell.Window.ComTypes;

namespace Potisan.Windows.Shell.Window;

/// <summary>
/// シェルのフォルダビュー。開いているフォルダや含まれる項目を管理します。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <see cref="IFolderView"/> COMインターフェイスのラッパーです。
/// </remarks>
public class FolderView(object? o) : ComUnknownWrapperBase<IFolderView>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<FolderViewMode> CurrentViewModeNoThrow
		=> new(_obj.GetCurrentViewMode(out var x), (FolderViewMode)x);

	public ComResult SetCurrentViewModeNoThrow(FolderViewMode value)
		=> new(_obj.SetCurrentViewMode((uint)value));

	public FolderViewMode CurrentViewMode
	{
		get => CurrentViewModeNoThrow.Value;
		set => SetCurrentViewModeNoThrow(value).ThrowIfError();
	}

	public ComResult<object> GetFolderNoThrow(in Guid iid)
		=> new(_obj.GetFolder(iid, out var x), x!);

	public object GetFolder(in Guid iid)
		=> GetFolderNoThrow(iid).Value;

	public ComResult<TWrapper> GetFolderNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(GetFolderNoThrow(typeof(TInterface).GUID));

	public TWrapper GetFolder<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetFolderNoThrow<TWrapper, TInterface>().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItem> FolderAsShellItemNoThrow
		=> GetFolderNoThrow<ShellItem, IShellItem>();

	public ShellItem FolderAsShellItem
		=> FolderAsShellItemNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItem2> FolderAsShellItem2NoThrow
		=> GetFolderNoThrow<ShellItem2, IShellItem2>();

	public ShellItem2 FolderAsShellItem2
		=> FolderAsShellItem2NoThrow.Value;

	public ComResult<SafeCoTaskMemHandle> GetItemIDListNoThrow(int index)
		=> new(_obj.Item(index, out var x), new(x, true));

	public SafeCoTaskMemHandle GetItemIDList(int index)
		=> GetItemIDListNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> SelectedItemCountNoThrow
		=> new(_obj.ItemCount((int)SVGIO.SELECTION, out var x), x);

	public int SelectedItemCount
		=> SelectedItemCountNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> AllViewItemCountNoThrow
		=> new(_obj.ItemCount((int)SVGIO.ALLVIEW, out var x), x);

	public int AllViewItemCount
		=> AllViewItemCountNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CheckedItemCountNoThrow
		=> new(_obj.ItemCount((int)SVGIO.CHECKED, out var x), x);

	public int CheckedItemCount
		=> CheckedItemCountNoThrow.Value;

	private ComResult<object> GetItemsNoThrow(SVGIO flags, in Guid iid)
		=> new(_obj.Items((uint)flags, iid, out var x), x!);

	private ComResult<TWrapper> GetItemsNoThrow<TWrapper, TInterface>(SVGIO flags)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(GetItemsNoThrow(flags, typeof(TInterface).GUID));

	public ComResult<object> GetAllItemsNoThrow(in Guid iid) => GetItemsNoThrow(SVGIO.ALLVIEW, iid);
	public ComResult<object> GetSelectedItemsNoThrow(in Guid iid) => GetItemsNoThrow(SVGIO.SELECTION, iid);
	public ComResult<object> GetCheckedItemsNoThrow(in Guid iid) => GetItemsNoThrow(SVGIO.CHECKED, iid);

	public object GetAllItems(in Guid iid) => GetAllItemsNoThrow(iid).Value;
	public object GetSelectedItems(in Guid iid) => GetSelectedItemsNoThrow(iid).Value;
	public object GetCheckedItems(in Guid iid) => GetCheckedItemsNoThrow(iid).Value;

	public ComResult<TWrapper> GetAllItemsNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetItemsNoThrow<TWrapper, TInterface>(SVGIO.ALLVIEW);
	public ComResult<TWrapper> GetSelectedItemsNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetItemsNoThrow<TWrapper, TInterface>(SVGIO.SELECTION);
	public ComResult<TWrapper> GetCheckedItemsNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetItemsNoThrow<TWrapper, TInterface>(SVGIO.CHECKED);

	public TWrapper GetAllItems<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetAllItemsNoThrow<TWrapper, TInterface>().Value;
	public TWrapper GetSelectedItems<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetSelectedItemsNoThrow<TWrapper, TInterface>().Value;
	public TWrapper GetCheckedItems<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetCheckedItemsNoThrow<TWrapper, TInterface>().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemArray> AllItemsNoThrow
		=> ShellItemArray.WrapNotFoundWithEmpty(GetAllItemsNoThrow<ShellItemArray, IShellItemArray>());
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemArray> SelectedItemsNoThrow
		=> ShellItemArray.WrapNotFoundWithEmpty(GetSelectedItemsNoThrow<ShellItemArray, IShellItemArray>());
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemArray> CheckedItemsNoThrow
		=> ShellItemArray.WrapNotFoundWithEmpty(GetCheckedItemsNoThrow<ShellItemArray, IShellItemArray>());

	public ShellItemArray AllItems => AllItemsNoThrow.Value;
	public ShellItemArray SelectedItems => SelectedItemsNoThrow.Value;
	public ShellItemArray CheckedItems => CheckedItemsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> SelectionMarkedItemIndexNoThrow
		=> new(_obj.GetSelectionMarkedItem(out var x), x);

	public int SelectionMarkedItemIndex
		=> SelectionMarkedItemIndexNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> FocusedItemIndexNoThrow
		=> new(_obj.GetFocusedItem(out var x), x);

	public int FocusedItemIndex
		=> FocusedItemIndexNoThrow.Value;

	public ComResult<(int X, int Y)> GetItemPositionNoThrow(SafeHandle pidl)
		=> new(_obj.GetItemPosition(pidl.DangerousGetHandle(), out var x), (x.x, x.y));

	public (int X, int Y) GetItemPosition(SafeHandle pidl)
		=> GetItemPositionNoThrow(pidl).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(int X, int Y)> SpacingNoThrow
		=> new(_obj.GetSpacing(out var x), (x.x, x.y));

	public (int X, int Y) Spacing
		=> SpacingNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(int X, int Y)> DefaultSpacingNoThrow
		=> new(_obj.GetDefaultSpacing(out var x), (x.x, x.y));

	public (int X, int Y) DefaultSpacing
		=> DefaultSpacingNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> AutoArrangeEnabledNoThrow
		=> ComResult.HRSuccess(_obj.GetAutoArrange());

	public bool AutoArrangeEnabled
		=> AutoArrangeEnabledNoThrow.Value;

	public ComResult SelectItemNoThrow(int index, ShellViewSelectItemFlag flags)
		=> new(_obj.SelectItem(index, (uint)flags));

	public void SelectItem(int index, ShellViewSelectItemFlag flags)
		=> SelectItemNoThrow(index, flags).ThrowIfError();

	public ComResult SelectAndPositionItemsNoThrow(SafeHandle[] pidls, (int X, int Y)[] positions, ShellViewSelectItemFlag flags)
	{
		if (pidls.Length != positions.Length)
			return new(CommonHResults.EInvalidArg);
		return new(_obj.SelectAndPositionItems((uint)pidls.Length,
			[.. pidls.Select(h => h.DangerousGetHandle())],
			[.. positions.Select(pt => new POINT { x = pt.X, y = pt.Y })],
			(uint)flags));
	}

	public void SelectAndPositionItems(SafeHandle[] pidls, (int X, int Y)[] positions, ShellViewSelectItemFlag flags)
		=> SelectAndPositionItemsNoThrow(pidls, positions, flags).ThrowIfError();
}

/// <summary>
/// <c>FOLDERVIEWMODE</c>
/// </summary>
public enum FolderViewMode
{
	Auto = -1,
	Icon = 1,
	SmallIcon = 2,
	List = 3,
	Details = 4,
	Thumbnail = 5,
	Tile = 6,
	ThumbStrip = 7,
	Content = 8,
}

/// <summary>
/// <c>SVSIF</c>
/// </summary>
[Flags]
public enum ShellViewSelectItemFlag : uint
{
	Deselect = 0,
	Select = 0x1,
	Edit = 0x3,
	DeselectOthers = 0x4,
	EnsureVisible = 0x8,
	Focused = 0x10,
	Translatept = 0x20,
	SelectionMark = 0x40,
	PositionItem = 0x80,
	Check = 0x100,
	Check2 = 0x200,
	KeyboardSelect = 0x401,
	NoTakeFocus = 0x40000000,
}