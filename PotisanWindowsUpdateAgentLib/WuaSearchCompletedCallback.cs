using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public delegate void WuaSearchCompletedCallbackInvoke(WuaSearchJob searchJob, WuaSearchCompletedCallbackArgs? callbackArgs);

public class WuaSearchCompletedCallback(WuaSearchCompletedCallbackInvoke invoke) : ISearchCompletedCallback
{
	public WuaSearchCompletedCallbackInvoke _invoke { get; } = invoke;

	int ISearchCompletedCallback.Invoke(ISearchJob searchJob, ISearchCompletedCallbackArgs? callbackArgs)
	{
		try
		{
			_invoke(new(searchJob), callbackArgs != null ? new(callbackArgs) : null);
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			return ex.HResult;
		}
	}
}
