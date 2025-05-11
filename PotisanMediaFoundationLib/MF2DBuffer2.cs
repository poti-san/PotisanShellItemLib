using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MF2DBuffer2(object? o) : MF2DBuffer(o)
{
	protected new readonly IMF2DBuffer2 _obj = o != null ? (IMF2DBuffer2)o : null!;

	public ComResult<(nint Scanline0, int Pitch, nint BufferStart, uint BufferLength)> Lock2DSizeNoThrow(MF2DBufferLockFlag lockFlags)
		=> new(_obj.Lock2DSize(lockFlags, out var x1, out var x2, out var x3, out var x4), (x1, x2, x3, x4));

	public (nint Scanline0, int Pitch, nint BufferStart, uint BufferLength) Lock2DSize(MF2DBufferLockFlag lockFlags)
		=> Lock2DSizeNoThrow(lockFlags).Value;

	public ComResult CopyToNoThrow(MF2DBuffer2 destination)
		=> new(_obj.Copy2DTo(destination._obj));

	public void CopyTo(MF2DBuffer2 destination)
		=> CopyToNoThrow(destination).ThrowIfError();
}

[Flags]
public enum MF2DBufferLockFlag : uint
{
	Read = 0x1,
	Write = 0x2,
	ReadWrite = 0x3,
}
