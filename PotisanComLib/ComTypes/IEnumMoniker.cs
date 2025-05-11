namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00000102-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IEnumMoniker
{
	[PreserveSig]
	int Next(
		uint celt,
		out IMoniker rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumMoniker ppenum);
}