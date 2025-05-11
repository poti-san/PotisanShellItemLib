namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("7DC9D5F9-9ED9-44ec-9BBF-0600BB589FBB")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMF2DBuffer
{
	[PreserveSig]
	int Lock2D(
		out nint ppbScanline0,
		out int plPitch);

	[PreserveSig]
	int Unlock2D();

	[PreserveSig]
	int GetScanline0AndPitch(
		out nint pbScanline0,
		out int plPitch);

	[PreserveSig]
	int IsContiguousFormat(
		[MarshalAs(UnmanagedType.Bool)] out bool pfIsContiguous);

	[PreserveSig]
	int GetContiguousLength(
		out uint pcbLength);

	[PreserveSig]
	int ContiguousCopyTo(
		ref byte pbDestBuffer,
		uint cbDestBuffer);

	[PreserveSig]
	int ContiguousCopyFrom(
		in byte pbSrcBuffer,
		uint cbSrcBuffer);
}

[ComImport]
[Guid("33ae5ea6-4316-436f-8ddd-d73d22f829ec")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMF2DBuffer2 // IMF2DBuffer
{
	#region IMF2DBuffer

	[PreserveSig]
	int Lock2D(
		out nint ppbScanline0, out int plPitch);

	[PreserveSig]
	int Unlock2D();

	[PreserveSig]
	int GetScanline0AndPitch(
		out nint pbScanline0, out int plPitch);

	[PreserveSig]
	int IsContiguousFormat(
		[MarshalAs(UnmanagedType.Bool)] out bool pfIsContiguous);

	[PreserveSig]
	int GetContiguousLength(
		out uint pcbLength);

	[PreserveSig]
	int ContiguousCopyTo(
		ref byte pbDestBuffer, uint cbDestBuffer);

	[PreserveSig]
	int ContiguousCopyFrom(
		in byte pbSrcBuffer, uint cbSrcBuffer);

	#endregion // IMF2DBuffer

	[PreserveSig]
	int Lock2DSize(
		MF2DBufferLockFlag lockFlags,
		out nint ppbScanline0,
		out int plPitch,
		out nint ppbBufferStart,
		out uint pcbBufferLength);

	[PreserveSig]
	int Copy2DTo(
		IMF2DBuffer2 pDestBuffer);
}