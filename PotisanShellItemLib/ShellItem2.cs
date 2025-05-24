using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem;
using Potisan.Windows.PropertySystem.ComTypes;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// プロパティを取得しやすいシェルアイテム。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IShellItem2</c> COMインターフェイスのラッパーです。
/// </remarks>
public class ShellItem2(object? o) : ShellItem(o)
{
	private new readonly IShellItem2 _obj = o == null ? null! : (IShellItem2)o;

	/// <summary>
	/// プロパティストアを取得します。
	/// </summary>
	/// <param name="flags">取得フラグ。</param>
	/// <returns>プロパティストア。</returns>
	/// <remarks>
	/// 取得されたプロパティストアは一部のプロパティキーを列挙しません。
	/// 確実に取得する場合は<c>PropertySystem</c>クラスからキーを取得してください。
	/// </remarks>
	public ComResult<PropertyStore> GetPropertyStoreNoThrow(GetPropertyStoreFlag flags)
		=> new(_obj.GetPropertyStore(flags, typeof(IPropertyStore).GUID, out var x), new(x));

	/// <inheritdoc cref="GetPropertyStoreNoThrow"/>
	public PropertyStore GetPropertyStore(GetPropertyStoreFlag flags)
		=> GetPropertyStoreNoThrow(flags).Value;

	/// <summary>
	/// プロパティキーを取得します。
	/// </summary>
	/// <remarks>
	/// このプロパティは内部でプロパティストアを作成します。
	/// 繰り返し使用する場合はキャッシュやプロパティストア自体の使用を検討してください。
	/// </remarks>
	public ImmutableArray<PropertyKey> PropertyKeys
	{
		get
		{
			using var ps = GetPropertyStore(GetPropertyStoreFlag.Default);
			return ps.Keys;
		}
	}

	/// <summary>
	/// プロパティを取得します。
	/// </summary>
	/// <remarks>
	/// このプロパティは内部でプロパティストアを作成します。
	/// 繰り返し使用する場合はキャッシュやプロパティストア自体の使用を検討してください。
	/// </remarks>
	public ImmutableArray<KeyValuePair<PropertyKey, PropVariant>> Properties
	{
		get
		{
			using var ps = GetPropertyStore(GetPropertyStoreFlag.Default);
			return ps.Items;
		}
	}

	//[PreserveSig]
	//int GetPropertyStoreWithCreateObject(
	//	GetPropertyStore flags,
	//	[MarshalAs(UnmanagedType.IUnknown)] object? punkCreateObject,
	//	in Guid riid,
	//	[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	//[PreserveSig]
	//int GetPropertyStoreForKeys(
	//	[MarshalAs(UnmanagedType.LPArray)] PropertyKey[] rgKeys,
	//	uint cKeys,
	//	GetPropertyStore flags,
	//	in Guid riid,
	//	[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	//[PreserveSig]
	//int GetPropertyDescriptionList(
	//	PropertyKey keyType,
	//		in Guid riid,
	//		[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	/// <summary>
	/// プロパティ情報のキャッシュを更新します。
	/// </summary>
	/// <param name="bc"></param>
	public ComResult UpdateNoThrow(BindCtx? bc = null)
		=> new(_obj.Update(bc?.WrappedObject as IBindCtx));

	/// <inheritdoc cref="UpdateNoThrow"/>
	public void Update(BindCtx? bc = null)
		=> UpdateNoThrow(bc).ThrowIfError();

	/// <summary>
	/// プロパティを取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティ。</returns>
	public ComResult<PropVariant> GetPropertyNoThrow(PropertyKey key)
	{
		var pv = new PropVariant();
		return new(_obj.GetProperty(key, pv), pv);
	}

	/// <inheritdoc cref="GetPropertyNoThrow"/>
	public PropVariant GetProperty(PropertyKey key)
		=> GetPropertyNoThrow(key).Value;

