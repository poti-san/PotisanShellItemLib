namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00000114-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IOleWindow
{
	[PreserveSig]
	int GetWindow(
		out nint phwnd);

	[PreserveSig]
	int ContextSensitiveHelp(
		[MarshalAs(UnmanagedType.Bool)] bool fEnterMode);
}