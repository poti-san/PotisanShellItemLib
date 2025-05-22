using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// 読み書きや位置移動が可能なCOMストリーム。
/// </summary>
/// <remarks>
/// <para><c>IStream</c> COMインターフェイスのラッパーです。</para>
/// <para>COMインターフェイスの継承関係からは<see cref="ComSequentialStream"/> COMインターフェイスを継承すべきですが、
/// IStorage COMインターフェイスのCreateStreamメンバー関数で作成したインスタンスが
/// IStream COMインターフェイスは実装してISequentialStream COMインターフェイスを実装しないので例外的に継承しません。</para>
/// </remarks>
public class ComStream(object? o) : ComUnknownWrapperBase<IStream>(o), ICloneable
{
	#region ISequentialStream

	public ComResult<uint> ReadNoThrow(Span<byte> buffer)
		=> new(_obj.Read(ref MemoryMarshal.GetReference(buffer), checked((uint)buffer.Length), out var x), x);

	public uint Read(Span<byte> buffer)
		=> ReadNoThrow(buffer).Value;

	public ComResult<uint> WriteNoThrow(ReadOnlySpan<byte> buffer)
		=> new(_obj.Write(ref MemoryMarshal.GetReference(buffer), checked((uint)buffer.Length), out var x), x);

	public uint Write(ReadOnlySpan<byte> buffer)
		=> WriteNoThrow(buffer).Value;

	#endregion

	public ComResult<ulong> SeekNoThrow(long move, ComStreamSeek origin)
		=> new(_obj.Seek(move, origin, out var x), x);

	public ulong Seek(long move, ComStreamSeek origin)
		=> SeekNoThrow(move, origin).Value;

	public ComResult SetLengthNoThrow(ulong newSize)
		=> new(_obj.SetSize(newSize));

	public ulong Length
	{
		get
		{
			var pos = Seek(0, ComStreamSeek.Current);
			try
			{
				return Seek(0, ComStreamSeek.End);
			}
			finally
			{
				Seek(unchecked((long)pos), ComStreamSeek.Begin);
			}
		}
		set => SetLengthNoThrow(value).ThrowIfError();
	}

	public ComResult<(ulong Read, ulong Written)> CopyToNoThrow(ComStream destination, ulong size)
		=> new(_obj.CopyTo((destination.WrappedObject as IStream)!, size, out var x1, out var x2), (x1, x2));

	public (ulong Read, ulong Written) CopyTo(ComStream destination, ulong size)
		=> CopyToNoThrow(destination, size).Value;

	public ComResult CommitNoThrow(ComStorageCommit flags = ComStorageCommit.Default)
		=> new(_obj.Commit(flags));

	public void Commit(ComStorageCommit flags = ComStorageCommit.Default)
		=> CommitNoThrow(flags).ThrowIfError();

	public ComResult RevertNoThrow()
		=> new(_obj.Revert());

	public void Revert()
		=> RevertNoThrow().ThrowIfError();

	public ComResult LockRegionNoThrow(ulong position, ulong size, ComStorageLockType lockType)
		=> new(_obj.LockRegion(position, size, (uint)lockType));

	public void LockRegion(ulong position, ulong size, ComStorageLockType lockType)
		=> LockRegionNoThrow(position, size, lockType).ThrowIfError();

	public ComResult UnlockRegionNoThrow(ulong position, ulong size, ComStorageLockType lockType)
		=> new(_obj.UnlockRegion(position, size, (uint)lockType));

	public void UnlockRegion(ulong position, ulong size, ComStorageLockType lockType)
		=> UnlockRegionNoThrow(position, size, lockType).ThrowIfError();

	public ComResult<ComStatStorage> StaticalDataNoThrow
		=> new(_obj.Stat(out var x, 0), x.GetAndFree());

	public ComStatStorage StaticalData
		=> StaticalDataNoThrow.Value;

	public ComResult<ComStream> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ComStream Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();

	public static ComResult<ComStream> CreateOnFileNoThrow(string path, ComStorageMode mode, bool creates, uint attrs = 0, ComStream? template = null)
	{
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		static extern int SHCreateStreamOnFileEx(string pszFile, uint grfMode, uint dwAttributes,
			[MarshalAs(UnmanagedType.Bool)] bool fCreate, IStream? pstmTemplate, out IStream ppstm);

		return new(SHCreateStreamOnFileEx(path, (uint)mode, attrs, creates, template?.WrappedObject as IStream, out var x), new(x));
	}

	public static ComStream CreateOnFile(string path, ComStorageMode mode, bool creates, uint attrs = 0, ComStream? template = null)
		=> CreateOnFileNoThrow(path, mode, creates, attrs, template).Value;

	public static ComResult<ComStream> OpenFileNoThrow(string path, ComStorageMode mode = ComStorageMode.Read | ComStorageMode.ShareDenyWrite)
		=> CreateOnFileNoThrow(path, mode, false, 0, null);

