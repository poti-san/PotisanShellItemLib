namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("bb08f7a9-607a-4384-8623-056892b64371")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfCompartment
{
	[PreserveSig]
	int SetValue(
		TFClientID tid,
		[MarshalAs(UnmanagedType.Struct)] in object pvarValue);

	[PreserveSig]
	int GetValue(
		[MarshalAs(UnmanagedType.Struct)] out object pvarValue);
}