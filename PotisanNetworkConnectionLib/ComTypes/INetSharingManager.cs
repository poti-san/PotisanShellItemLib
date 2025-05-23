namespace Potisan.Windows.Network.ComTypes;

[ComImport]
[Guid("C08956B7-1CD3-11D1-B1C5-00805FC1270E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface INetSharingManager // IDispatch
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
	int get_SharingInstalled(
		[MarshalAs(UnmanagedType.VariantBool)] out bool pbInstalled);

	[PreserveSig]
	int get_EnumPublicConnections(
		SharingConnectionEnumFlag Flags,
		out INetSharingPublicConnectionCollection ppColl);

	[PreserveSig]
	int get_EnumPrivateConnections(
		SharingConnectionEnumFlag Flags,
		out INetSharingPrivateConnectionCollection ppColl);

	[PreserveSig]
	int get_INetSharingConfigurationForINetConnection(
		INetConnection pNetConnection,
		out INetSharingConfiguration ppNetSharingConfiguration);

	[PreserveSig]
	int get_EnumEveryConnection(
		out INetSharingEveryConnectionCollection ppColl);

	[PreserveSig]
	int get_NetConnectionProps(
		INetConnection pNetConnection,
		out INetConnectionProps ppProps);
}