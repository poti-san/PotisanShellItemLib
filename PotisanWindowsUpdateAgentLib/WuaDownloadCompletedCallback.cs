using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public delegate void WuaDownloadCompletedCallbackInvoke(
	WuaDownloadJob downloadJob,
	WuaDownloadCompletedCallbackArgs? callbackArgs);

public class WuaDownloadCompletedCallback(WuaDownloadCompletedCallbackInvoke invoke) : IDownloadCompletedCallback
{
	public WuaDownloadCompletedCallbackInvoke Invoke { get; } = invoke;

	int IDownloadCompletedCallback.Invoke(IDownloadJob downloadJob, IDownloadCompletedCallbackArgs callbackArgs)
	{
		try
		{
			Invoke(new(downloadJob), callbackArgs != null ? new(callbackArgs) : null);
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			return ex.HResult;
		}
	}
}
