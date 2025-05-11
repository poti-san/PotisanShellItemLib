namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000010b-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IPersistFile : IPersist
{
	[PreserveSig]
	int IsDirty();

	[PreserveSig]
	int Load(
		[MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
		uint dwMode);

	[PreserveSig]
	int Save(
		[MarshalAs(UnmanagedType.LPWStr)] string? pszFileName,
		[MarshalAs(UnmanagedType.Bool)] bool fRemember);

	[PreserveSig]
	int SaveCompleted(
		[MarshalAs(UnmanagedType.LPWStr)] string? pszFileName);

	[PreserveSig]
	int GetCurFile(
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszFileName);
}