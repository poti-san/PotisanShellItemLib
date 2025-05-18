namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("19188cb0-aca9-11d2-afc5-00105a2799b5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumTfProperties
{
	[PreserveSig]
	int Clone(
		out IEnumTfProperties ppEnum);

	[PreserveSig]
	int Next(
		uint ulCount,
		out ITfProperty ppProp,
		out uint pcFetched);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Skip(
		uint ulCount);
}