using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// WUAアップデートサービス。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <see cref="WuaUpdateServiceManager"/>から<see cref="WuaUpdateServiceCollection"/>を介して取得できます。
/// </remarks>
public class WuaUpdateService(object? o) : ComUnknownWrapperBase<IUpdateService>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> NameNoThrow
		=> new(_obj.get_Name(out var x), x!);

	public string Name
		=> NameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<object> ContentValidationCertNoThrow
		=> new(_obj.get_ContentValidationCert(out var x), x);

	public object ContentValidationCert
		=> ContentValidationCertNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DateTime> ExpirationDateNoThrow
		=> new(_obj.get_ExpirationDate(out var x), x.ToDateTime());

	public DateTime ExpirationDate
		=> ExpirationDateNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsManagedNoThrow
		=> new(_obj.get_IsManaged(out var x), x!);

	public bool IsManaged
		=> IsManagedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsRegisteredWithAUNoThrow
		=> new(_obj.get_IsRegisteredWithAU(out var x), x!);

	public bool IsRegisteredWithAU
		=> IsRegisteredWithAUNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DateTime> IssueDateNoThrow
		=> new(_obj.get_IssueDate(out var x), x.ToDateTime());

	public DateTime IssueDate
		=> IssueDateNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> OffersWindowsUpdatesNoThrow
		=> new(_obj.get_OffersWindowsUpdates(out var x), x!);

	public bool OffersWindowsUpdates
		=> OffersWindowsUpdatesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> RedirectUrlsNoThrow
		=> new(_obj.get_RedirectUrls(out var x), new(x!));

	public StringCollection RedirectUrls
		=> RedirectUrlsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ServiceIDNoThrow
		=> new(_obj.get_ServiceID(out var x), x!);

	public string ServiceID
		=> ServiceIDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsScanPackageServiceNoThrow
		=> new(_obj.get_IsScanPackageService(out var x), x!);

	public bool IsScanPackageService
		=> IsScanPackageServiceNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> CanRegisterWithAUNoThrow
		=> new(_obj.get_CanRegisterWithAU(out var x), x!);

	public bool CanRegisterWithAU
		=> CanRegisterWithAUNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ServiceUrlNoThrow
		=> new(_obj.get_ServiceUrl(out var x), x!);

	public string ServiceUrl
		=> ServiceUrlNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> SetupPrefixNoThrow
		=> new(_obj.get_SetupPrefix(out var x), x!);

	public string SetupPrefix
		=> SetupPrefixNoThrow.Value;

	public override string ToString()
		=> NameNoThrow.Or(null) ?? base.ToString()!;
}
