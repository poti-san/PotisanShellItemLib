using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaUpdateException(object? o) : ComUnknownWrapperBase<IUpdateException>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> MessageNoThrow
		=> new(_obj.get_Message(out var x), x!);

	public string Message
		=> MessageNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> HResultNoThrow
		=> new(_obj.get_HResult(out var x), x!);

	public int HResult
		=> HResultNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateExceptionContext> ContextNoThrow
		=> new(_obj.get_Context(out var x), x!);

	public WuaUpdateExceptionContext Context
		=> ContextNoThrow.Value;
}
