using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("d31a5bac-f719-4178-9dbb-5e2cb47fd18a")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDownloadProgress // IDispatch
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
	int get_CurrentUpdateBytesDownloaded(
		out ComDecimal retval);

	[PreserveSig]
	int get_CurrentUpdateBytesToDownload(
		out ComDecimal retval);

	[PreserveSig]
	int get_CurrentUpdateIndex(
		out int retval);

	[PreserveSig]
	int get_PercentComplete(
		out int retval);

	[PreserveSig]
	int get_TotalBytesDownloaded(
		out ComDecimal retval);

	[PreserveSig]
	int get_TotalBytesToDownload(
		out ComDecimal retval);

	[PreserveSig]
	int GetUpdateResult(
		int updateIndex,
		out IUpdateDownloadResult retval);

	[PreserveSig]
	int get_CurrentUpdateDownloadPhase(
		out WuaDownloadPhase retval);

	[PreserveSig]
	int get_CurrentUpdatePercentComplete(
		out int retval);
}