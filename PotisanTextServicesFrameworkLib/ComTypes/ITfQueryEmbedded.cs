namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("0fab9bdb-d250-4169-84e5-6be118fdd7a8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfQueryEmbedded
{
	[PreserveSig]
	int QueryInsertEmbedded(
		in Guid pguidService,
		ComFormatEtc pFormatEtc,
		[MarshalAs(UnmanagedType.Bool)] out bool pfInsertable);
}