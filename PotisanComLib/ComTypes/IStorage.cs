namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000000b-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IStorage
{
	[PreserveSig]
	int CreateStream(
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
		uint grfMode,
		uint reserved1,
		uint reserved2,
		out IStream? ppstm);

	[PreserveSig]
	int OpenStream(
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
		nint reserved1,
		uint grfMode,
		uint reserved2,
		out IStream ppstm);

	[PreserveSig]
	int CreateStorage(
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
		uint grfMode,
		uint reserved1,
		uint reserved2,
		out IStorage ppstg);

	[PreserveSig]
	int OpenStorage(
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
		IStorage? pstgPriority,
		uint grfMode,
		nint snbExclude,
		uint reserved,
		out IStorage? ppstg);

	[PreserveSig]
	int CopyTo(
		uint ciidExclude,
		in Guid rgiidExclude,
		nint snbExclude,
		IStorage pstgDest);

	[PreserveSig]
	int MoveElementTo(
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
		IStorage? pstgDest,
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsNewName,
		uint grfFlags);

	[PreserveSig]
	int Commit(
		uint grfCommitFlags);

	[PreserveSig]
	int Revert();

	[PreserveSig]
	int EnumElements(
		uint reserved1,
		nint reserved2,
		uint reserved3,
		out IEnumSTATSTG ppenum);

	[PreserveSig]
	int DestroyElement(
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsName);

	[PreserveSig]
	int RenameElement(
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsOldName,
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsNewName);

	[PreserveSig]
	int SetElementTimes(
		[MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
		in long pctime,
		in long patime,
		in long pmtime);

	[PreserveSig]
	int SetClass(
		in Guid clsid);

	[PreserveSig]
	int SetStateBits(
		uint grfStateBits,
		uint grfMask);

	[PreserveSig]
	int Stat(
		out STATSTG pstatstg,
		uint grfStatFlag);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct RemSNB0
{
	public uint ulCntStr;
	public uint ulCntChar;
	public char rgString0;
}