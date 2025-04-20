using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFByteStream(object? o) : ComUnknownWrapperBase<IMFByteStream>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFByteStreamCapability> CapabilitiesNoThrow
		=> new(_obj.GetCapabilities(out var x), (MFByteStreamCapability)x);

	public MFByteStreamCapability Capabilities
		=> CapabilitiesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ulong> LengthNoThrow
		=> new(_obj.GetLength(out var x), x);

	public ComResult SetLengthNoThrow(ulong value)
		=> new(_obj.SetLength(value));

	public ulong Length
	{
		get => LengthNoThrow.Value;
		set => SetLengthNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ulong> PositionNoThrow
		=> new(_obj.GetCurrentPosition(out var x), x);

	public ComResult SetPositionNoThrow(ulong value)
		=> new(_obj.SetCurrentPosition(value));

	public ulong Position
	{
		get => PositionNoThrow.Value;
		set => SetPositionNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsEndOfStreamNoThrow
		=> new(_obj.IsEndOfStream(out var x), x);

	public bool IsEndOfStream
		=> IsEndOfStreamNoThrow.Value;

	public ComResult<uint> ReadNoThrow(Span<byte> buffer)
		=> new(_obj.Read(ref MemoryMarshal.GetReference(buffer), checked((uint)buffer.Length), out var x), x);

	public uint Read(Span<byte> buffer)
		=> ReadNoThrow(buffer).Value;

	//[PreserveSig]
	//int BeginRead(
	//	ref byte pb,//	uint cb,//	IMFAsyncCallback pCallback,//	[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	//[PreserveSig]
	//int EndRead(
	//	IMFAsyncResult pResult,//	out uint pcbRead);

	public ComResult<uint> WriteNoThrow(ReadOnlySpan<byte> buffer)
		=> new(_obj.Write(in MemoryMarshal.GetReference(buffer), checked((uint)buffer.Length), out var x), x);

	public uint Write(ReadOnlySpan<byte> buffer)
		=> WriteNoThrow(buffer).Value;

	//[PreserveSig]
	//int BeginWrite(
	//	in byte pb,//	uint cb,//	IMFAsyncCallback pCallback,//	[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	//[PreserveSig]
	//int EndWrite(
	//	IMFAsyncResult pResult,//	out uint pcbWritten);

	public ComResult<ulong> SeekNoThrow(MFByteStreamSeekOrigin origin, long offset, MFByteStreamSeekFlag flags = 0)
		=> new(_obj.Seek(origin, offset, (uint)flags, out var x), x);

	public ulong Seek(MFByteStreamSeekOrigin origin, long offset, MFByteStreamSeekFlag flags = 0)
		=> SeekNoThrow(origin, offset, flags).Value;

	public ComResult FlushNoThrow()
		=> new(_obj.Flush());

	public void Flush()
		=> FlushNoThrow().ThrowIfError();

	public ComResult CloseNoThrow()
		=> new(_obj.Close());

	public void Close()
		=> CloseNoThrow().ThrowIfError();

	public static ComResult<MFByteStream> CreateFileNoThrow(
		string url, MFFileAccessMode access, MFFileOpenMode openMode, MFFileFlag flags)
	{
		[DllImport("mfplat.dll", CharSet = CharSet.Unicode)]
		static extern int MFCreateFile(
			MFFileAccessMode AccessMode,
			MFFileOpenMode OpenMode,
			MFFileFlag fFlags,
			string pwszFileURL,
			out IMFByteStream ppIByteStream);

		return new(MFCreateFile(access, openMode, flags, url, out var x), new(x));
	}

	public static MFByteStream CreateFile(
		string url, MFFileAccessMode access, MFFileOpenMode openMode, MFFileFlag flags)
		=> CreateFileNoThrow(url, access, openMode, flags).Value;

	public static ComResult<MFByteStream> OpenFileToReadNoThrow(
		string url, MFFileOpenMode openMode = MFFileOpenMode.FailIfNotExist, MFFileFlag flags = 0)
		=> CreateFileNoThrow(url, MFFileAccessMode.Read, openMode, flags);

	public static MFByteStream OpenFileToRead(
		string url, MFFileOpenMode openMode = MFFileOpenMode.FailIfNotExist, MFFileFlag flags = 0)
		=> OpenFileToReadNoThrow(url, openMode, flags).Value;
}

//	STDAPI MFCreateTempFile(
//		MF_FILE_ACCESSMODE AccessMode,
//		MF_FILE_OPENMODE OpenMode,
//		MF_FILE_FLAGS fFlags,
//		_Out_ IMFByteStream       **ppIByteStream );

//	STDAPI MFBeginCreateFile(
//		MF_FILE_ACCESSMODE AccessMode,
//		MF_FILE_OPENMODE OpenMode,
//		MF_FILE_FLAGS fFlags,
//		LPCWSTR pwszFilePath,
//		IMFAsyncCallback* pCallback,
//		IUnknown* pState,
//		[MarshalAs(UnmanagedType.IUnknown)] out object?  ppCancelCookie);

//	STDAPI MFEndCreateFile(
//		IMFAsyncResult* pResult,
//		_Out_ IMFByteStream **ppFile );

//	STDAPI MFCancelCreateFile(
//		IUnknown* pCancelCookie);

/// <summary>
/// MFBYTESTREAM_SEEK_ORIGIN
/// </summary>
public enum MFByteStreamSeekOrigin
{
	Begin = 0,
	Current = Begin + 1,
}

/// <summary>
/// MFBYTESTREAM_*
/// </summary>
[Flags]
public enum MFByteStreamCapability : uint
{
	IsReadable = 0x00000001,
	IsWritable = 0x00000002,
	IsSeekable = 0x00000004,
	IsRemote = 0x00000008,
	IsDirectory = 0x00000080,
	HasSlowSeek = 0x00000100,
	IsPartiallyDownloaded = 0x00000200,
	ShareWrite = 0x00000400,
	DoesNotUseNetwork = 0x0000080,
}

/// <summary>
/// MFBYTESTREAM_SEEK_FLAG_*
/// </summary>
[Flags]
public enum MFByteStreamSeekFlag : uint
{
	CancelPendingIO = 0x00000001,
}

/// <summary>
/// MF_FILE_ACCESSMODE
/// </summary>
public enum MFFileAccessMode : uint
{
	Read = 1,
	Write = 2,
	ReadWrite = 3
}

/// <summary>
/// MF_FILE_OPENMODE
/// </summary>
public enum MFFileOpenMode : uint
{
	FailIfNotExist = 0,
	FailIfExist = 1,
	ResetIfExist = 2,
	AppendIfExist = 3,
	DeleteIfExist = 4
}

/// <summary>
/// MF_FILE_FLAGS
/// </summary>
[Flags]
public enum MFFileFlag : uint
{
	None = 0,
	NoBuffering = 0x1,
	AllowWriteSharing = 0x2
}
