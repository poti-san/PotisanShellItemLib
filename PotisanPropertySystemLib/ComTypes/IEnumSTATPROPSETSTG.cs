namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("0000013B-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumSTATPROPSETSTG
{
	[PreserveSig]
	int Next(
		uint celt,
		out ComStatPropSetStorage rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumSTATPROPSETSTG? ppenum);
}