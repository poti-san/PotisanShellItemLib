using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public delegate void WuaInstallationProgressChangedCallbackInvoke(
	WuaInstallationJob downloadJob,
	WuaInstallationProgressChangedCallbackArgs? callbackArgs);

public class WuaInstallationProgressChangedCallback(WuaInstallationProgressChangedCallbackInvoke invoke) : IInstallationProgressChangedCallback
{
	public WuaInstallationProgressChangedCallbackInvoke Invoke { get; } = invoke;

	int IInstallationProgressChangedCallback.Invoke(IInstallationJob installationJob, IInstallationProgressChangedCallbackArgs callbackArgs)
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
