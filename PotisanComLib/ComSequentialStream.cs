using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COM読み書きストリーム。
/// </summary>
/// <remarks>
/// <c>ISequentialStream</c> COMインターフェイスのラッパーです。
/// </remarks>
public class ComSequentialStream(object? o) : ComUnknownWrapperBase<ISequentialStream>(o)
{
	public ComResult<uint> ReadNoThrow(Span<byte> buffer)
		=> new(_obj.Read(ref MemoryMarshal.GetReference(buffer), checked((uint)buffer.Length), out var x), x);

	public uint Read(Span<byte> buffer)
		=> ReadNoThrow(buffer).Value;

	public ComResult<uint> WriteNoThrow(ReadOnlySpan<byte> buffer)
		=> new(_obj.Write(ref MemoryMarshal.GetReference(buffer), checked((uint)buffer.Length), out var x), x);

	public uint Write(ReadOnlySpan<byte> buffer)
		=> WriteNoThrow(buffer).Value;

	// このクラスにはReadExact、ReadAtLeastなどを実装しません。
	// シークできないため、エラー時にポインタを戻せないためです。
}
