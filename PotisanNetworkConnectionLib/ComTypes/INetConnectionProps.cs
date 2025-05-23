namespace Potisan.Windows.Network.ComTypes;

[ComImport]
[Guid("F4277C95-CE5B-463D-8167-5662D9BCAA72")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface INetConnectionProps // IDispatch
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
	int get_Guid(
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrGuid);

	[PreserveSig]
	int get_Name(
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrName);

	[PreserveSig]
	int get_DeviceName(
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrDeviceName);

	[PreserveSig]
	int get_Status(
		out NetConStatus pStatus);

	[PreserveSig]
	int get_MediaType(
		out NetConMediaType pMediaType);

	[PreserveSig]
	int get_Characteristics(
		out uint pdwFlags);
}