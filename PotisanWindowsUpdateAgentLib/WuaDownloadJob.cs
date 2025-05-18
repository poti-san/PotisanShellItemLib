using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaDownloadJob(object? o) : ComUnknownWrapperBase<IDownloadJob>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<object?> AsyncStateNoThrow
		=> new(_obj.get_AsyncState(out var x), x);

	public object? AsyncState
		=> AsyncStateNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsCompletedNoThrow
		=> new(_obj.get_IsCompleted(out var x), x);

	public bool IsCompleted
		=> IsCompletedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateCollection> UpdatesNoThrow
		=> new(_obj.get_Updates(out var x), new(x));

	public WuaUpdateCollection Updates
		=> UpdatesNoThrow.Value;

	public ComResult CleanUpNoThrow()
		=> new(_obj.CleanUp());

	public void CleanUp()
		=> CleanUpNoThrow().ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaDownloadProgress> ProgressNoThrow
		=> new(_obj.GetProgress(out var x), new(x));

	public WuaDownloadProgress Progress
		=> ProgressNoThrow.Value;

	public ComResult RequestAbortNoThrow()
		=> new(_obj.RequestAbort());

	public void RequestAbort()
		=> RequestAbortNoThrow().ThrowIfError();
}
