namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("3AA7AF7E-9B36-420c-A8E3-F77D4674A488")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IKnownFolder
{
	[PreserveSig]
	int GetId(
		out Guid pkfid);

	[PreserveSig]
	int GetCategory(
		out KnownFolderCategory pCategory);

	[PreserveSig]
	int GetShellItem(
		uint dwFlags,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[PreserveSig]
	int GetPath(
		uint dwFlags,
		[MarshalAs(UnmanagedType.LPWStr)] out string ppszPath);

	[PreserveSig]
	int SetPath(
		uint dwFlags,
		string pszPath);

	[PreserveSig]
	int GetIDList(
		uint dwFlags,
		out nint ppidl);

	[PreserveSig]
	int GetFolderType(
		out FolderTypeID pftid);

	[PreserveSig]
	int GetRedirectionCapabilities(
		out KnownFolderRedirectionCapability pCapabilities);

	[PreserveSig]
	int GetFolderDefinition(
		out KNOWNFOLDER_DEFINITION pKFD);
}