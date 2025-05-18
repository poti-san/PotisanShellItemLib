namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// <c>AutomaticUpdatesNotificationLevel</c>
/// </summary>
public enum WuaAutomaticUpdatesNotificationLevel
{
	NotConfigured = 0,
	Disabled = 1,
	NotifyBeforeDownload = 2,
	NotifyBeforeInstallation = 3,
	ScheduledInstallation = 4,
}

/// <summary>
/// <c>AutomaticUpdatesScheduledInstallationDay</c>
/// </summary>
public enum WuaAutomaticUpdatesScheduledInstallationDay
{
	EveryDay = 0,
	EverySunday = 1,
	EveryMonday = 2,
	EveryTuesday = 3,
	EveryWednesday = 4,
	EveryThursday = 5,
	EveryFriday = 6,
	EverySaturday = 7,
}

/// <summary>
/// <c>DownloadPhase</c>
/// </summary>
public enum WuaDownloadPhase
{
	Initializing = 1,
	Downloading = 2,
	Verifying = 3,
}

/// <summary>
/// <c>DownloadPriority</c>
/// </summary>
public enum WuaDownloadPriority
{
	Low = 1,
	Normal = 2,
	High = 3,
	ExtraHigh = 4,
}

/// <summary>
/// <c>AutoSelectionMode</c>
/// </summary>
public enum WuaAutoSelectionMode
{
	LetWindowsUpdateDecide = 0,
	AutoSelectIfDownloaded = 1,
	NeverAutoSelect = 2,
	AlwaysAutoSelect = 3,
}

/// <summary>
/// <c>AutoDownloadMode</c>
/// </summary>
public enum WuaAutoDownloadMode
{
	LetWindowsUpdateDecide = 0,
	NeverAutoDownload = 1,
	AlwaysAutoDownload = 2,
}

/// <summary>
/// <c>InstallationImpact</c>
/// </summary>
public enum WuaInstallationImpact
{
	Normal = 0,
	Minor = 1,
	RequiresExclusiveHandling = 2,
}

/// <summary>
/// <c>InstallationRebootBehavior</c>
/// </summary>
public enum WuaInstallationRebootBehavior
{
	NeverReboots = 0,
	AlwaysRequiresReboot = 1,
	CanRequestReboot = 2,
}

/// <summary>
/// <c>OperationResultCode</c>
/// </summary>
public enum WuaOperationResultCode
{
	NotStarted = 0,
	InProgress = 1,
	Succeeded = 2,
	SucceededWithErrors = 3,
	Failed = 4,
	Aborted = 5,
}

/// <summary>
/// <c>ServerSelection</c>
/// </summary>
public enum WuaServerSelection
{
	Default = 0,
	ManagedServer = 1,
	WindowsUpdate = 2,
	Others = 3,
}

/// <summary>
/// <c>UpdateType</c>
/// </summary>
public enum WuaUpdateType
{
	Software = 1,
	Driver = 2,
}

/// <summary>
/// <c>UpdateOperation</c>
/// </summary>
public enum WuaUpdateOperation
{
	Installation = 1,
	Uninstallation = 2,
}

/// <summary>
/// <c>DeploymentAction</c>
/// </summary>
public enum WuaDeploymentAction
{
	None = 0,
	Installation = 1,
	Uninstallation = 2,
	Detection = 3,
	OptionalInstallation = 4,
}

/// <summary>
/// <c>UpdateExceptionContext</c>
/// </summary>
public enum WuaUpdateExceptionContext
{
	General = 1,
	WindowsDriver = 2,
	WindowsInstaller = 3,
	SearchIncomplete = 4,
}

/// <summary>
/// <c>AutomaticUpdatesUserType</c>
/// </summary>
public enum WuaAutomaticUpdatesUserType
{
	CurrentUser = 1,
	LocalAdministrator = 2,
}

/// <summary>
/// <c>AutomaticUpdatesPermissionType</c>
/// </summary>
public enum AutomaticUpdatesPermissionType
{
	SetNotificationLevel = 1,
	DisableAutomaticUpdates = 2,
	SetIncludeRecommendedUpdates = 3,
	SetFeaturedUpdatesEnabled = 4,
	SetNonAdministratorsElevated = 5,
}

/// <summary>
/// <c>UpdateServiceRegistrationState</c>
/// </summary>
public enum WuaUpdateServiceRegistrationState
{
	NotRegistered = 1,
	RegistrationPending = 2,
	Registered = 3,
}

/// <summary>
/// <c>SearchScope</c>
/// </summary>
public enum WuaSearchScope
{
	Default = 0,
	MachineOnly = 1,
	CurrentUserOnly = 2,
	MachineAndCurrentUser = 3,
	MachineAndAllUsers = 4,
	AllUsers = 5
}

/// <summary>
/// <c>DownloadType</c>
/// </summary>
public enum WuaDownloadType
{
	Full = 0,
	UpdateBootstrapper = 1,
}
