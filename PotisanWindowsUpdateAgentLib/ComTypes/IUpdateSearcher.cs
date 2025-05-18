using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("8f45abf1-f9ae-4b95-a933-f0f66e5056ea")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUpdateSearcher // IDispatch
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
	int get_CanAutomaticallyUpgradeService(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int put_CanAutomaticallyUpgradeService(
		[MarshalAs(UnmanagedType.VariantBool)] bool value);

	[PreserveSig]
	int get_ClientApplicationID(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int put_ClientApplicationID(
		[MarshalAs(UnmanagedType.BStr)] string value);

	[PreserveSig]
	int get_IncludePotentiallySupersededUpdates(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int put_IncludePotentiallySupersededUpdates(
		[MarshalAs(UnmanagedType.VariantBool)] bool value);

	[PreserveSig]
	int get_ServerSelection(
		out WuaServerSelection retval);

	[PreserveSig]
	int put_ServerSelection(
		/* [in] */ WuaServerSelection value);

	[PreserveSig]
	int BeginSearch(
		[MarshalAs(UnmanagedType.BStr)] string criteria,
		[MarshalAs(UnmanagedType.IUnknown)] object? onCompleted,
		[MarshalAs(UnmanagedType.Struct)] object? state,
		out ISearchJob retval);

	[PreserveSig]
	int EndSearch(
		ISearchJob searchJob,
		out ISearchResult retval);

	[PreserveSig]
	int EscapeString(
		[MarshalAs(UnmanagedType.BStr)] string unescaped,
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int QueryHistory(
		int startIndex,
		int count,
		out IUpdateHistoryEntryCollection retval);

	[PreserveSig]
	int Search(
		[MarshalAs(UnmanagedType.BStr)] string criteria,
		out ISearchResult retval);

	[PreserveSig]
	int get_Online(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int put_Online(
		[MarshalAs(UnmanagedType.VariantBool)] bool value);

	[PreserveSig]
	int GetTotalHistoryCount(
		out int retval);

	[PreserveSig]
	int get_ServiceID(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int put_ServiceID(
		[MarshalAs(UnmanagedType.BStr)] string value);
}
