namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00000105-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IEnumSTATDATA
{
	[PreserveSig]
	int Next(
		uint celt,
		[Out] ComStatData rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumSTATDATA ppenum);
}
