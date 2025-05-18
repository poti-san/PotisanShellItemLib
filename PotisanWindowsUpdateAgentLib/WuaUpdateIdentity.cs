using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaUpdateIdentity(object? o) : ComUnknownWrapperBase<IUpdateIdentity>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> RevisionNumberNoThrow
		=> new(_obj.get_RevisionNumber(out var x), x);

	public int RevisionNumber
		=> RevisionNumberNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> UpdateIDNoThrow
		=> new(_obj.get_UpdateID(out var x), x!);

	public string UpdateID
		=> UpdateIDNoThrow.Value;
}
