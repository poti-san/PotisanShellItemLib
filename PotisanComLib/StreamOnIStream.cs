using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// IStream COMインターフェイスをラップする.NETのSystem.Streamの実装。
/// </summary>
public class StreamOnIStream(IStream innerStream, bool readable = true, bool seekable = true, bool writable = true) : Stream
{
	private readonly ComStream _innerStream = new(innerStream);
	public bool Readable { get; } = readable;
	public bool Seekable { get; } = seekable;
	public bool Writable { get; } = writable;

	public object InnerStream => _innerStream.WrappedObject!;

	public override bool CanRead => Readable;
	public override bool CanSeek => Seekable;
	public override bool CanWrite => Writable;

	public override long Length => checked((long)_innerStream.Length);

	public override long Position
	{
		get => checked((long)_innerStream.Position);
		set => _innerStream.Position = checked((ulong)value);
	}

	public override void Flush()
	{
		_innerStream.Commit(ComStorageCommit.Default);
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		return checked((int)_innerStream.Read(buffer.AsSpan(offset, count)));
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		return checked((int)_innerStream.Seek(
			offset,
			origin switch
			{
				SeekOrigin.Begin => ComStreamSeek.Begin,
				SeekOrigin.Current => ComStreamSeek.Current,
				SeekOrigin.End => ComStreamSeek.End,
				_ => throw new NotSupportedException(),
			}));
	}

	public override void SetLength(long value)
	{
		_innerStream.Length = checked((ulong)value);
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		_innerStream.Write(buffer.AsSpan(offset, count));
	}
}
