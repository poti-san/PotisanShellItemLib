namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface ISequentialStream
{
	[PreserveSig]
	int Read(
		ref byte pv,
		uint cb,
		out uint pcbRead);

	[PreserveSig]
	int Write(
		ref byte pv,
		uint cb,
		out uint pcbWritten);
}