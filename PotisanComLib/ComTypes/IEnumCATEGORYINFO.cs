namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0002E011-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IEnumCATEGORYINFO
{
	[PreserveSig]
	int Next(
		uint celt,
		[Out] ComCategoryInfo rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumCATEGORYINFO ppenum);
}
