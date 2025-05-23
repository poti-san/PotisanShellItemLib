namespace Potisan.Windows.Network.ComTypes;

[ComImport]
[Guid("C08956B6-1CD3-11D1-B1C5-00805FC1270E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface INetSharingConfiguration // IDispatch
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
	int get_SharingEnabled(
		[MarshalAs(UnmanagedType.VariantBool)] out bool pbEnabled);

	[PreserveSig]
	int get_SharingConnectionType(
		out SharingConnectionType pType);

	[PreserveSig]
	int DisableSharing();

	[PreserveSig]
	int EnableSharing(
		SharingConnectionType Type);

	[PreserveSig]
	int get_InternetFirewallEnabled(
		[MarshalAs(UnmanagedType.VariantBool)] out bool pbEnabled);

	[PreserveSig]
	int DisableInternetFirewall();

	[PreserveSig]
	int EnableInternetFirewall();

	[PreserveSig]
	int get_EnumPortMappings(
		SharingConnectionEnumFlag Flags,
		out INetSharingPortMappingCollection ppColl);

	[PreserveSig]
	int AddPortMapping(
		[MarshalAs(UnmanagedType.BStr)] string bstrName,
		byte ucIPProtocol,
		ushort usExternalPort,
		ushort usInternalPort,
		uint dwOptions,
		[MarshalAs(UnmanagedType.BStr)] string bstrTargetNameOrIPAddress,
		IcsTargetType eTargetType,
		out INetSharingPortMapping ppMapping);

	[PreserveSig]
	int RemovePortMapping(
		INetSharingPortMapping pMapping);
}