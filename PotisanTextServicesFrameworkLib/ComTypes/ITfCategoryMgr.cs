using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("c3acefb5-f69d-4905-938f-fcadcf4be830")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfCategoryMgr
{
	[PreserveSig]
	int RegisterCategory(
		in Guid rclsid,
		in Guid rcatid,
		in Guid rguid);

	[PreserveSig]
	int UnregisterCategory(
		in Guid rclsid,
		in Guid rcatid,
		in Guid rguid);

	[PreserveSig]
	int EnumCategoriesInItem(
		in Guid rguid,
		out IEnumGUID ppEnum);

	[PreserveSig]
	int EnumItemsInCategory(
		in Guid rcatid,
		out IEnumGUID ppEnum);

	[PreserveSig]
	int FindClosestCategory(
		in Guid rguid,
		out Guid pcatid,
		in Guid ppcatidList,
		uint ulCount);

	[PreserveSig]
	int RegisterGUIDDescription(
		in Guid rclsid,
		in Guid rguid,
		[MarshalAs(UnmanagedType.I2)] in char pchDesc,
		uint cch);

	[PreserveSig]
	int UnregisterGUIDDescription(
		in Guid rclsid,
		in Guid rguid);

	[PreserveSig]
	int GetGUIDDescription(
		in Guid rguid,
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrDesc);

	[PreserveSig]
	int RegisterGUIDDWORD(
		in Guid rclsid,
		in Guid rguid,
		uint dw);

	[PreserveSig]
	int UnregisterGUIDDWORD(
		in Guid rclsid,
		in Guid rguid);

	[PreserveSig]
	int GetGUIDDWORD(
		in Guid rguid,
		out uint pdw);

	[PreserveSig]
	int RegisterGUID(
		in Guid rguid,
		out TFGuidAtom pguidatom);

	[PreserveSig]
	int GetGUID(
		TFGuidAtom guidatom,
		out Guid pguid);

	[PreserveSig]
	int IsEqualTfGuidAtom(
		TFGuidAtom guidatom,
		in Guid rguid,
		[MarshalAs(UnmanagedType.Bool)] out bool pfEqual);
}
