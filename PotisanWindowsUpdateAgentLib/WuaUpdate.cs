using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// WUAアップデート情報。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// </remarks>
public class WuaUpdate(object? o) : ComUnknownWrapperBase<IUpdate>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> TitleNoThrow
		=> new(_obj.get_Title(out var x), x!);

	public string Title
		=> TitleNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> AutoSelectOnWebSitesNoThrow
		=> new(_obj.get_AutoSelectOnWebSites(out var x), x!);

	public bool AutoSelectOnWebSites
		=> AutoSelectOnWebSitesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateCollection> BundledUpdatesNoThrow
		=> new(_obj.get_BundledUpdates(out var x), new(x));

	public WuaUpdateCollection BundledUpdates
		=> BundledUpdatesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> CanRequireSourceNoThrow
		=> new(_obj.get_CanRequireSource(out var x), x!);

	public bool CanRequireSource
		=> CanRequireSourceNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaCategoryCollection> CategoriesNoThrow
		=> new(_obj.get_Categories(out var x), new(x!));

	public WuaCategoryCollection Categories
		=> CategoriesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<object> DeadlineNoThrow
		=> new(_obj.get_Deadline(out var x), x!);

	public object Deadline
		=> DeadlineNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> DeltaCompressedContentAvailableNoThrow
		=> new(_obj.get_DeltaCompressedContentAvailable(out var x), x!);

	public bool DeltaCompressedContentAvailable
		=> DeltaCompressedContentAvailableNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> DeltaCompressedContentPreferredNoThrow
		=> new(_obj.get_DeltaCompressedContentPreferred(out var x), x!);

	public bool DeltaCompressedContentPreferred
		=> DeltaCompressedContentPreferredNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DescriptionNoThrow
		=> new(_obj.get_Description(out var x), x!);

	public string Description
		=> DescriptionNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> EulaAcceptedNoThrow
		=> new(_obj.get_EulaAccepted(out var x), x!);

	public bool EulaAccepted
		=> EulaAcceptedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> EulaTextNoThrow
		=> new(_obj.get_EulaText(out var x), x!);

	public string EulaText
		=> EulaTextNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> HandlerIDNoThrow
		=> new(_obj.get_HandlerID(out var x), x!);

	public string HandlerID
		=> HandlerIDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateIdentity> IdentityNoThrow
		=> new(_obj.get_Identity(out var x), new(x!));

	public WuaUpdateIdentity Identity
		=> IdentityNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaImageInformation> ImageNoThrow
		=> new(_obj.get_Image(out var x), new(x!));

	public WuaImageInformation Image
		=> ImageNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaInstallationBehavior> InstallationBehaviorNoThrow
		=> new(_obj.get_InstallationBehavior(out var x), new(x!));

	public WuaInstallationBehavior InstallationBehavior
		=> InstallationBehaviorNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsBetaNoThrow
		=> new(_obj.get_IsBeta(out var x), x!);

	public bool IsBeta
		=> IsBetaNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsDownloadedNoThrow
		=> new(_obj.get_IsDownloaded(out var x), x!);

	public bool IsDownloaded
		=> IsDownloadedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsHiddenNoThrow
		=> new(_obj.get_IsHidden(out var x), x!);

	public bool IsHidden
		=> IsHiddenNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsInstalledNoThrow
		=> new(_obj.get_IsInstalled(out var x), x!);

	public bool IsInstalled
		=> IsInstalledNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsMandatoryNoThrow
		=> new(_obj.get_IsMandatory(out var x), x!);

	public bool IsMandatory
		=> IsMandatoryNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsUninstallableNoThrow
		=> new(_obj.get_IsUninstallable(out var x), x!);

	public bool IsUninstallable
		=> IsUninstallableNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> LanguagesNoThrow
		=> new(_obj.get_Languages(out var x), new(x));

	public StringCollection Languages
		=> LanguagesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DateTime> LastDeploymentChangeTimeNoThrow
		=> new(_obj.get_LastDeploymentChangeTime(out var x), x.ToDateTime());

	public DateTime LastDeploymentChangeTime
		=> LastDeploymentChangeTimeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<decimal> MaxDownloadSizeNoThrow
		=> new(_obj.get_MaxDownloadSize(out var x), x.ToDecimal());

	public decimal MaxDownloadSize
		=> MaxDownloadSizeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<decimal> MinDownloadSizeNoThrow
		=> new(_obj.get_MinDownloadSize(out var x), x.ToDecimal());

	public decimal MinDownloadSize
		=> MinDownloadSizeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> MoreInfoUrlsNoThrow
		=> new(_obj.get_MoreInfoUrls(out var x), new(x));

	public StringCollection MoreInfoUrls
		=> MoreInfoUrlsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> MsrcSeverityNoThrow
		=> new(_obj.get_MsrcSeverity(out var x), x!);

	public string MsrcSeverity
		=> MsrcSeverityNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> RecommendedCpuSpeedNoThrow
		=> new(_obj.get_RecommendedCpuSpeed(out var x), x!);

	public int RecommendedCpuSpeed
		=> RecommendedCpuSpeedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> RecommendedHardDiskSpaceNoThrow
		=> new(_obj.get_RecommendedHardDiskSpace(out var x), x!);

	public int RecommendedHardDiskSpace
		=> RecommendedHardDiskSpaceNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> RecommendedMemoryNoThrow
		=> new(_obj.get_RecommendedMemory(out var x), x!);

	public int RecommendedMemory
		=> RecommendedMemoryNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ReleaseNotesNoThrow
		=> new(_obj.get_ReleaseNotes(out var x), x!);

	public string ReleaseNotes
		=> ReleaseNotesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> SecurityBulletinIDsNoThrow
		=> new(_obj.get_SecurityBulletinIDs(out var x), new(x));

	public StringCollection SecurityBulletinIDs
		=> SecurityBulletinIDsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> SupersededUpdateIDsNoThrow
		=> new(_obj.get_SupersededUpdateIDs(out var x), new(x));

	public StringCollection SupersededUpdateIDs
		=> SupersededUpdateIDsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> SupportUrlNoThrow
		=> new(_obj.get_SupportUrl(out var x), x!);

	public string SupportUrl
		=> SupportUrlNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateType> TypeNoThrow
		=> new(_obj.get_Type(out var x), x!);

	public WuaUpdateType Type
		=> TypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> UninstallationNotesNoThrow
		=> new(_obj.get_UninstallationNotes(out var x), x!);

	public string UninstallationNotes
		=> UninstallationNotesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaInstallationBehavior> UninstallationBehaviorNoThrow
		=> new(_obj.get_UninstallationBehavior(out var x), new(x));

	public WuaInstallationBehavior UninstallationBehavior
		=> UninstallationBehaviorNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> UninstallationStepsNoThrow
		=> new(_obj.get_UninstallationSteps(out var x), new(x));

	public StringCollection UninstallationSteps
		=> UninstallationStepsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> KBArticleIDsNoThrow
		=> new(_obj.get_KBArticleIDs(out var x), new(x));

	public StringCollection KBArticleIDs
		=> KBArticleIDsNoThrow.Value;

	public ComResult AcceptEulaNoThrow()
		=> new(_obj.AcceptEula());

	public void AcceptEula()
		=> AcceptEulaNoThrow().ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaDeploymentAction> DeploymentActionNoThrow
		=> new(_obj.get_DeploymentAction(out var x), x);

	public WuaDeploymentAction DeploymentAction
		=> DeploymentActionNoThrow.Value;

	public ComResult CopyFromCacheNoThrow(string path, bool toExtractCabFiles)
		=> new(_obj.CopyFromCache(path, toExtractCabFiles));

	public void CopyFromCache(string path, bool toExtractCabFiles)
		=> CopyFromCacheNoThrow(path, toExtractCabFiles).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaDownloadPriority> DownloadPriorityNoThrow
		=> new(_obj.get_DownloadPriority(out var x), x);

	public WuaDownloadPriority DownloadPriority
		=> DownloadPriorityNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateDownloadContentCollection> DownloadContentsNoThrow
		=> new(_obj.get_DownloadContents(out var x), new(x));

	public WuaUpdateDownloadContentCollection DownloadContents
		=> DownloadContentsNoThrow.Value;
}
