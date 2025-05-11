using Potisan.Windows.MediaFoundation.Async.ComTypes;
using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFTimer(object? o) : ComUnknownWrapperBase<IMFTimer>(o)
{
	public ComResult<object> SetTimerNoThrow(long clockTime, IMFAsyncCallback callback, MFTimerFlag flags = 0, object? state = null)
		=> new(_obj.SetTimer((uint)flags, clockTime, callback, state, out var x), x!);

	public object SetTimer(long clockTime, IMFAsyncCallback callback, MFTimerFlag flags = 0, object? state = null)
		=> SetTimerNoThrow(clockTime, callback, flags, state).Value;

	public ComResult CancelTimerNoThrow(object? key)
		=> new(_obj.CancelTimer(key));

	public void CancelTimer(object? key)
		=> CancelTimerNoThrow(key).ThrowIfError();
}

/// <summary>
/// MFTIMER_FLAGS
/// </summary>
[Flags]
public enum MFTimerFlag : uint
{
	Relative = 0x1
}
