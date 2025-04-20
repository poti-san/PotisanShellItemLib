using System.Collections;

using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// 列挙型プロパティのリスト。IPropertyEnumTypeList COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class PropertyEnumTypeList(object? o) :
	ComUnknownWrapperBase<IPropertyEnumTypeList>(o),
	IReadOnlyList<PropertyEnumType>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CountNoThrow
		=> new(_obj.GetCount(out var x), x);

	public uint Count
		=> CountNoThrow.Value;

	public ComResult<PropertyEnumType> GetAtNothrow(uint index)
		=> new(_obj.GetAt(index, typeof(IPropertyEnumType).GUID, out var x), new(x));

	public PropertyEnumType GetAt(uint index)
		=> GetAtNothrow(index).Value;

	public ComResult<uint> FindMatchingIndexNoThrow(PropVariant value)
		=> new(_obj.FindMatchingIndex(value, out var x), x);

	public uint FindMatchingIndex(PropVariant value)
		=> FindMatchingIndexNoThrow(value).Value;

	public IEnumerable<PropertyEnumType> Items
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetAt(i);
		}
	}

	IEnumerator<PropertyEnumType> IEnumerable<PropertyEnumType>.GetEnumerator()
		=> Items.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
		=> Items.GetEnumerator();

	int IReadOnlyCollection<PropertyEnumType>.Count
		=> checked((int)Count);

	PropertyEnumType IReadOnlyList<PropertyEnumType>.this[int index]
		=> GetAt(checked((uint)index));
}
