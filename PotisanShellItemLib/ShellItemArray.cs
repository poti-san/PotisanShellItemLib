using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem;
using Potisan.Windows.PropertySystem.ComTypes;
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
	public ComResult<ShellItem> GetItemAtNoThrow(uint index)
		=> new(_obj.GetItemAt(index, out var x), new(x));

	public ShellItem GetItemAt(uint index)
		=> GetItemAtNoThrow(index).Value;

	public ComResult<ShellItemEnumerable> GetEnumerableNoThrow()
		=> new(_obj.EnumItems(out var x), new(x));

	public ShellItemEnumerable GetEnumerable()
		=> GetEnumerableNoThrow().Value;

	// ShellItem2
	public ComResult<ShellItem2> GetItem2AtNoThrow(uint index)
		=> new(_obj.GetItemAt(index, out var x), new(x));

	public ShellItem2 GetItem2At(uint index)
		=> GetItem2AtNoThrow(index).Value;

	public ComResult<ShellItem2Enumerable> GetEnumerable2NoThrow()
		=> new(_obj.EnumItems(out var x), new(x));

	public ShellItem2Enumerable GetEnumerable2()
		=> GetEnumerable2NoThrow().Value;

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
}
