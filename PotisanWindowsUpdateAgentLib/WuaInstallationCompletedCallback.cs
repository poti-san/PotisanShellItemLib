using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public delegate void WuaInstallationCompletedCallbackInvoke(
	WuaInstallationJob downloadJob,
	WuaInstallationCompletedCallbackArgs? callbackArgs);

public class WuaInstallationCompletedCallback(WuaInstallationCompletedCallbackInvoke invoke) : IInstallationCompletedCallback
{
	public WuaInstallationCompletedCallbackInvoke Invoke { get; } = invoke;

	int IInstallationCompletedCallback.Invoke(IInstallationJob installationJob, IInstallationCompletedCallbackArgs callbackArgs)
	{
		try
		{
			Invoke(new(installationJob), callbackArgs != null ? new(callbackArgs) : null);
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			return ex.HResult;
		}
	}
}
