using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public class OleWindow(object? o) : ComUnknownWrapperBase<IOleWindow>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> WindowHandleNoThrow
		=> new(_obj.GetWindow(out var x), x);

	public nint WindowHandle
		=> WindowHandleNoThrow.Value;

	public ComResult ContextSensitiveHelpNoThrow(bool enterMode)
		=> new(_obj.ContextSensitiveHelp(enterMode));

	public void ContextSensitiveHelp(bool enterMode)
		=> ContextSensitiveHelpNoThrow(enterMode).ThrowIfError();
}
