using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaUpdateInstallationResult(object? o) : ComUnknownWrapperBase<IUpdateInstallationResult>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> HResultNoThrow
		=> new(_obj.get_HResult(out var x), x);

	public int HResult
		=> HResultNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> RebootRequiredNoThrow
		=> new(_obj.get_RebootRequired(out var x), x);

	public bool RebootRequired
		=> RebootRequiredNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaOperationResultCode> ResultCodeNoThrow
		=> new(_obj.get_ResultCode(out var x), x);

	public WuaOperationResultCode ResultCode
		=> ResultCodeNoThrow.Value;
}
