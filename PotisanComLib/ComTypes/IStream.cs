namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000000c-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IStream : ISequentialStream
{
	[PreserveSig]
	int Seek(
		long dlibMove,
		ComStreamSeek dwOrigin,
		out ulong plibNewPosition);

	[PreserveSig]
	int SetSize(
		ulong libNewSize);

	[PreserveSig]
	int CopyTo(
		IStream pstm,
		ulong cb,
		out ulong pcbRead,
		out ulong pcbWritten);

	[PreserveSig]
	int Commit(
		ComStorageCommit grfCommitFlags);

	[PreserveSig]
	int Revert();

	[PreserveSig]
	int LockRegion(
		ulong libOffset,
		ulong cb,
		uint dwLockType);

	[PreserveSig]
	int UnlockRegion(
		ulong libOffset,
		ulong cb,
		uint dwLockType);

	[PreserveSig]
	int Stat(
		out STATSTG pstatstg,
		uint grfStatFlag);

	[PreserveSig]
	int Clone(
		out IStream? ppstm);
}

public struct STATSTG
{
	public nint pwcsName; // LPOLESTR
	public uint type;
	public ulong cbSize;
	public FileTime mtime;
	public FileTime ctime;
	public FileTime atime;
	public uint grfMode;
	public uint grfLocksSupported;
	public Guid clsid;
	public uint grfStateBits;
	public uint reserved;

#pragma warning disable IDE0251 // メンバーを 'readonly' にする
	public ComStatStorage GetAndFree()
	{
		try
		{
			return new(Marshal.PtrToStringUni(pwcsName), type, cbSize, mtime, ctime, atime,
				(ComStorageMode)grfMode, (ComStorageLockType)grfLocksSupported, clsid, grfStateBits, reserved);
		}
		finally
		{
			Marshal.FreeCoTaskMem(pwcsName);
		}
	}
#pragma warning restore IDE0251
}