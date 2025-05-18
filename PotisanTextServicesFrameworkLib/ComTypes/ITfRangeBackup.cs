namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("463a506d-6992-49d2-9b88-93d55e70bb16")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfRangeBackup
{
	[PreserveSig]
	int Restore(
		TFEditCookie ec,
		ITfRange? pRange);
}