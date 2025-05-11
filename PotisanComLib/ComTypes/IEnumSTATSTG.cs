namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000000d-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IEnumSTATSTG
{
	[PreserveSig]
	int Next(
		uint celt,
		out STATSTG rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumSTATSTG ppenum);
}