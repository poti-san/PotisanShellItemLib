namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("045FA593-8799-42b8-BC8D-8968C6453507")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaBuffer
{
	[PreserveSig]
	int Lock(
		out nint ppbBuffer,
		out uint pcbMaxLength,
		out uint pcbCurrentLength);

	[PreserveSig]
	int Unlock();

	[PreserveSig]
	int GetCurrentLength(
		out uint pcbCurrentLength);

	[PreserveSig]
	int SetCurrentLength(
		uint cbCurrentLength);

	[PreserveSig]
	int GetMaxLength(
		out uint pcbMaxLength);
}
