namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0002E000-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IEnumGUID
{
	[PreserveSig]
	int Next(
		uint celt,
		out Guid rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumGUID ppenum);
}