	/// <summary>
	/// GUID形式のプロパティを取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティの値。</returns>
	public ComResult<Guid> GetClsidNoThrow(PropertyKey key)
		=> new(_obj.GetCLSID(key, out var x), x);

	/// <inheritdoc cref="GetClsidNoThrow"/>
	public Guid GetClsid(PropertyKey key)
		=> GetClsidNoThrow(key).Value;

	/// <summary>
	/// FILETIME形式のプロパティを取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティの値。</returns>
	public ComResult<FileTime> GetFileTimeNoThrow(PropertyKey key)
		=> new(_obj.GetFileTime(key, out var x), x);

	/// <inheritdoc cref="GetFileTimeNoThrow"/>
	public FileTime GetFileTime(PropertyKey key)
		=> GetFileTimeNoThrow(key).Value;

	/// <summary>
	/// 32ビット符号付き整数形式のプロパティを取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティの値。</returns>
	public ComResult<int> GetInt32NoThrow(PropertyKey key)
		=> new(_obj.GetInt32(key, out var x), x);

	/// <inheritdoc cref="GetInt32NoThrow"/>
	public int GetInt32(PropertyKey key)
		=> GetInt32NoThrow(key).Value;

	/// <summary>
	/// 文字列形式のプロパティを取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティの値。</returns>
	public ComResult<string> GetStringNoThrow(PropertyKey key)
		=> new(_obj.GetString(key, out var x), x!);

	/// <inheritdoc cref="GetStringNoThrow"/>
	public string GetString(PropertyKey key)
		=> GetStringNoThrow(key).Value;

	/// <summary>
	/// 32ビット符号無し整数形式のプロパティを取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティの値。</returns>
	public ComResult<uint> GetUInt32NoThrow(PropertyKey key)
		=> new(_obj.GetUInt32(key, out var x), x);

	/// <inheritdoc cref="GetUInt32NoThrow"/>
	public uint GetUInt32(PropertyKey key)
		=> GetUInt32NoThrow(key).Value;

	/// <summary>
	/// 64ビット符号無し整数形式のプロパティを取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティの値。</returns>
	public ComResult<ulong> GetUInt64NoThrow(PropertyKey key)
		=> new(_obj.GetUInt64(key, out var x), x);

	/// <inheritdoc cref="GetUInt64NoThrow"/>
	public ulong GetUInt64(PropertyKey key)
		=> GetUInt64NoThrow(key).Value;

	/// <summary>
	/// 論理値形式のプロパティを取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティの値。</returns>
	public ComResult<bool> GetBoolNoThrow(PropertyKey key)
		=> new(_obj.GetBool(key, out var x), x);

	/// <inheritdoc cref="GetBoolNoThrow"/>
	public bool GetBool(PropertyKey key)
		=> GetBoolNoThrow(key).Value;

