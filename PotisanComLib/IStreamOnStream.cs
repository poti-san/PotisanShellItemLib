using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// .NETのSystem.StreamをラップするIStream COMインターフェイスの実装。
/// </summary>
public sealed partial class IStreamOnStream : IStream
{
	public Stream WrappedStream { get; }

	public IStreamOnStream(Stream stream) => WrappedStream = stream;

	public int Read(ref byte pv, uint cb, out uint pcbRead)
	{
		unsafe
		{
			pcbRead = checked((uint)WrappedStream.Read(new Span<byte>(Unsafe.AsPointer(ref pv), checked((int)cb))));
			return CommonHResults.SOK;
		}
	}

	public int Write(ref byte pv, uint cb, out uint pcbWritten)
	{
		unsafe
		{
			WrappedStream.Write(new ReadOnlySpan<byte>(Unsafe.AsPointer(ref pv), checked((int)cb)));
			pcbWritten = cb;
			return CommonHResults.SOK;
		}
	}

	public int Seek(long dlibMove, ComStreamSeek dwOrigin, out ulong plibNewPosition)
	{
		try
		{
			var seek = dwOrigin switch
			{
				ComStreamSeek.Begin => SeekOrigin.Begin,
				ComStreamSeek.Current => SeekOrigin.Current,
				ComStreamSeek.End => SeekOrigin.End,
				_ => throw new ArgumentOutOfRangeException(nameof(dlibMove)),
			};
			plibNewPosition = checked((ulong)WrappedStream.Seek(dlibMove, seek));
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			plibNewPosition = 0;
			return ex.HResult;
		}
	}

	public int SetSize(ulong libNewSize)
	{
		WrappedStream.SetLength(checked((long)libNewSize));
		return CommonHResults.SOK;
	}

	public int CopyTo(IStream pstm, ulong cb, out ulong pcbRead, out ulong pcbWritten)
	{
		var buffer = GC.AllocateUninitializedArray<byte>(checked((int)cb));
		pcbRead = checked((ulong)WrappedStream.Read(buffer));
		pstm.Write(ref MemoryMarshal.GetArrayDataReference(buffer), checked((uint)cb), out var written);
		pcbWritten = written;
		return CommonHResults.SOK;
	}

	public int Commit(ComStorageCommit grfCommitFlags)
	{
		// TODO フラグチェック
		WrappedStream.Flush();
		return CommonHResults.SOK;
	}

	public int Revert()
	{
		return CommonHResults.ENotImpl;
	}

	public int LockRegion(ulong libOffset, ulong cb, uint dwLockType)
	{
		return CommonHResults.ENotImpl;
	}

	public int UnlockRegion(ulong libOffset, ulong cb, uint dwLockType)
	{
		return CommonHResults.ENotImpl;
	}

	// TODO
	public int Stat(out STATSTG pstatstg, uint grfStatFlag)
	{
		pstatstg = new();
		return CommonHResults.ENotImpl;
	}

	public int Clone(out IStream? ppstm)
	{
		ppstm = null;
		return CommonHResults.ENotImpl;
	}
}
