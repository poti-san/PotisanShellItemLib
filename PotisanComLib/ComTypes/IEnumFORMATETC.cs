// TODO

namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00000103-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IEnumFORMATETC
{
	[PreserveSig]
	int Next(
		uint celt,
		[Out] ComFormatEtc rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumFORMATETC ppenum);
}