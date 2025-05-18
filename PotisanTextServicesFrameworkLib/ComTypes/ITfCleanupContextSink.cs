namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("01689689-7acb-4e9b-ab7c-7ea46b12b522")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfCleanupContextSink
{
	[PreserveSig]
	int OnCleanupContext(
		TFEditCookie ecWrite,
		ITfContext? pic);
}