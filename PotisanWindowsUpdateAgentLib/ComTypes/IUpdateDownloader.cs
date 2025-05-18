using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("68f1c6f9-7ecc-4666-a464-247fe12496c3")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUpdateDownloader // IDispatch
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
	int get_Priority(
		out WuaDownloadPriority retval);

	[PreserveSig]
	int put_Priority(
		WuaDownloadPriority value);

	[PreserveSig]
	int get_Updates(
		out IUpdateCollection retval);

	[PreserveSig]
	int put_Updates(
		IUpdateCollection value);

	[PreserveSig]
	int BeginDownload(
		[MarshalAs(UnmanagedType.IUnknown)] object? onProgressChanged,
		[MarshalAs(UnmanagedType.IUnknown)] object? onCompleted,
		[MarshalAs(UnmanagedType.Struct)] object? state,
		out IDownloadJob retval);

	[PreserveSig]
	int Download(
		out IDownloadResult retval);

	[PreserveSig]
	int EndDownload(
		IDownloadJob? value,
		out IDownloadResult retval);
}
