namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000013D-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IClientSecurity
{
	[PreserveSig]
	int QueryBlanket(
		[MarshalAs(UnmanagedType.IUnknown)] object pProxy,
		out ComAuthenticationService pAuthnSvc,
		out ComAuthorizationService pAuthzSvc,
		[MarshalAs(UnmanagedType.LPWStr)] out string pServerPrincName,
		out ComAuthenticationLevel pAuthnLevel,
		out ComImpersonateLevel pImpLevel,
		out nint pAuthInfo,
		out OleAuthenticationCap pCapabilites);

	[PreserveSig]
	int SetBlanket(
		[MarshalAs(UnmanagedType.IUnknown)] object pProxy,
		ComAuthenticationService dwAuthnSvc,
		ComAuthorizationService dwAuthzSvc,
		[MarshalAs(UnmanagedType.U2)] ref char pServerPrincName,
		ComAuthenticationLevel dwAuthnLevel,
		ComImpersonateLevel dwImpLevel,
		ref byte pAuthInfo,
		OleAuthenticationCap dwCapabilities);

	[PreserveSig]
	int CopyProxy(
		[MarshalAs(UnmanagedType.IUnknown)] object pProxy,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppCopy);
}
