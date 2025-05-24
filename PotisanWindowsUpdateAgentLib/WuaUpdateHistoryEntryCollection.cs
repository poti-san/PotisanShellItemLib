using System.Collections;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaUpdateHistoryEntryCollection(object? o) : ComUnknownWrapperBase<IUpdateHistoryEntryCollection>(o), IReadOnlyList<WuaUpdateHistoryEntry>
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public ComResult<WuaUpdateHistoryEntry> GetAtNoThrow(int index)
		=> new(_obj.get_Item(index, out var x), new(x));

	public WuaUpdateHistoryEntry this[int index]
		=> GetAtNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;

	public IEnumerator<WuaUpdateHistoryEntry> GetEnumerator()
	{
		Marshal.ThrowExceptionForHR(_obj.get__NewEnum(out var oenum));
		return new VariantEnumerable(oenum).Select(o => new WuaUpdateHistoryEntry(o)).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
}
