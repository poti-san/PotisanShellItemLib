namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("8BE2D872-86AA-4d47-B776-32CCA40C7018")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IKnownFolderManager
{
	[PreserveSig]
	int FolderIdFromCsidl(
		int nCsidl,
		out Guid pfid);

	[PreserveSig]
	int FolderIdToCsidl(
		in Guid rfid,
		out int pnCsidl);

	[PreserveSig]
	int GetFolderIds(
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] out Guid[] ppKFId,
		out uint pCount);

	[PreserveSig]
	int GetFolder(
		in Guid rfid,
		out IKnownFolder ppkf);

	[PreserveSig]
	int GetFolderByName(
		string pszCanonicalName,
		out IKnownFolder ppkf);

	[PreserveSig]
	int RegisterFolder(
		in Guid rfid,
		in KNOWNFOLDER_DEFINITION pKFD);

	[PreserveSig]
	int UnregisterFolder(
		in Guid rfid);

	[PreserveSig]
	int FindFolderFromPath(
		string pszPath,
		FffpMode mode,
		out IKnownFolder ppkf);

	[PreserveSig]
	int FindFolderFromIDList(
		nint pidl,
		out IKnownFolder ppkf);

	[PreserveSig]
	int Redirect(
		in Guid rfid,
		nint hwnd,
		KnownFolderRedirectFlag flags,
		[MarshalAs(UnmanagedType.LPWStr)] string? pszTargetPath,
		uint cFolders,
		[MarshalAs(UnmanagedType.LPArray)] Guid[]? pExclusion,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszError);
}
