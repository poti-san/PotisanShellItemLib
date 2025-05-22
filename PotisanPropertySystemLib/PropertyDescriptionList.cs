using System.Collections;

using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// プロパティ記述子リスト。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IPropertyDescriptionList</c> COMインターフェイスのラッパーです。
/// </remarks>
public class PropertyDescriptionList(object? o) :
	ComUnknownWrapperBase<IPropertyDescriptionList>(o),
	IReadOnlyList<PropertyDescription>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CountNoThrow
		=> new(_obj.GetCount(out var x), x);

	public uint Count
		=> CountNoThrow.Value;

	public ComResult<PropertyDescription> GetAtNoThrow(uint index)
		=> new(_obj.GetAt(index, typeof(IPropertyDescription).GUID, out var x), new(x));

	public PropertyDescription GetAt(uint index)
		=> GetAtNoThrow(index).Value;

	public IEnumerable<PropertyDescription> Items
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetAt(i);
		}
	}

	IEnumerator<PropertyDescription> IEnumerable<PropertyDescription>.GetEnumerator()
		=> Items.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
		=> Items.GetEnumerator();

	int IReadOnlyCollection<PropertyDescription>.Count
		=> checked((int)Count);

	PropertyDescription IReadOnlyList<PropertyDescription>.this[int index]
		=> GetAt(checked((uint)index));
}