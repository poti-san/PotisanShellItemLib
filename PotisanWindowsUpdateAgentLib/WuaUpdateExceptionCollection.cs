using System.Collections;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaUpdateExceptionCollection(object? o) : ComUnknownWrapperBase<IUpdateExceptionCollection>(o), IReadOnlyList<WuaUpdateException>
{
	public ComDispatch AsDispatch => new(_obj);

	public ComResult<WuaUpdateException> GetAtNoThrow(int index)
		=> new(_obj.get_Item(index, out var x), new(x));

	public WuaUpdateException this[int index]
		=> GetAtNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;

	public IEnumerator<WuaUpdateException> GetEnumerator()
	{
		Marshal.ThrowExceptionForHR(_obj.get__NewEnum(out var oenum));
		return new VariantEnumerable(oenum).Select(o => new WuaUpdateException(o)).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
}
