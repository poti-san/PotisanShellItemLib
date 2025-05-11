namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0002E013-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface ICatInformation
{
	[PreserveSig]
	int EnumCategories(
		uint lcid,
		out IEnumCATEGORYINFO ppenumCategoryInfo);

	[PreserveSig]
	int GetCategoryDesc(
		in Guid rcatid,
		uint lcid,
		[MarshalAs(UnmanagedType.LPWStr)] out string pszDesc);

	[PreserveSig]
	int EnumClassesOfCategories(
		uint cImplemented,
		in Guid rgcatidImpl,
		uint cRequired,
		in Guid rgcatidReq,
		out IEnumGUID ppenumClsid);

	[PreserveSig]
	int IsClassOfCategories(
		in Guid rclsid,
		uint cImplemented,
		in Guid rgcatidImpl,
		uint cRequired,
		in Guid rgcatidReq);

	[PreserveSig]
	int EnumImplCategoriesOfClass(
		in Guid rclsid,
		out IEnumGUID ppenumCatid);

	[PreserveSig]
	int EnumReqCategoriesOfClass(
		in Guid rclsid,
		out IEnumGUID ppenumCatid);
}