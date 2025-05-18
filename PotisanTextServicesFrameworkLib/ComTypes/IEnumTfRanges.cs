namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("f99d3f40-8e32-11d2-bf46-00105a2799b5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumTfRanges
{
	[PreserveSig]
	int Clone(
		out IEnumTfRanges ppEnum);

	[PreserveSig]
	int Next(
		uint ulCount,
		out ITfRange ppRange,
		out uint pcFetched);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Skip(
		uint ulCount);
}