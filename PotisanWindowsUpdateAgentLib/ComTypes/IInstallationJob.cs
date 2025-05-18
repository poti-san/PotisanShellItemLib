using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("5c209f0b-bad5-432a-9556-4699bed2638a")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IInstallationJob // IDispatch
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
	int get_AsyncState(
		[MarshalAs(UnmanagedType.Struct)] out object retval);

	[PreserveSig]
	int get_IsCompleted(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_Updates(
		out IUpdateCollection retval);

	[PreserveSig]
	int CleanUp();

	[PreserveSig]
	int GetProgress(
		out IInstallationProgress retval);

	[PreserveSig]
	int RequestAbort();
}