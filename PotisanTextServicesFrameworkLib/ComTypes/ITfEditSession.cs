namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("aa80e803-2021-11d2-93e0-0060b067b86e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfEditSession
{
	[PreserveSig]
	int DoEditSession(
		TFEditCookie ec);
}