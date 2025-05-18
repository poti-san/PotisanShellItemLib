using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("be56a644-af0e-4e0e-a311-c1d8e695cbff")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUpdateHistoryEntry // IDispatch
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
	int get_Operation(
		out WuaUpdateOperation retval);

	[PreserveSig]
	int get_ResultCode(
		out WuaOperationResultCode retval);

	[PreserveSig]
	int get_HResult(
		out int retval);

	[PreserveSig]
	int get_Date(
		out ComDate retval);

	[PreserveSig]
	int get_UpdateIdentity(
		out IUpdateIdentity retval);

	[PreserveSig]
	int get_Title(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_Description(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_UnmappedResultCode(
		out int retval);

	[PreserveSig]
	int get_ClientApplicationID(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_ServerSelection(
		out WuaServerSelection retval);

	[PreserveSig]
	int get_ServiceID(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_UninstallationSteps(
		out IStringCollection retval);

	[PreserveSig]
	int get_UninstallationNotes(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_SupportUrl(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);
}