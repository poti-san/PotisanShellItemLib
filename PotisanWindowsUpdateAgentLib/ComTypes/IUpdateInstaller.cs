using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("7b929c68-ccdc-4226-96b1-8724600b54c2")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUpdateInstaller // IDispatch
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
	int get_ClientApplicationID(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int put_ClientApplicationID(
		[MarshalAs(UnmanagedType.BStr)] string value);

	[PreserveSig]
	int get_IsForced(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int put_IsForced(
		[MarshalAs(UnmanagedType.VariantBool)] bool value);

	[PreserveSig]
	int get_ParentHwnd(
		out nint retval);

	[PreserveSig]
	int put_ParentHwnd(
		nint value);

	[PreserveSig]
	int put_ParentWindow(
		[MarshalAs(UnmanagedType.IUnknown)] object? value);

	[PreserveSig]
	int get_ParentWindow(
		[MarshalAs(UnmanagedType.IUnknown)] out object? retval);

	[PreserveSig]
	int get_Updates(
		out IUpdateCollection retval);

	[PreserveSig]
	int put_Updates(
		IUpdateCollection value);

	[PreserveSig]
	int BeginInstall(
		[MarshalAs(UnmanagedType.IUnknown)] object? onProgressChanged,
		[MarshalAs(UnmanagedType.IUnknown)] object? onCompleted,
		[MarshalAs(UnmanagedType.Struct)] object? state,
		out IInstallationJob retval);

	[PreserveSig]
	int BeginUninstall(
		[MarshalAs(UnmanagedType.IUnknown)] object? onProgressChanged,
		[MarshalAs(UnmanagedType.IUnknown)] object? onCompleted,
		[MarshalAs(UnmanagedType.Struct)] object? state,
		out IInstallationJob retval);

	[PreserveSig]
	int EndInstall(
		IInstallationJob value,
		out IInstallationResult retval);

	[PreserveSig]
	int EndUninstall(
		IInstallationJob value,
		out IInstallationResult retval);

	[PreserveSig]
	int Install(
		out IInstallationResult retval);

	[PreserveSig]
	int RunWizard(
		[MarshalAs(UnmanagedType.BStr)] string? dialogTitle,
		out IInstallationResult retval);

	[PreserveSig]
	int get_IsBusy(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int Uninstall(
		out IInstallationResult retval);

	[PreserveSig]
	int get_AllowSourcePrompts(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int put_AllowSourcePrompts(
		[MarshalAs(UnmanagedType.VariantBool)] bool value);

	[PreserveSig]
	int get_RebootRequiredBeforeInstallation(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);
}