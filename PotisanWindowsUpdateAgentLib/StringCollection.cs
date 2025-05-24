using System.Collections;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// 文字列コレクション。WUAの様々なクラスから使用されます。
/// </summary>
/// <param name="o"></param>
public class StringCollection(object? o) : ComUnknownWrapperBase<IStringCollection>(o), IList<string>, ICloneable
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public static ComResult<StringCollection> CreateEmptyNoThrow()
	{
		Guid CLSID_StringCollection = new(0x72C97D74, 0x7C3B, 0x40AE, 0xB7, 0x7D, 0xAB, 0xDB, 0x22, 0xEB, 0xA6, 0xFB);
		// Both
		return ComHelper.CreateInstanceNoThrow<StringCollection, IStringCollection>(
			CLSID_StringCollection, ComClassContext.InProcServer);
	}

	public static StringCollection CreateEmpty()
		=> CreateEmptyNoThrow().Value;

	public ComResult<string> GetAtNoThrow(int index)
		=> new(_obj.get_Item(index, out var x), x!);

	public ComResult SetAtNoThrow(int index, string item)
		=> new(_obj.put_Item(index, item));

	public string this[int index]
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

	public ComResult AddNoThrow(string item)
		=> new(_obj.Add(item, out _));

	public void Add(string item)
		=> AddNoThrow(item).ThrowIfError();

	public ComResult ClearNoThrow()
		=> new(_obj.Clear());

	public void Clear()
		=> ClearNoThrow().ThrowIfError();

	public bool Contains(string item)
	{
		throw new NotImplementedException();
	}

	public ComResult<StringCollection> CloneNoThrow()
		=> new(_obj.Copy(out var x), new(x));

	public StringCollection Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();

	public void CopyTo(string[] array, int arrayIndex)
	{
		var c = Count;
		for (var i = 0; i < c; i++)
		{
			array[arrayIndex + i] = this[i];
		}
	}

	public IEnumerator<string> GetEnumerator()
	{
		var c = Count;
		for (var i = 0; i < c; i++)
		{
			yield return this[i];
		}
	}

	public int IndexOf(string item)
	{
		throw new NotImplementedException();
	}

	public ComResult InsertNoThrow(int index, string item)
		=> new(_obj.Insert(index, item));

	public void Insert(int index, string item)
		=> InsertNoThrow(index, item).ThrowIfError();

	[Obsolete("このメソッドはインターフェイスの定義として形だけ実装されています。常に失敗します。")]
	public bool Remove(string item)
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
