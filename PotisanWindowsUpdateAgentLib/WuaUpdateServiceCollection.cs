using System.Collections;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// WUAアップデートサービスコレクション。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <see cref="WuaUpdateServiceManager"/>から取得できます。
/// </remarks>
public class WuaUpdateServiceCollection(object? o) : ComUnknownWrapperBase<IUpdateServiceCollection>(o), IReadOnlyList<WuaUpdateService>
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public ComResult<WuaUpdateService> GetAtNoThrow(int index)
		=> new(_obj.get_Item(index, out var x), new(x));

	public WuaUpdateService this[int index]
		=> GetAtNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;

	public IEnumerator<WuaUpdateService> GetEnumerator()
	{
		Marshal.ThrowExceptionForHR(_obj.get__NewEnum(out var oenum));
		return new ComVariantEnumerable(oenum).Select(o => new WuaUpdateService(o)).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
}
