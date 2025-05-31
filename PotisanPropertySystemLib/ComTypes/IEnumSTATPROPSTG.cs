namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("00000139-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumSTATPROPSTG
{
	[PreserveSig]
	int Next(
		uint celt,
		out ComStatPropStorage rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumSTATPROPSTG ppenum);
}