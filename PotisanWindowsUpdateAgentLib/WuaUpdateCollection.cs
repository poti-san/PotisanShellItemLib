using System.Collections;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaUpdateCollection(object? o) : ComUnknownWrapperBase<IUpdateCollection>(o), IList<WuaUpdate>, ICloneable
{
	public ComDispatch AsDispatch => new(_obj);

	public static ComResult<WuaUpdateCollection> CreateEmptyNoThrow()
	{
		// {13639463-00DB-4646-803D-528026140D88}
		Guid CLSID_UpdateCollection = new(0x13639463, 0x00DB, 0x4646, 0x80, 0x3D, 0x52, 0x80, 0x26, 0x14, 0x0D, 0x88);
		// Apartment: Both
		return ComHelper.CreateInstanceNoThrow<WuaUpdateCollection, IUpdateCollection>(
			CLSID_UpdateCollection, ComClassContext.InProcServer);
	}

	public static WuaUpdateCollection CreateEmpty()
		=> CreateEmptyNoThrow().Value;

	public ComResult<WuaUpdate> GetAtNoThrow(int index)
		=> new(_obj.get_Item(index, out var x), new(x));

	public ComResult SetAtNoThrow(int index, WuaUpdate item)
		=> new(_obj.put_Item(index, (IUpdate)item.WrappedObject!));

	public WuaUpdate this[int index]
	{
		get => GetAtNoThrow(index).Value;
		set => SetAtNoThrow(index, value).ThrowIfError();
	}

	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;

	public ComResult<bool> IsReadOnlyNoThrow
		=> new(_obj.get_ReadOnly(out var x), x);

	public bool IsReadOnly
		=> IsReadOnlyNoThrow.Value;

	public ComResult AddNoThrow(WuaUpdate item)
		=> new(_obj.Add((IUpdate)item.WrappedObject!, out _));

	public void Add(WuaUpdate item)
		=> AddNoThrow(item).ThrowIfError();

	public ComResult ClearNoThrow()
		=> new(_obj.Clear());

	public void Clear()
		=> ClearNoThrow().ThrowIfError();

	public bool Contains(WuaUpdate item)
	{
		throw new NotImplementedException();
	}

	public ComResult<StringCollection> CloneNoThrow()
		=> new(_obj.Copy(out var x), new(x));

	public StringCollection Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();

	public void CopyTo(WuaUpdate[] array, int arrayIndex)
	{
		var c = Count;
		for (var i = 0; i < c; i++)
		{
			array[arrayIndex + i] = this[i];
		}
	}

	public IEnumerator<WuaUpdate> GetEnumerator()
	{
		var c = Count;
		for (var i = 0; i < c; i++)
		{
			yield return this[i];
		}
	}

	public int IndexOf(WuaUpdate item)
	{
		throw new NotImplementedException();
	}

	public ComResult InsertNoThrow(int index, WuaUpdate item)
		=> new(_obj.Insert(index, (IUpdate)item.WrappedObject!));

	public void Insert(int index, WuaUpdate item)
		=> InsertNoThrow(index, item).ThrowIfError();

	[Obsolete("このメソッドはインターフェイスの定義として形だけ実装されています。常に失敗します。")]
	public bool Remove(WuaUpdate item)
	{
		throw new NotImplementedException();
	}

	public ComResult RemoveAtNoThrow(int index)
		=> new(_obj.RemoveAt(index));

	public void RemoveAt(int index)
		=> RemoveAtNoThrow(index).ThrowIfError();

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
