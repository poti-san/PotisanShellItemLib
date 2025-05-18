using System.Collections;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaUpdateDownloadContentCollection(object? o) : ComUnknownWrapperBase<IUpdateDownloadContentCollection>(o), IReadOnlyList<WuaUpdateDownloadContent>
{
	public ComDispatch AsDispatch => new(_obj);

	public ComResult<WuaUpdateDownloadContent> GetAtNoThrow(int index)
		=> new(_obj.get_Item(index, out var x), new(x));

	public WuaUpdateDownloadContent this[int index]
		=> GetAtNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;

	public IEnumerator<WuaUpdateDownloadContent> GetEnumerator()
	{
		Marshal.ThrowExceptionForHR(_obj.get__NewEnum(out var oenum));
		return new VariantEnumerable(oenum).Select(o => new WuaUpdateDownloadContent(o)).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
}
