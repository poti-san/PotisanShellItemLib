using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaInstallationProgress(object? o) : ComUnknownWrapperBase<IInstallationProgress>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CurrentUpdateIndexNoThrow
		=> new(_obj.get_CurrentUpdateIndex(out var x), x);

	public int CurrentUpdateIndex
		=> CurrentUpdateIndexNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> PercentCompleteNoThrow
		=> new(_obj.get_PercentComplete(out var x), x);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CurrentUpdatePercentCompleteNoThrow
		=> new(_obj.get_CurrentUpdatePercentComplete(out var x), x);

	public int CurrentUpdatePercentComplete
		=> CurrentUpdatePercentCompleteNoThrow.Value;

	public int PercentComplete
		=> PercentCompleteNoThrow.Value;

	public ComResult<WuaUpdateDownloadResult> GetUpdateResultNoThrow(int updateIndex)
		=> new(_obj.GetUpdateResult(updateIndex, out var x), new(x));

	public WuaUpdateDownloadResult GetUpdateResult(int updateIndex)
		=> GetUpdateResultNoThrow(updateIndex).Value;
}
