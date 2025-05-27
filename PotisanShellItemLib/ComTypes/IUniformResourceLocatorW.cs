namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("cabb0da0-da57-11cf-9974-0020afd79762")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUniformResourceLocatorW
{
	[PreserveSig]
	int SetURL(
		[MarshalAs(UnmanagedType.LPWStr)] string pcszURL,
		uint dwInFlags);

	[PreserveSig]
	int GetURL(
		[MarshalAs(UnmanagedType.LPWStr)] out string ppszURL);

	[PreserveSig]
	int InvokeCommand(
		in URLINVOKECOMMANDINFOW purlici);
}