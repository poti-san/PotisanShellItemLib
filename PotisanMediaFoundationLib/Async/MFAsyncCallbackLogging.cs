using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.Async;

public class MFAsyncCallbackLoggingStatic(
	Func<(MFAsyncCallbackFlag Flags, MFAsyncCallbackQueue Queue)> getParameters,
	Action<MFAsyncResult> invoke, object? objectPointer, int objectTag
	) : IMFAsyncCallbackLogging
{
	private readonly Func<(MFAsyncCallbackFlag Flags, MFAsyncCallbackQueue Queue)> _getParameters = getParameters;
	private readonly Action<MFAsyncResult> _invoke = invoke;
	private readonly object? _objectPointer = objectPointer;
	private readonly int _objectTag = objectTag;

	public int GetParameters(out uint pdwFlags, out uint pdwQueue)
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

	public int Invoke(IMFAsyncResult pAsyncResult)
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

	public object? GetObjectPointer()
	{
		return _objectPointer;
	}

	[PreserveSig]
	public int GetObjectTag()
	{
		return _objectTag;
	}
}

public class MFAsyncCallbackLoggingDynamic(
	Func<(MFAsyncCallbackFlag Flags, MFAsyncCallbackQueue Queue)> getParameters,
	Action<MFAsyncResult> invoke, Func<object?> objectPointer, Func<int> objectTag
	) : IMFAsyncCallbackLogging
{
	private readonly Func<(MFAsyncCallbackFlag Flags, MFAsyncCallbackQueue Queue)> _getParameters = getParameters;
	private readonly Action<MFAsyncResult> _invoke = invoke;
	private readonly Func<object?> _objectPointer = objectPointer;
	private readonly Func<int> _objectTag = objectTag;

	public int GetParameters(out uint pdwFlags, out uint pdwQueue)
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

	public int Invoke(IMFAsyncResult pAsyncResult)
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

	public object? GetObjectPointer()
	{
		return _objectPointer();
	}

	[PreserveSig]
	public int GetObjectTag()
	{
		return _objectTag();
	}
}
