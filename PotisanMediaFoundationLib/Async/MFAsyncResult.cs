using System.Diagnostics;

using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.Async;

public class MFAsyncResult(object? o) : ComUnknownWrapperBase<IMFAsyncResult>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<object> StateNoThrow
		=> new(_obj.GetState(out var x), x!);

	public object State
		=> StateNoThrow.Value;

	public int Status
		=> _obj.GetStatus();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> StatusNoThrow
		=> new(CommonHResults.SOK, Status);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<object> ObjectNoThrow
		=> new(_obj.GetObject(out var x), x!);

	public object Object
		=> ObjectNoThrow.Value;

	public object? StateNoAddRef
		=> _obj.GetStateNoAddRef();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<object> StateNoAddRefNoThrow
		=> StateNoAddRef is { } o ? new(CommonHResults.SOK, o) : new(CommonHResults.EFail, null!);
}
