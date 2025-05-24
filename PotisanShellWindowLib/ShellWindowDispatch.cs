using Potisan.Windows.Shell.Window.ComTypes;

namespace Potisan.Windows.Shell.Window;

using IServiceProvider = Com.ComTypes.IServiceProvider;

/// <summary>
/// <see cref="ShellWindows"/>の項目として取得できる<c>IDispatch</c>のラッパーです。
/// サービスプロバイダやそれを介したインターフェイスを取得できます。
/// </summary>
public class ShellWindowDispatch(object? o) : ComDispatch(o)
{
	public ComServiceProvider? AsServiceProvider
		=> this.As<ComServiceProvider, IServiceProvider>();

	// TODO: AsWebBrowser

	public ComResult<ShellBrowser> QueryTopLevelShellBrowserNoThrow()
		=> AsServiceProvider switch
		{
			{ } provider
				=> provider.QueryServiceNoThrow<ShellBrowser, IShellBrowser>(ShellWindowServiceIDs.TopLevelShellBrowser),
			_ => new(CommonHResults.ENoInterface, null!),
		};

	public ShellBrowser QueryTopLevelShellBrowser()
		=> QueryTopLevelShellBrowserNoThrow().Value;
}
