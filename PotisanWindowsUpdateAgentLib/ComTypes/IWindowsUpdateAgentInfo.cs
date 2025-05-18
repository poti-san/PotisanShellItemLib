using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("85713fa1-7796-4fa2-be3b-e2d6124dd373")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IWindowsUpdateAgentInfo // IDispatch
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
	int GetInfo(
		[MarshalAs(UnmanagedType.Struct)] object varInfoIdentifier,
		[MarshalAs(UnmanagedType.Struct)] out object retval);
}