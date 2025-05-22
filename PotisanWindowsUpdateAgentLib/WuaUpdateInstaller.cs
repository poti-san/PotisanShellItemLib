using System.Collections.Immutable;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaUpdateInstaller(object? o) : ComUnknownWrapperBase<IUpdateInstaller>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	public static ComResult<WuaUpdateInstaller> CreateNoThrow()
	{
		// {D2E0FE7F-D23E-48E1-93C0-6FA8CC346474}
		Guid CLSID_UpdateInstaller = new(0xD2E0FE7F, 0xD23E, 0x48E1, 0x93, 0xC0, 0x6F, 0xA8, 0xCC, 0x34, 0x64, 0x74);
		// Apartment: Both
		return ComHelper.CreateInstanceNoThrow<WuaUpdateInstaller, IUpdateInstaller>(CLSID_UpdateInstaller, ComClassContext.InProcServer);
	}

	public static WuaUpdateInstaller Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ClientApplicationIDNoThrow
		=> new(_obj.get_ClientApplicationID(out var x), x!);

	public ComResult SetClientApplicationIDNoThrow(string value)
		=> new(_obj.put_ClientApplicationID(value));

	public string ClientApplicationID
	{
		get => ClientApplicationIDNoThrow.Value;
		set => SetClientApplicationIDNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsForcedNoThrow
		=> new(_obj.get_IsForced(out var x), x!);

	public ComResult SetIsForcedNoThrow(bool value)
		=> new(_obj.put_IsForced(value));

	public bool IsForced
	{
		get => IsForcedNoThrow.Value;
		set => SetIsForcedNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> ParentWindowHandleNoThrow
		=> new(_obj.get_ParentHwnd(out var x), x);

	public nint ParentWindowHandle
		=> ParentWindowHandleNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<object?> ParentWindowNoThrow
		=> new(_obj.get_ParentWindow(out var x), x);

	public object? ParentWindow
		=> ParentWindowNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateCollection> UpdateCollectionNoThrow
		=> new(_obj.get_Updates(out var x), new(x));

	public ComResult SetUpdateCollectionNoThrow(WuaUpdateCollection value)
		=> new(_obj.put_Updates((IUpdateCollection)value.WrappedObject!));

	public WuaUpdateCollection UpdateCollection
	{
		get => UpdateCollectionNoThrow.Value;
		set => SetUpdateCollectionNoThrow(value).ThrowIfError();
	}

	// TODO set
	public ImmutableArray<WuaUpdate> Update => [.. UpdateCollection];

	public ComResult<WuaInstallationJob> BeginInstallNoThrow(
		WuaInstallationProgressChangedCallback? onProgressChanged = null,
		WuaInstallationCompletedCallback? onCompleted = null,
		object? state = null)
	{
		return new(_obj.BeginInstall(onProgressChanged, onCompleted, state, out var x), new(x));
	}

	public WuaInstallationJob BeginInstall(
		WuaInstallationProgressChangedCallback? onProgressChanged = null,
		WuaInstallationCompletedCallback? onCompleted = null,
		object? state = null)
		=> BeginInstallNoThrow(onProgressChanged, onCompleted, state).Value;

	public ComResult<WuaInstallationJob> BeginUninstallNoThrow(
		WuaInstallationProgressChangedCallback? onProgressChanged = null,
		WuaInstallationCompletedCallback? onCompleted = null,
		object? state = null)
	{
		return new(_obj.BeginUninstall(onProgressChanged, onCompleted, state, out var x), new(x));
	}

	public WuaInstallationJob BeginUninstall(
		WuaInstallationProgressChangedCallback? onProgressChanged = null,
		WuaInstallationCompletedCallback? onCompleted = null,
		object? state = null)
		=> BeginUninstallNoThrow(onProgressChanged, onCompleted, state).Value;

	public ComResult<WuaInstallationResult> EndInstallNoThrow(WuaInstallationJob value)
		=> new(_obj.EndInstall((IInstallationJob)value.WrappedObject!, out var x), new(x));

	public WuaInstallationResult EndInstall(WuaInstallationJob value)
		=> EndInstallNoThrow(value).Value;

	public ComResult<WuaInstallationResult> EndUninstallNoThrow(WuaInstallationJob value)
		=> new(_obj.EndUninstall((IInstallationJob)value.WrappedObject!, out var x), new(x));

	public WuaInstallationResult EndUninstall(WuaInstallationJob value)
		=> EndUninstallNoThrow(value).Value;

	public ComResult<WuaInstallationResult> InstallNoThrow()
		=> new(_obj.Install(out var x), new(x));

	public WuaInstallationResult Install()
		=> InstallNoThrow().Value;

	public ComResult<WuaInstallationResult> RunWizardNoThrow(string dialogTitle)
		=> new(_obj.RunWizard(dialogTitle, out var x), new(x));

	public WuaInstallationResult RunWizard(string dialogTitle)
		=> RunWizardNoThrow(dialogTitle).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsBusyNoThrow
		=> new(_obj.get_IsBusy(out var x), x);

	public bool IsBusy
		=> IsBusyNoThrow.Value;

	public ComResult<WuaInstallationResult> UninstallNoThrow()
		=> new(_obj.Uninstall(out var x), new(x));

	public WuaInstallationResult Uninstall()
		=> UninstallNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> AllowSourcePromptsNoThrow
		=> new(_obj.get_AllowSourcePrompts(out var x), x!);

	public ComResult SetAllowSourcePromptsNoThrow(bool value)
		=> new(_obj.put_AllowSourcePrompts(value));

	public bool AllowSourcePrompts
	{
		get => AllowSourcePromptsNoThrow.Value;
		set => SetAllowSourcePromptsNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> RebootRequiredBeforeInstallationNoThrow
		=> new(_obj.get_RebootRequiredBeforeInstallation(out var x), x);

	public bool RebootRequiredBeforeInstallation
		=> RebootRequiredBeforeInstallationNoThrow.Value;
}
