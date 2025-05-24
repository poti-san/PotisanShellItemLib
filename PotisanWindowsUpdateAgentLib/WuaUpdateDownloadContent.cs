using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaUpdateDownloadContent(object? o) : ComUnknownWrapperBase<IUpdateDownloadContent>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DownloadUrlNoThrow
		=> new(_obj.get_DownloadUrl(out var x), x!);

	public string DownloadUrl
		=> DownloadUrlNoThrow.Value;
}
