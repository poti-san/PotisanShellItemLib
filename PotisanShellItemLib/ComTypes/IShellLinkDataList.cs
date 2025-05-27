namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("45e2b4ae-b1c3-11d0-b92f-00a0c90312e1")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellLinkDataList
{
	[PreserveSig]
	int AddDataBlock(
		in byte pDataBlock);

	[PreserveSig]
	int CopyDataBlock(
		uint dwSig,
		out nint ppDataBlock);

	[PreserveSig]
	int RemoveDataBlock(
		uint dwSig);

	[PreserveSig]
	int GetFlags(
		out uint pdwFlags);

	[PreserveSig]
	int SetFlags(
		uint dwFlags);
}