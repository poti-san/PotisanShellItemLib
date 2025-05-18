using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public delegate void WuaDownloadProgressChangedCallbackInvoke(
	WuaDownloadJob downloadJob,
	WuaDownloadProgressChangedCallbackArgs? callbackArgs);

public class WuaDownloadProgressChangedCallback(WuaDownloadProgressChangedCallbackInvoke invoke) : IDownloadProgressChangedCallback
{
	public WuaDownloadProgressChangedCallbackInvoke Invoke { get; } = invoke;

	int IDownloadProgressChangedCallback.Invoke(IDownloadJob downloadJob, IDownloadProgressChangedCallbackArgs? callbackArgs)
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
