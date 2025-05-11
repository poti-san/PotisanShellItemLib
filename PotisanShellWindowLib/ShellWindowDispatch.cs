using Potisan.Windows.Shell.Window.ComTypes;

namespace Potisan.Windows.Shell.Window;

using IServiceProvider = Com.ComTypes.IServiceProvider;

/// <summary>
/// <see cref="ShellWindows"/>の項目として取得できる<c>IDispatch</c>のラッパーです。
/// サービスプロバイダやそれを介したインターフェイスを取得できます。
/// </summary>
public class ShellWindowDispatch(object? o) : ComDispatch(o)
{
	public ComResult<ComServiceProvider> AsServiceProviderNoThrow
		=> _obj is IServiceProvider x ? new(CommonHResults.SOK, new(x)) : new(CommonHResults.ENoInterface, null!);

	public ComServiceProvider AsServiceProvider
		=> AsServiceProviderNoThrow.Value;

	// TODO: AsWebBrowser

	public ComResult<ShellBrowser> AsTopLevelShellBrowserNoThrow
		=> AsServiceProviderNoThrow switch
		{
			{ Succeeded: true, ValueUnchecked: var provider }
				=> provider.QueryServiceNoThrow<ShellBrowser, IShellBrowser>(ShellWindowServiceIDs.TopLevelShellBrowser),
			{ HResult: var hr } => new(hr, null!),
		};

	public ShellBrowser AsTopLevelShellBrowser
		=> AsTopLevelShellBrowserNoThrow.Value;
}
