namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000013E-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IServerSecurity
{
	[PreserveSig]
	int QueryBlanket(
		out uint pAuthnSvc,
		out uint pAuthzSvc,
		[MarshalAs(UnmanagedType.LPWStr)] out string pServerPrincName,
		out uint pAuthnLevel,
		out uint pImpLevel,
		out nint pPrivs,
		out uint pCapabilities);

	[PreserveSig]
	int ImpersonateClient();

	[PreserveSig]
	int RevertToSelf();

	[PreserveSig]
	int IsImpersonating();
}
