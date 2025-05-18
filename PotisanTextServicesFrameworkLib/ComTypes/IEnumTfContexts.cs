namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("8f1a7ea6-1654-4502-a86e-b2902344d507")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumTfContexts
{
	[PreserveSig]
	int Clone(
		out IEnumTfContexts ppEnum);

	[PreserveSig]
	int Next(
		uint ulCount,
		out ITfContext rgContext,
		out uint pcFetched);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Skip(
		uint ulCount);
}