using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem;
using Potisan.Windows.PropertySystem.ComTypes;
using Potisan.Windows.Shell.ComImplements;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// シェルアイテム配列。IShellItemArray COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
/// <remarks>
/// 扱うインターフェイスにShellItemとShellItem2があるため、IReadOnlyList&gt;ShellItem&lt;は実装しません。
/// </remarks>
public class ShellItemArray(object? o) : ComUnknownWrapperBase<IShellItemArray>(o)
{
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

	public ComResult<PropertyStore> GetPropertyStoreNoThrow(GetPropertyStoreFlag flags)
		=> new(_obj.GetPropertyStore(flags, typeof(IPropertyStore).GUID, out var x), new(x));

	public PropertyStore GetPropertyStore(GetPropertyStoreFlag flags)
		=> GetPropertyStoreNoThrow(flags).Value;

	public ComResult<PropertyDescriptionList> PropertyDescriptionListNoThrow(PropertyKey key)
		=> new(_obj.GetPropertyDescriptionList(key, typeof(IPropertyStore).GUID, out var x), new(x));

	public PropertyDescriptionList PropertyDescriptionList(PropertyKey key)
		=> PropertyDescriptionListNoThrow(key).Value;

	public ComResult<ShellItemAttribute> GetAttributesNoThrow(
		ShellItemAttributeOp op,
		ShellItemAttribute mask = (ShellItemAttribute)0xffffffff)
		=> new(_obj.GetAttributes(op, mask, out var x), x);

	public ShellItemAttribute GetAttributes(ShellItemAttributeOp op,
		ShellItemAttribute mask = (ShellItemAttribute)0xffffffff)
		=> GetAttributesNoThrow(op, mask).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CountNoThrow
		=> new(_obj.GetCount(out var x), x);

	public uint Count
		=> CountNoThrow.Value;

	// ShellItem
	public ComResult<ShellItem> GetShellItemNoThrow(uint index)
		=> new(_obj.GetItemAt(index, out var x), new(x));

	public ShellItem GetShellItem(uint index)
		=> GetShellItemNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItemEnumerable> ShellItemEnumerableNoThrow
		=> new(_obj.EnumItems(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ShellItemEnumerable ShellItemEnumerable
		=> ShellItemEnumerableNoThrow.Value;

	public ImmutableArray<ShellItem> ShellItems
		=> [.. ShellItemEnumerable];

	// ShellItem2
	public ComResult<ShellItem2> GetShellItem2NoThrow(uint index)
		=> new(_obj.GetItemAt(index, out var x), new(x));

	public ShellItem2 GetShellItem2(uint index)
		=> GetShellItem2NoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItem2Enumerable> ShellItem2EnumerableNoThrow
		=> new(_obj.EnumItems(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ShellItem2Enumerable ShellItem2Enumerable
		=> ShellItem2EnumerableNoThrow.Value;

	public ImmutableArray<ShellItem> ShellItems2
		=> [.. ShellItem2Enumerable];

	public static ComResult<ShellItemArray> CreateNoThrow(nint parentPidl, ReadOnlySpan<nint> pidls)
	{
		[DllImport("shell32.dll")]
		static extern int SHCreateShellItemArray(
			nint pidlParent,
			[MarshalAs(UnmanagedType.IUnknown)] object?/*IShellFolder**/ psf,
			uint cidl,
			ref nint ppidl,
			out IShellItemArray ppsiItemArray);

		return new(
			SHCreateShellItemArray(parentPidl, null, unchecked((uint)pidls.Length),
				ref MemoryMarshal.GetReference(pidls), out var x),
			new(x));
	}

	public static ShellItemArray Create(nint parentPidl, ReadOnlySpan<nint> pidls)
		=> CreateNoThrow(parentPidl, pidls).Value;

	public static ComResult<ShellItemArray> CreateNoThrow(ComDataObject dataObj)
	{
		[DllImport("shell32.dll")]
		static extern int SHCreateShellItemArrayFromDataObject(
			IDataObject pdo, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(
			SHCreateShellItemArrayFromDataObject((IDataObject)dataObj, typeof(IShellItemArray).GUID, out var x),
			new(x));
	}

	public static ShellItemArray Create(ComDataObject dataObj)
		=> CreateNoThrow(dataObj).Value;

	public static ComResult<ShellItemArray> CreateNoThrow(ReadOnlySpan<nint> absolutePidls)
	{
		[DllImport("shell32.dll")]
		static extern int SHCreateShellItemArrayFromIDLists(uint cidl, ref nint rgpidl, out IShellItemArray ppsiItemArray);

		return new(
			SHCreateShellItemArrayFromIDLists(unchecked((uint)absolutePidls.Length),
				ref MemoryMarshal.GetReference(absolutePidls), out var x),
			new(x));
	}
	public static ShellItemArray Create(ReadOnlySpan<nint> absolutePidls)
		=> CreateNoThrow(absolutePidls).Value;

	public static ComResult<ShellItemArray> CreateNoThrow(ShellItem item)
	{
		[DllImport("shell32.dll")]
		static extern int SHCreateShellItemArrayFromShellItem(
			IShellItem psi, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(
			SHCreateShellItemArrayFromShellItem((IShellItem)item.WrappedObject!, typeof(IShellItemArray).GUID, out var x),
			new(x));
	}

	public static ShellItemArray Create(ShellItem item)
		=> CreateNoThrow(item).Value;

	/// <summary>
	/// エラーコードがHRESULT版ERROR_NOT_FOUND (0x80070490)の場合に空の配列に置換します。
	/// </summary>
	public static ComResult<ShellItemArray> WrapNotFoundWithEmpty(ComResult<ShellItemArray> cr)
		=> cr switch
		{
			{ HResult: unchecked((int)0x80070490)/*ERROR_NOT_FOUND*/ } => new(CommonHResults.SOK, new(new EmptyShellItemArray())),
			{ } cr_ => cr_,
		};
}
