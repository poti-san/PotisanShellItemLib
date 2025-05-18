using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaDownloadProgress(object? o) : ComUnknownWrapperBase<IDownloadProgress>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<decimal> CurrentUpdateBytesDownloadedNoThrow
		=> new(_obj.get_CurrentUpdateBytesDownloaded(out var x), x.ToDecimal());

	public decimal CurrentUpdateBytesDownloaded
		=> CurrentUpdateBytesDownloadedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<decimal> CurrentUpdateBytesToDownloadNoThrow
		=> new(_obj.get_CurrentUpdateBytesToDownload(out var x), x.ToDecimal());

	public decimal CurrentUpdateBytesToDownload
		=> CurrentUpdateBytesToDownloadNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CurrentUpdateIndexNoThrow
		=> new(_obj.get_CurrentUpdateIndex(out var x), x);

	public int CurrentUpdateIndex
		=> CurrentUpdateIndexNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> PercentCompleteNoThrow
		=> new(_obj.get_PercentComplete(out var x), x);

	public int PercentComplete
		=> PercentCompleteNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<decimal> TotalBytesDownloadedNoThrow
		=> new(_obj.get_TotalBytesDownloaded(out var x), x.ToDecimal());

	public decimal TotalBytesDownloaded
		=> TotalBytesDownloadedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<decimal> TotalBytesToDownloadNoThrow
		=> new(_obj.get_TotalBytesToDownload(out var x), x.ToDecimal());

	public decimal TotalBytesToDownload
		=> TotalBytesToDownloadNoThrow.Value;

	public ComResult<WuaUpdateDownloadResult> GetUpdateResultNoThrow(int updateIndex)
		=> new(_obj.GetUpdateResult(updateIndex, out var x), new(x));

	public WuaUpdateDownloadResult GetUpdateResult(int updateIndex)
		=> GetUpdateResultNoThrow(updateIndex).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaDownloadPhase> CurrentUpdateDownloadPhaseNoThrow
		=> new(_obj.get_CurrentUpdateDownloadPhase(out var x), x);

	public WuaDownloadPhase CurrentUpdateDownloadPhase
		=> CurrentUpdateDownloadPhaseNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CurrentUpdatePercentCompleteNoThrow
		=> new(_obj.get_CurrentUpdatePercentComplete(out var x), x);

	public int CurrentUpdatePercentComplete
		=> CurrentUpdatePercentCompleteNoThrow.Value;
}
