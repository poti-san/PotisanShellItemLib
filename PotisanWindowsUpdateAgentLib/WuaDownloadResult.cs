using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaDownloadResult(object? o) : ComUnknownWrapperBase<IDownloadResult>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> HResultNoThrow
		=> new(_obj.get_HResult(out var x), x);

	public int HResult
		=> HResultNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaOperationResultCode> ResultCodeNoThrow
		=> new(_obj.get_ResultCode(out var x), x);

	public WuaOperationResultCode ResultCode
		=> ResultCodeNoThrow.Value;

	public ComResult<WuaUpdateDownloadResult> GetUpdateResultNoThrow(int updateIndex)
		=> new(_obj.GetUpdateResult(updateIndex, out var x), new(x));

	public WuaUpdateDownloadResult GetUpdateResult(int updateIndex)
		=> GetUpdateResultNoThrow(updateIndex).Value;
}
