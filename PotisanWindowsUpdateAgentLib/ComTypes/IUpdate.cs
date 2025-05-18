#pragma warning disable CA1707

using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("6a92b07a-d821-4682-b423-5c805022cc4d")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUpdate // IDispatch
{
	#region IDispatch

	[PreserveSig]
	int GetTypeInfoCount(
		out uint pctinfo);

	[PreserveSig]
	int GetTypeInfo(
		uint iTInfo,
		Lcid lcid,
		out ITypeInfo ppTInfo);

	[PreserveSig]
	int GetIDsOfNames(
		in Guid riid,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rgszNames,
		uint cNames,
		Lcid lcid,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out int[] rgDispId);

	[PreserveSig]
	int Invoke(
		ComMemberID dispIdMember,
		in Guid riid,
		Lcid lcid,
		ushort wFlags,
		[In, Out] DISPPARAMS pDispParams,
		[MarshalAs(UnmanagedType.Struct)] out object pVarResult,
		[Out] ComExceptionInfo pExcepInfo,
		out uint puArgErr);

	#endregion IDispatch

	[PreserveSig]
	int get_Title(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_AutoSelectOnWebSites(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_BundledUpdates(
		out IUpdateCollection retval);

	[PreserveSig]
	int get_CanRequireSource(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_Categories(
		out ICategoryCollection retval);

	[PreserveSig]
	int get_Deadline(
		[MarshalAs(UnmanagedType.Struct)] out object retval);

	[PreserveSig]
	int get_DeltaCompressedContentAvailable(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_DeltaCompressedContentPreferred(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_Description(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_EulaAccepted(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_EulaText(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_HandlerID(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_Identity(
		out IUpdateIdentity retval);

	[PreserveSig]
	int get_Image(
		out IImageInformation retval);

	[PreserveSig]
	int get_InstallationBehavior(
		out IInstallationBehavior retval);

	[PreserveSig]
	int get_IsBeta(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_IsDownloaded(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_IsHidden(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int put_IsHidden(
		[MarshalAs(UnmanagedType.VariantBool)] bool value);

	[PreserveSig]
	int get_IsInstalled(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_IsMandatory(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_IsUninstallable(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_Languages(
		out IStringCollection retval);

	[PreserveSig]
	int get_LastDeploymentChangeTime(
		out ComDate retval);

	[PreserveSig]
	int get_MaxDownloadSize(
		out ComDecimal retval);

	[PreserveSig]
	int get_MinDownloadSize(
		out ComDecimal retval);

	[PreserveSig]
	int get_MoreInfoUrls(
		out IStringCollection retval);

	[PreserveSig]
	int get_MsrcSeverity(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_RecommendedCpuSpeed(
		out int retval);

	[PreserveSig]
	int get_RecommendedHardDiskSpace(
		out int retval);

	[PreserveSig]
	int get_RecommendedMemory(
		out int retval);

	[PreserveSig]
	int get_ReleaseNotes(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_SecurityBulletinIDs(
		out IStringCollection retval);

	[PreserveSig]
	int get_SupersededUpdateIDs(
		out IStringCollection retval);

	[PreserveSig]
	int get_SupportUrl(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_Type(
		out WuaUpdateType retval);

	[PreserveSig]
	int get_UninstallationNotes(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_UninstallationBehavior(
		out IInstallationBehavior retval);

	[PreserveSig]
	int get_UninstallationSteps(
		out IStringCollection retval);

	[PreserveSig]
	int get_KBArticleIDs(
		out IStringCollection retval);

	[PreserveSig]
	int AcceptEula();

	[PreserveSig]
	int get_DeploymentAction(
		out WuaDeploymentAction retval);

	[PreserveSig]
	int CopyFromCache(
		[MarshalAs(UnmanagedType.BStr)] string path,
		[MarshalAs(UnmanagedType.VariantBool)] bool toExtractCabFiles);

	[PreserveSig]
	int get_DownloadPriority(
		out WuaDownloadPriority retval);

	[PreserveSig]
	int get_DownloadContents(
		out IUpdateDownloadContentCollection retval);
}
