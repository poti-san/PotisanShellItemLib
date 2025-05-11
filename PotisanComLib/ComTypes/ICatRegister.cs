namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0002E012-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface ICatRegister
{
	[PreserveSig]
	int RegisterCategories(
		uint cCategories,
		[MarshalAs(UnmanagedType.LPArray)] ComCategoryInfo[] rgCategoryInfo);

	[PreserveSig]
	int UnRegisterCategories(
		uint cCategories,
		in Guid rgcatid);

	[PreserveSig]
	int RegisterClassImplCategories(
		in Guid rclsid,
		uint cCategories,
		in Guid rgcatid);

	[PreserveSig]
	int UnRegisterClassImplCategories(
		in Guid rclsid,
		uint cCategories,
		in Guid rgcatid);

	[PreserveSig]
	int RegisterClassReqCategories(
		in Guid rclsid,
		uint cCategories,
		in Guid rgcatid);

	[PreserveSig]
	int UnRegisterClassReqCategories(
		in Guid rclsid,
		uint cCategories,
		in Guid rgcatid);
}