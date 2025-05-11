namespace Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

[ComImport]
[Guid("db71a2fc-078a-414e-9df9-8c2531b0aa6c")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaTimeRange
{
	[PreserveSig]
	uint GetLength();

	[PreserveSig]
	int GetStart(
		uint index,
		out double pStart);

	[PreserveSig]
	int GetEnd(
		uint index,
		out double pEnd);

	[return: MarshalAs(UnmanagedType.Bool)]
	bool ContainsTime(
		double time);

	[PreserveSig]
	int AddRange(
		double startTime,
		double endTime);

	[PreserveSig]
	int Clear();
}
