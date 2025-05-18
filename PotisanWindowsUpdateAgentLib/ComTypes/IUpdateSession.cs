using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("816858a4-260d-4260-933a-2585f1abc76b")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUpdateSession // IDispatch
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
	int get_ReadOnly(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_WebProxy(
		out IWebProxy retval);

	[PreserveSig]
	int put_WebProxy(
		IWebProxy value);

	[PreserveSig]
	int CreateUpdateSearcher(
		out IUpdateSearcher retval);

	[PreserveSig]
	int CreateUpdateDownloader(
		out IUpdateDownloader retval);

	[PreserveSig]
	int CreateUpdateInstaller(
		out IUpdateInstaller retval);
}