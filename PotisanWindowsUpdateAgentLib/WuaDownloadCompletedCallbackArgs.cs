using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaDownloadCompletedCallbackArgs(object? o) : ComUnknownWrapperBase<IDownloadCompletedCallbackArgs>(o)
{
	public ComDispatch AsDispatch => new(_obj);
}