	public static ComStream OpenFile(string path, ComStorageMode mode = ComStorageMode.Read | ComStorageMode.ShareDenyWrite)
		=> OpenFileNoThrow(path, mode).Value;

	public static ComResult<ComStream> CreateOnMemoryNoThrow(ReadOnlySpan<byte> init)
	{
		[DllImport("shlwapi.dll")]
		static extern IStream? SHCreateMemStream(in byte pInit, uint cbInit);

		var x = SHCreateMemStream(MemoryMarshal.GetReference(init), checked((uint)init.Length));
		return new(x != null ? CommonHResults.SOK : CommonHResults.EFail, new(x));
	}

	public static ComStream CreateOnMemory(ReadOnlySpan<byte> init)
		=> CreateOnMemoryNoThrow(init).Value;

	public static ComResult<ComStream> CreateOnMemoryNoThrow()
	{
		[DllImport("shlwapi.dll")]
		static extern IStream? SHCreateMemStream(in byte pInit, uint cbInit);

		var x = SHCreateMemStream(Unsafe.NullRef<byte>(), 0);
		return new(x != null ? CommonHResults.SOK : CommonHResults.EFail, new(x));
	}

	public static ComStream CreateOnMemory()
		=> CreateOnMemoryNoThrow().Value;

	// ユーティリティ

	public ulong Position
	{
		get => Seek(0, ComStreamSeek.Current);
		set => Seek(unchecked((long)value), ComStreamSeek.Begin);
	}

	public void ReadExact(Span<byte> buffer)
	{
		var pos = Position;
		try
		{
			var size = Read(buffer);
			if (size != buffer.Length)
				throw new InvalidOperationException();
		}
		catch
		{
			Position = pos;
			throw;
		}
	}

	public void WriteExact(ReadOnlySpan<byte> buffer)
	{
		var pos = Position;
		try
		{
			var size = Write(buffer);
			if (size != buffer.Length)
				throw new InvalidOperationException();
		}
		catch
		{
			Position = pos;
			throw;
		}
	}

	public void ReadAtLeast(Span<byte> buffer)
	{
		var pos = Position;
		try
		{
			Read(buffer);
		}
		catch
		{
			Position = pos;
			throw;
		}
	}

	public void WriteAtLeast(ReadOnlySpan<byte> buffer)
	{
		var pos = Position;
		try
		{
			Write(buffer);
		}
		catch
		{
			Position = pos;
			throw;
		}
	}

	public byte[] ReadAllBytes()
	{
		var curPos = Position;
		try
		{
			Position = 0;
			var buffer = GC.AllocateUninitializedArray<byte>(checked((int)Length));
			ReadExact(buffer);
			return buffer;
		}
		finally
		{
			Position = curPos;
		}
	}
}

/// <summary>
/// ストレージ初期化モード。
/// </summary>
/// <remarks>
/// <c>STGM</c>
/// </remarks>
[Flags]
public enum ComStorageMode : uint
{
	Direct = 0x00000000,
	Transacted = 0x00010000,
	Simple = 0x08000000,
	Read = 0x00000000,
	Write = 0x00000001,
	ReadWrite = 0x00000002,
	ShareDenyNode = 0x00000040,
	ShareDenyRead = 0x00000030,
	ShareDenyWrite = 0x00000020,
	ShareExclusive = 0x00000010,
	Priority = 0x00040000,
	DeleteOnRelease = 0x04000000,
	NoScratch = 0x00100000,
	Create = 0x00001000,
	Convert = 0x00020000,
	FailIfThere = 0x00000000,
	NoSnapshot = 0x00200000,
	DirectSwmr = 0x00400000,
}

// STATSTG
public record class ComStatStorage(
	string? Name,
	uint Type,
	ulong Size,
	FileTime ModifyTime,
	FileTime CreationTime,
	FileTime AccessTime,
	ComStorageMode Mode,
	ComStorageLockType LocksSupported,
	Guid Clsid,
	uint StateBits,
	uint Reserved);

/// <summary>
/// STGTY
/// </summary>
public enum ComStorageType : uint
{
	Storage = 1,
	Stream = 2,
	LockBytes = 3,
	Property = 4,
}

/// <summary>
/// STREAM_SEEK
/// </summary>
public enum ComStreamSeek : uint
{
	Begin = 0,
	Current = 1,
	End = 2,
}

/// <summary>
/// LOCKTYPE
/// </summary>
public enum ComStorageLockType : uint
{
	Write = 1,
	Exclusive = 2,
	OnlyOnce = 4,
}

/// <summary>
/// STGC
/// </summary>
public enum ComStorageCommit : uint
{
	Default = 0,
	Overwrite = 1,
	OnlyIfCurrent = 2,
	DangerouslyCommitMerelyToDiskCache = 4,
	Consolidate = 8,
}