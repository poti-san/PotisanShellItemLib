using System.Collections.Immutable;
using System.ComponentModel;

using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// シェルアイテム。ファイルシステムオブジェクトと特殊オブジェクトの名前や属性を取得できます。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <para><c>IShellItem</c> COMインターフェイスのラッパーです。</para>
/// <para><see cref="ShellItem.CreateKnownFolderItem(in Guid, Potisan.Windows.Shell.KnownFolderFlag)"/>等で作成できます。</para>
/// </remarks>
[DebuggerDisplay("{NormalDisplayName}")]
public class ShellItem(object? o) : ComUnknownWrapperBase<IShellItem>(o)
{
	/// <summary>
	/// シェルアイテムをハンドラにバインドします。
	/// </summary>
	/// <typeparam name="TWrapper">作成するハンドラの型。ComUnknownWrapper派生クラス。</typeparam>
	/// <typeparam name="TInterface">ラップするCOMインターフェイスの型。</typeparam>
	/// <param name="bhid">バインドハンドラID。<c>BindHandlerID</c>静的クラスで定義されたIDが使用できます。</param>
	/// <param name="bc">バインドコンテキスト。</param>
	/// <returns>バインドされたハンドラ。</returns>
	public ComResult<TWrapper> BindToHandlerNoThrow<TWrapper, TInterface>(in Guid bhid, BindCtx? bc = null)
		where TWrapper : IComUnknownWrapper
	{
		var hr = _obj.BindToHandler(bc?.WrappedObject as IBindCtx, bhid, typeof(TInterface).GUID, out var x);
		return IComUnknownWrapper.Wrap<TWrapper>(hr, x);
	}

	/// <inheritdoc cref="BindToHandlerNoThrow"/>
	public TWrapper BindToHandler<TWrapper, TInterface>(in Guid bhid, BindCtx? bc = null)
		where TWrapper : IComUnknownWrapper
		=> BindToHandlerNoThrow<TWrapper, TInterface>(bhid, bc).Value;

	/// <summary>
	/// 親アイテム。
	/// </summary>
	/// <remarks>デスクトップの親アイテムは存在しません。</remarks>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItem> ParentNoThrow
		=> new(_obj.GetParent(out var x), new(x));

	/// <inheritdoc cref="ParentNoThrow"/>
	public ShellItem Parent
		=> ParentNoThrow.Value;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public ComResult<string> GetDisplayNameNoThrow(ShellItemDisplayName name)
		=> new(_obj.GetDisplayName(name, out var s), s!);

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public string GetDisplayName(ShellItemDisplayName name)
		=> GetDisplayNameNoThrow(name).Value;

	/// <summary>
	/// 標準表示名。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> NormalDisplayNameNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.NormalDisplay);

	/// <inheritdoc cref="NormalDisplayNameNoThrow"/>
	public string NormalDisplayName
		=> GetDisplayNameNoThrow(ShellItemDisplayName.NormalDisplay).Value;

	/// <summary>
	/// 親フォルダを基準とした相対解析名。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ParentRelativeParsingNameNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelativeParsing);

