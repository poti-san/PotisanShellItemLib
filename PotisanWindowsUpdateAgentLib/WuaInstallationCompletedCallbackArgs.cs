using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaInstallationCompletedCallbackArgs(object? o) : ComUnknownWrapperBase<IInstallationCompletedCallbackArgs>(o)
{
	public ComDispatch AsDispatch => new(_obj);
}