	/// <inheritdoc cref="ShellItem.CreateKnownFolderItemNoThrow(in Guid, KnownFolderFlag)"/>
	public new static ComResult<ShellItem2> CreateKnownFolderItemNoThrow(
		in Guid folderId,
		KnownFolderFlag flags = 0)
	{
		[DllImport("shell32.dll")]
		static extern int SHGetKnownFolderItem(in Guid rfid, KnownFolderFlag flags, nint hToken,
			in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

		return new(SHGetKnownFolderItem(folderId, flags, 0, typeof(IShellItem2).GUID, out var x), new(x));
	}

	/// <inheritdoc cref="CreateKnownFolderItemNoThrow"/>
	public new static ShellItem2 CreateKnownFolderItem(in Guid folderId, KnownFolderFlag flags = 0)
		=> CreateKnownFolderItemNoThrow(folderId, flags).Value;

	/// <inheritdoc cref="ShellItem.CreateKnownFolderItemNoThrow(in Guid, KnownFolderFlag)"/>
	public new static ComResult<ShellItem2> CreateItemInKnownFolderNoThrow(
		in Guid folderId,
		string itemname,
		KnownFolderFlag flags = 0)
	{
		[DllImport("shell32.dll")]
		static extern int SHCreateItemInKnownFolder(in Guid kfid, KnownFolderFlag dwKFFlags, string? pszItem,
			in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

		return new(SHCreateItemInKnownFolder(folderId, flags, itemname, typeof(IShellItem2).GUID, out var x), new(x));
	}

	/// <inheritdoc cref="CreateItemInKnownFolderNoThrow"/>
	public new static ShellItem2 CreateItemInKnownFolder(in Guid folderId, string itename, KnownFolderFlag flags = 0)
		=> CreateItemInKnownFolderNoThrow(folderId, itename, flags).Value;

	public new static ComResult<ShellItem2> CreateItemFromParsingNameNoThrow(string path, BindCtx? bc = null)
	{
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHCreateItemFromParsingName(string pszPath, IBindCtx? pbc, in Guid riid,
			[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(SHCreateItemFromParsingName(path, bc?.WrappedObject as IBindCtx, typeof(IShellItem2).GUID, out var x), new(x));
	}

	public new static ShellItem2 CreateItemFromParsingName(string path, BindCtx? bc = null)
		=> CreateItemFromParsingNameNoThrow(path, bc).Value;

	public new ComResult<ShellItem2> CreateItemFromRelativeNameNoThrow(string name, BindCtx? bc = null)
	{
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHCreateItemFromRelativeName(
			IShellItem psiParent, string pszName, IBindCtx? pbc, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(SHCreateItemFromRelativeName((IShellItem)WrappedObject!, name,
			bc?.WrappedObject as IBindCtx, typeof(IShellItem2).GUID, out var x), new(x));
	}

	public new static ShellItem2 CreateItemFromRelativeName(string name, BindCtx? bc = null)
		=> CreateItemFromParsingNameNoThrow(name, bc).Value;

	public new static ComResult<ShellItem2> CreateItemFromIDListNoThrow(nint pidl)
	{
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHCreateItemFromIDList(nint pidl, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(SHCreateItemFromIDList(pidl, typeof(IShellItem2).GUID, out var x), new(x));
	}

	public new static ShellItem2 CreateItemFromIDList(nint pidl)
		=> CreateItemFromIDListNoThrow(pidl).Value;

	/// <inheritdoc cref="ShellItem.ItemsNoThrow"/>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public new ComResult<ShellItem2Enumerable> ItemEnumerableNoThrow
		=> BindToHandlerNoThrow<ShellItem2Enumerable, IEnumShellItems>(BindHandlerID.EnumItems);

	/// <inheritdoc cref="ItemEnumerable"/>
	public new ShellItem2Enumerable ItemEnumerable
		=> ItemEnumerableNoThrow.ValueUnchecked;

	/// <inheritdoc cref="ShellItem.Items"/>
	public new ImmutableArray<ShellItem2> Items
		=> [.. ItemEnumerable];

	/// <inheritdoc cref="ShellItem.StorageItemEnumerableNoThrow"/>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public new ComResult<ShellItem2Enumerable> StorageItemEnumerableNoThrow
		=> BindToHandlerNoThrow<ShellItem2Enumerable, IEnumShellItems>(BindHandlerID.StorageEnum);

	/// <inheritdoc cref="StorageItemEnumerableNoThrow"/>
	public new ShellItem2Enumerable StorageItemEnumerable
		=> StorageItemEnumerableNoThrow.ValueUnchecked;

	/// <inheritdoc cref="ShellItem.StorageItems"/>
	public new ImmutableArray<ShellItem2> StorageItems
		=> [.. StorageItemEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public new ComResult<ShellItem2> LinkTargetNoThrow
		=> BindToHandlerNoThrow<ShellItem2, IShellItem2>(BindHandlerID.LinkTargetItem);

	public new ShellItem2 LinkTarget
		=> LinkTargetNoThrow.Value;
}
