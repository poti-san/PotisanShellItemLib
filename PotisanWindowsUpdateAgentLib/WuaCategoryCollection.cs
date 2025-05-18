using System.Collections;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaCategoryCollection(object? o) : ComUnknownWrapperBase<ICategoryCollection>(o), IReadOnlyList<WuaCategory>
{
	public ComResult<WuaCategory> GetAtNoThrow(int index)
		=> new(_obj.get_Item(index, out var x), new(x));

	public WuaCategory this[int index]
		=> GetAtNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;

	public IEnumerator<WuaCategory> GetEnumerator()
	{
		var c = Count;
		for (var i = 0; i < c; i++)
			yield return this[i];
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
}