	/// <inheritdoc cref="ParentRelativeParsingNameNoThrow"/>
	public string ParentRelativeParsingName
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelativeParsing).Value;

	/// <summary>
	/// 絶対解析名。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DesktopAbsoluteParsingNameNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.DesktopAbsoluteParsing);

	/// <inheritdoc cref="DesktopAbsoluteParsingNameNoThrow"/>
	public string DesktopAbsoluteParsingName
		=> GetDisplayNameNoThrow(ShellItemDisplayName.DesktopAbsoluteParsing).Value;

	/// <summary>
	/// 親フォルダを基準とした相対編集名。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ParentRelativeEditingNameNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelativeEditing);

	/// <inheritdoc cref="ParentRelativeEditingNameNoThrow"/>
	public string ParentRelativeEditingName
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelativeEditing).Value;

	/// <summary>
	/// 絶対解析名。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DesktopAbsoluteEditingNameNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.DesktopAbsoluteEditing);

	/// <inheritdoc cref="DesktopAbsoluteEditingNameNoThrow"/>
	public string DesktopAbsoluteEditingName
		=> GetDisplayNameNoThrow(ShellItemDisplayName.DesktopAbsoluteEditing).Value;

	/// <summary>
	/// ファイルシステムパス。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> FileSystemPathNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.FileSystemPath);

	/// <inheritdoc cref="FileSystemPathNoThrow"/>
	public string FileSystemPath
		=> GetDisplayNameNoThrow(ShellItemDisplayName.FileSystemPath).Value;

	/// <summary>
	/// URL形式のパス。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> UrlNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.Url);

	/// <inheritdoc cref="UrlNoThrow"/>
	public string Url
		=> GetDisplayNameNoThrow(ShellItemDisplayName.Url).Value;

	/// <summary>
	/// 親フォルダを基準としたアドレスバー上の相対名。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ParentRelativeNameForAddressBarNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelativeForAddressBar);

	/// <inheritdoc cref="ParentRelativeNameForAddressBarNoThrow"/>
	public string ParentRelativeNameForAddressBar
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelativeForAddressBar).Value;

	/// <summary>
	/// 親フォルダを基準とした相対名。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ParentRelativeNameNoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelative);

	/// <inheritdoc cref="ParentRelativeNameNoThrow"/>
	public string ParentRelativeName
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelative).Value;

	/// <summary>
	/// 親フォルダを基準としたUI上の相対名。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ParentRelativeNameForUINoThrow
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelativeForUI);

	/// <inheritdoc cref="ParentRelativeNameForUINoThrow"/>
	public string ParentRelativeNameForUI
		=> GetDisplayNameNoThrow(ShellItemDisplayName.ParentRelativeForUI).Value;

	/// <summary>
	/// マスクを指定してアイテム属性を取得します。
	/// </summary>
	/// <param name="mask">アイテム属性のマスク。</param>
	/// <returns>マスクされたアイテム属性。</returns>
	public ComResult<ShellItemAttribute> GetAttributesNoThrow(ShellItemAttribute mask)
		=> new(_obj.GetAttributes(mask, out var x), x);

	/// <inheritdoc cref="GetAttributesNoThrow"/>
	public ShellItemAttribute GetAttributes(ShellItemAttribute mask)
		=> GetAttributesNoThrow(mask).Value;

	/// <summary>
	/// アイテム属性。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemAttribute> AttributesNoThrow
		=> GetAttributesNoThrow((ShellItemAttribute)0xffffffff);

	/// <inheritdoc cref="AttributesNoThrow"/>
	public ShellItemAttribute Attributes
		=> GetAttributes((ShellItemAttribute)0xffffffff);

	/// <summary>
	/// アイテムの大小を判定します。
	/// </summary>
	/// <param name="other">比較するアイテム。</param>
	/// <param name="hint">判定方法。</param>
	/// <returns>このアイテムが小さければ負、大きければ正、一致すれば0。</returns>
	public ComResult<int> CompareNoThrow(ShellItem other, ShellItemCompareHint hint)
		=> new(_obj.Compare(other._obj, hint, out var x), x);

	/// <inheritdoc cref="CompareNoThrow"/>
	public int Compare(ShellItem other, ShellItemCompareHint hint)
		=> CompareNoThrow(other, hint).Value;

	/// <summary>
	/// アイテムの一致を判定します。
	/// </summary>
	/// <param name="other">比較するアイテム。</param>
	/// <param name="hint">判定方法。</param>
	/// <returns>一致する場合は真。</returns>
	public ComResult<bool> EqualsNoThrow(ShellItem other, ShellItemCompareHint hint)
		=> new(_obj.Compare(other._obj, hint, out var x), x == 0);

	/// <inheritdoc cref="EqualsNoThrow"/>
	public bool Equals(ShellItem other, ShellItemCompareHint hint)
		=> EqualsNoThrow(other, hint).Value;

	/// <summary>
	/// 既知フォルダを表すシェルアイテムを作成します。
	/// </summary>
	/// <param name="folderId">既知フォルダID。<c>KnownFolderID</c>静的クラスで定義されたIDが使用できます。</param>
	/// <param name="flags">既知フォルダの取得フラグ。</param>
	/// <returns>既知フォルダを表すシェルアイテム。</returns>
	public static ComResult<ShellItem> CreateKnownFolderItemNoThrow(in Guid folderId, KnownFolderFlag flags = 0)
	{
		[DllImport("shell32.dll")]
		static extern int SHGetKnownFolderItem(in Guid rfid, KnownFolderFlag flags, nint hToken, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

		return new(SHGetKnownFolderItem(folderId, flags, 0, typeof(IShellItem).GUID, out var x), new(x));
	}

	/// <inheritdoc cref="CreateKnownFolderItemNoThrow"/>
	/// <completionlist cref="KnownFolderID"/>
	public static ShellItem CreateKnownFolderItem(in Guid folderId, KnownFolderFlag flags = 0)
		=> CreateKnownFolderItemNoThrow(folderId, flags).Value;

	/// <summary>
	/// 既知フォルダのサブアイテムを表すシェルアイテムを作成します。
	/// </summary>
	/// <param name="folderId">既知フォルダID。<c>KnownFolderID</c>静的クラスで定義されたIDが使用できます。</param>
	/// <param name="itemname">サブアイテムの名前。</param>
	/// <param name="flags">既知フォルダの取得フラグ。</param>
	/// <returns>既知フォルダのサブアイテムを表すシェルアイテム。</returns>
	public static ComResult<ShellItem> CreateItemInKnownFolderNoThrow(in Guid folderId, string itemname, KnownFolderFlag flags = 0)
	{
		[DllImport("shell32.dll")]
		static extern int SHCreateItemInKnownFolder(in Guid kfid, KnownFolderFlag dwKFFlags, string? pszItem, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

		return new(SHCreateItemInKnownFolder(folderId, flags, itemname, typeof(IShellItem).GUID, out var x), new(x));
	}

	/// <inheritdoc cref="CreateItemInKnownFolderNoThrow"/>
	public static ShellItem CreateItemInKnownFolder(in Guid folderId, string itename, KnownFolderFlag flags = 0)
		=> CreateItemInKnownFolderNoThrow(folderId, itename, flags).Value;

	public static ComResult<ShellItem> CreateItemFromParsingNameNoThrow(string path, BindCtx? bc = null)
	{
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHCreateItemFromParsingName(string pszPath, IBindCtx? pbc, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(SHCreateItemFromParsingName(path, bc?.WrappedObject as IBindCtx, typeof(IShellItem).GUID, out var x), new(x));
	}

	public static ShellItem CreateItemFromParsingName(string path, BindCtx? bc = null)
		=> CreateItemFromParsingNameNoThrow(path, bc).Value;

	public ComResult<ShellItem> CreateItemFromRelativeNameNoThrow(string name, BindCtx? bc = null)
	{
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHCreateItemFromRelativeName(
			IShellItem psiParent, string pszName, IBindCtx? pbc, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(SHCreateItemFromRelativeName((IShellItem)WrappedObject!, name, bc?.WrappedObject as IBindCtx, typeof(IShellItem).GUID, out var x), new(x));
	}

	public static ShellItem CreateItemFromRelativeName(string name, BindCtx? bc = null)
		=> CreateItemFromParsingNameNoThrow(name, bc).Value;

	public static ComResult<ShellItem> CreateItemFromIDListNoThrow(nint pidl)
	{
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHCreateItemFromIDList(nint pidl, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(SHCreateItemFromIDList(pidl, typeof(IShellItem).GUID, out var x), new(x));
	}

	public static ShellItem CreateItemFromIDList(nint pidl)
		=> CreateItemFromIDListNoThrow(pidl).Value;

	/// <summary>
	/// サブアイテム。ファイルシステムオブジェクトと非ファイルシステムオブジェクトを含みます。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemEnumerable> ItemEnumerableNoThrow
		=> BindToHandlerNoThrow<ShellItemEnumerable, IEnumShellItems>(BindHandlerID.EnumItems);

	/// <inheritdoc cref="ItemsNoThrow"/>
	public ShellItemEnumerable ItemEnumerable
		=> ItemEnumerableNoThrow.Value;

	/// <summary>
	/// サブアイテム。ファイルシステムオブジェクトと非ファイルシステムオブジェクトを含みます。
	/// </summary>
	/// <inheritdoc cref="ItemsNoThrow"/>
	public ImmutableArray<ShellItem> Items => [.. ItemEnumerable];

	/// <summary>
	/// サブアイテム。ファイルシステムオブジェクトのみ含みます。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemEnumerable> StorageItemEnumerableNoThrow
		=> BindToHandlerNoThrow<ShellItemEnumerable, IEnumShellItems>(BindHandlerID.StorageEnum);

	/// <inheritdoc cref="StorageItemEnumerableNoThrow"/>
	public ShellItemEnumerable StorageItemEnumerable
		=> StorageItemEnumerableNoThrow.Value;

	/// <summary>
	/// サブアイテム。ファイルシステムオブジェクトのみ含みます。
	/// </summary>
	public ImmutableArray<ShellItem> StorageItems
		=> [.. StorageItemEnumerable];

	/// <summary>
	/// シェルリンクのリンク先アイテム。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItem> LinkTargetNoThrow
		=> BindToHandlerNoThrow<ShellItem, IShellItem>(BindHandlerID.LinkTargetItem);

	/// <inheritdoc cref="LinkTargetNoThrow"/>
	public ShellItem LinkTarget
		=> LinkTargetNoThrow.Value;

	/// <summary>
	/// アイテム名制限。
	/// </summary>
	/// <inheritdoc cref="ItemNameLimitsNoThrow"/>
	public ItemNameLimits? AsItemNameLimits
		=> this.As<ItemNameLimits, IItemNameLimits>();

	/// <summary>
	/// 画像ファクトリ。
	/// </summary>
	/// <inheritdoc cref="ImageFactoryNoThrow"/>
	public ShellItemImageFactory? AsImageFactory
		=> this.As<ShellItemImageFactory, IShellItemImageFactory>();

	/// <summary>
	/// 永続化用のアイテムID。
	/// </summary>
	/// <inheritdoc cref="AsPersistIDListNoThrow"/>
	public PersistIDList? AsPersistIDList
		=> this.As<PersistIDList, IPersistIDList>();

	/// <summary>
	/// シェルアイテムの保持する親フォルダとアイテムのペア。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ParentAndItem> AsParentAndItemNoThrow
		=> IComUnknownWrapper.Casted<ParentAndItem, IParentAndItem>(this);

	/// <inheritdoc cref="AsParentAndItemNoThrow"/>
	public ParentAndItem AsParentAndItem
		=> AsParentAndItemNoThrow.Value;
}

/// <summary>
/// シェルアイテムの属性。SHFGAOに対応します。
/// </summary>
[Flags]
public enum ShellItemAttribute : uint
{
	CanCopy = 0x1,
	CanMove = 0x2,
	CanLink = 0x4,
	Storage = 0x00000008,
	CanRename = 0x00000010,
	CanDelete = 0x00000020,
	HasPropSheet = 0x00000040,
	DropTarget = 0x00000100,
	Placeholder = 0x00000800,
	System = 0x00001000,
	Encrypted = 0x00002000,
	IsSlow = 0x00004000,
	Ghosted = 0x00008000,
	Link = 0x00010000,
	Share = 0x00020000,
	ReadOnly = 0x00040000,
	Hidden = 0x00080000,
	FileSystemAncestor = 0x10000000,
	Folder = 0x20000000,
	FileSystem = 0x40000000,
	HasSubFolder = 0x80000000,
	Validate = 0x01000000,
	Removable = 0x02000000,
	Cpmpressed = 0x04000000,
	Browsable = 0x08000000,
	NonEnumerated = 0x00100000,
	NewContent = 0x00200000,
	CanMoniker = 0x00400000,
	HasStorage = 0x00400000,
	Stream = 0x00400000,
	StorageAncestor = 0x00800000,
}

/// <summary>
/// シェルアイテムの表示名。SIGDNに対応します。
/// </summary>
[Flags]
public enum ShellItemDisplayName : uint
{
	NormalDisplay = 0,
	ParentRelativeParsing = 0x80018001,
	DesktopAbsoluteParsing = 0x80028000,
	ParentRelativeEditing = 0x80031001,
	DesktopAbsoluteEditing = 0x8004c000,
	FileSystemPath = 0x80058000,
	Url = 0x80068000,
	ParentRelativeForAddressBar = 0x8007c001,
	ParentRelative = 0x80080001,
	ParentRelativeForUI = 0x80094001,
}

/// <summary>
/// シェルアイテムの比較方法。SICHINTFに対応します。
/// </summary>
[Flags]
public enum ShellItemCompareHint : uint
{
	Display = 0,
	AllFields = 0x80000000,
	Canonical = 0x10000000,
	TestFileSystemPathIfNotEqual = 0x20000000,
}
