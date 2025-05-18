using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("d40cff62-e08c-4498-941a-01e25f0fd33c")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ISearchResult // IDispatch
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
	int get_ResultCode(
		out WuaOperationResultCode retval);

	[PreserveSig]
	int get_RootCategories(
		out ICategoryCollection retval);

	[PreserveSig]
	int get_Updates(
		out IUpdateCollection retval);

	[PreserveSig]
	int get_Warnings(
		out IUpdateExceptionCollection retval);
}