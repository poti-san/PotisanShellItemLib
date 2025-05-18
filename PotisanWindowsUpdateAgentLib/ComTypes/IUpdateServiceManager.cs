#pragma warning disable CA1707

using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("23857e3c-02ba-44a3-9423-b1c900805f37")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUpdateServiceManager // IDispatch
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
	int get_Services(
		out IUpdateServiceCollection? retval);

	[PreserveSig]
	int AddService(
		[MarshalAs(UnmanagedType.BStr)] string serviceID,
		[MarshalAs(UnmanagedType.BStr)] string authorizationCabPath,
		out IUpdateService retval);

	[PreserveSig]
	int RegisterServiceWithAU(
		[MarshalAs(UnmanagedType.BStr)] string serviceID);

	[PreserveSig]
	int RemoveService(
		[MarshalAs(UnmanagedType.BStr)] string serviceID);

	[PreserveSig]
	int UnregisterServiceWithAU(
		[MarshalAs(UnmanagedType.BStr)] string serviceID);

	[PreserveSig]
	int AddScanPackageService(
		[MarshalAs(UnmanagedType.BStr)] string serviceName,
		[MarshalAs(UnmanagedType.BStr)] string scanFileLocation,
		int flags,
		out IUpdateService ppService);

	[PreserveSig]
	int SetOption(
		[MarshalAs(UnmanagedType.BStr)] string optionName,
		[MarshalAs(UnmanagedType.Struct)] object optionValue);
}
