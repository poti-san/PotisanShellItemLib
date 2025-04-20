using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MF2DBuffer(object? o) : ComUnknownWrapperBase<IMF2DBuffer>(o)
{
	public ComResult<(nint Scanline0, int Pitch)> Lock2DNoThrow()
		=> new(_obj.Lock2D(out var x1, out var x2), (x1, x2));

	public (nint Scanline0, int Pitch) Lock2D()
		=> Lock2DNoThrow().Value;

	public ComResult Unlock2DNoThrow()
		=> new(_obj.Unlock2D());

	public void Unlock2D()
		=> Unlock2DNoThrow().ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(nint Scanline0, int Pitch)> Scanline0AndPitchNoThrow
		=> new(_obj.GetScanline0AndPitch(out var x1, out var x2), (x1, x2));

	public (nint Scanline0, int Pitch) Scanline0AndPitch
		=> Scanline0AndPitchNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsContiguousFormatNoThrow
		=> new(_obj.IsContiguousFormat(out var x), x);

	public bool IsContiguousFormat
		=> IsContiguousFormatNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> ContiguousLengthNoThrow
		=> new(_obj.GetContiguousLength(out var x), x);

	public uint ContiguousLength
		=> ContiguousLengthNoThrow.Value;

	public ComResult ContiguousCopyToNoThrow(Span<byte> buffer)
		=> new(_obj.ContiguousCopyTo(MemoryMarshal.GetReference(buffer), (uint)buffer.Length));

	public void ContiguousCopyTo(Span<byte> buffer)
		=> ContiguousCopyToNoThrow(buffer).ThrowIfError();

	public ComResult ContiguousCopyFromNoThrow(ReadOnlySpan<byte> buffer)
		=> new(_obj.ContiguousCopyFrom(MemoryMarshal.GetReference(buffer), (uint)buffer.Length));

	public void ContiguousCopyFrom(ReadOnlySpan<byte> buffer)
		=> ContiguousCopyFromNoThrow(buffer).ThrowIfError();
}
