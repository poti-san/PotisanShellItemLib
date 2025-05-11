using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.Async;

public class MFAsyncCallbackDynamic(
	Func<(MFAsyncCallbackFlag Flags, MFAsyncCallbackQueue Queue)> getParameters,
	Action<MFAsyncResult> invoke
) : IMFAsyncCallback
{
	private readonly Func<(MFAsyncCallbackFlag Flags, MFAsyncCallbackQueue Queue)> _getParameters = getParameters;
	private readonly Action<MFAsyncResult> _invoke = invoke;

	int IMFAsyncCallback.GetParameters(out uint pdwFlags, out uint pdwQueue)
	{
		try
		{
			var (flags, queue) = _getParameters();
			pdwFlags = (uint)flags;
			pdwQueue = (uint)queue;
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			pdwFlags = 0;
			pdwQueue = 0;
			return ex.HResult;
		}
	}

	int IMFAsyncCallback.Invoke(IMFAsyncResult pAsyncResult)
	{
		try
		{
			_invoke(new(pAsyncResult));
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			return ex.HResult;
		}
	}
}

public class MFAsyncCallbackStatic(
	MFAsyncCallbackFlag flags, MFAsyncCallbackQueue queue, Action<MFAsyncResult> invoke
	) : IMFAsyncCallback
{
	private readonly MFAsyncCallbackFlag _flags = flags;
	private readonly MFAsyncCallbackQueue _queue = queue;
	private readonly Action<MFAsyncResult> _invoke = invoke;

	int IMFAsyncCallback.GetParameters(out uint pdwFlags, out uint pdwQueue)
	{
		try
		{
			pdwFlags = (uint)_flags;
			pdwQueue = (uint)_queue;
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			pdwFlags = 0;
			pdwQueue = 0;
			return ex.HResult;
		}
	}

	int IMFAsyncCallback.Invoke(IMFAsyncResult pAsyncResult)
	{
		try
		{
			_invoke(new(pAsyncResult));
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			return ex.HResult;
		}
	}
}

/// <summary>
/// MFASYNC_*
/// </summary>
[Flags]
public enum MFAsyncCallbackFlag : uint
{
	FastIOProcessingCallback = 0x00000001,
	SignalCallback = 0x00000002,
	BlockingCallback = 0x00000004,
	ReplyCallback = 0x00000008,
	LocalizeRemoteCallback = 0x00000010,
}

/// <summary>
/// MFASYNC_CALLBACK_QUEUE_*
/// </summary>
[Flags]
public enum MFAsyncCallbackQueue : uint
{
	Undefined = 0x00000000,
	Standard = 0x00000001,
	RT = 0x00000002,
	IO = 0x00000003,
	Timer = 0x00000004,
	MultiThreaded = 0x00000005,
	LongFunction = 0x00000007,
	PrivateMask = 0xFFFF0000,
	All = 0xFFFFFFFF,
}
