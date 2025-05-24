using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaDownloadProgressChangedCallbackArgs(object? o) : ComUnknownWrapperBase<IDownloadProgressChangedCallbackArgs>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public ComResult<WuaDownloadProgress> ProgressNoThrow
		=> new(_obj.get_Progress(out var x), new(x));

	public WuaDownloadProgress Progress
		=> ProgressNoThrow.Value;
}
