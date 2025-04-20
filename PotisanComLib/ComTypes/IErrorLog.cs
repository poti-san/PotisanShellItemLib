namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("3127CA40-446E-11CE-8135-00AA004BB851")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IErrorLog
{
	[PreserveSig]
	int AddError(
		[MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
		ComExceptionInfo pExcepInfo);
}