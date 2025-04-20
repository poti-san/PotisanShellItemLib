namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IPropertyBag
{
	[PreserveSig]
	int Read(
		[MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
		[MarshalAs(UnmanagedType.Struct)][Out] out object pVar,
		IErrorLog? pErrorLog);

	[PreserveSig]
	int Write(
		[MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
		[MarshalAs(UnmanagedType.Struct)] in object pVar);
}
