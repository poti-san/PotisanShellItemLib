namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("D7540241-F9A1-4364-BEFC-DBCD2C4395B7")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfCompositionView
{
	[PreserveSig]
	int GetOwnerClsid(
		out Guid pclsid);

	[PreserveSig]
	int GetRange(
		out ITfRange ppRange);
}