namespace Potisan.Windows.Network.ComTypes;

[ComImport]
[Guid("24B7E9B5-E38F-4685-851B-00892CF5F940")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface INetSharingPortMappingProps // IDispatch
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
	int get_Name(
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrName);

	[PreserveSig]
	int get_IPProtocol(
		out byte pucIPProt);

	[PreserveSig]
	int get_ExternalPort(
		out int pusPort);

	[PreserveSig]
	int get_InternalPort(
		out int pusPort);

	[PreserveSig]
	int get_Options(
		out uint pdwOptions);

	[PreserveSig]
	int get_TargetName(
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrTargetName);

	[PreserveSig]
	int get_TargetIPAddress(
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrTargetIPAddress);

	[PreserveSig]
	int get_Enabled(
		[MarshalAs(UnmanagedType.VariantBool)] out bool pbool);
}