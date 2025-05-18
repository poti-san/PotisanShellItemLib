using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaSearchCompletedCallbackArgs(object? o) : ComUnknownWrapperBase<ISearchCompletedCallbackArgs>(o)
{
	public ComDispatch AsDispatch => new(_obj);
}
