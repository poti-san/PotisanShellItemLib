namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("101d6610-0990-11d3-8df0-00105a2799b5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfFunctionProvider
{
	[PreserveSig]
	int GetType(
		out Guid pguid);

	[PreserveSig]
	int GetDescription(
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrDesc);

	[PreserveSig]
	int GetFunction(
		in Guid rguid,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppunk);
}