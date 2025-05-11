namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("2eb1e945-18b8-4139-9b1a-d5d584818530")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFClock
{
	[PreserveSig]
	int GetClockCharacteristics(
		out uint pdwCharacteristics);

	[PreserveSig]
	int GetCorrelatedTime(
		uint dwReserved,
		out long pllClockTime,
		out long phnsSystemTime);

	[PreserveSig]
	int GetContinuityKey(
		out uint pdwContinuityKey);

	[PreserveSig]
	int GetState(
		uint dwReserved,
		out MFClockState peClockState);

	[PreserveSig]
	int GetProperties(
		out MFClockProperties pClockProperties);
}