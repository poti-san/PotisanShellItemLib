using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFMediaBuffer(object? o) : ComUnknownWrapperBase<IMFMediaBuffer>(o)
{
	public sealed class LockedBuffer(MFMediaBuffer owner, nint p, uint maxLength, uint currentLength) : IDisposable
	{
		public MFMediaBuffer Owner { get; } = owner;
		public nint Pointer { get; private set; } = p;
		public uint MaxLength { get; private set; } = maxLength;
		public uint CurrentLength { get; private set; } = currentLength;

		~LockedBuffer() => Dispose();

		public Span<byte> Span
		{
			get
			{
				if (Pointer == 0) throw new InvalidOperationException();
				unsafe
				{
					return new Span<byte>((void*)Pointer, checked((int)MaxLength));
				}
			}
		}

		public void Dispose()
		{
			if (Pointer == 0)
				return;
			((IMFMediaBuffer)Owner.WrappedObject!).Unlock();
			Pointer = 0;
			MaxLength = 0;
			CurrentLength = 0;
			GC.SuppressFinalize(this);
		}
	}

	public ComResult<LockedBuffer> LockNoThrow()
	{
		return new(_obj.Lock(out var p, out var maxLen, out var curLen), new(this, p, maxLen, curLen));
	}
	public LockedBuffer Lock() => LockNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CurrentLengthNoThrow => new(_obj.GetCurrentLength(out var x), x);
	public ComResult SetCurrentLengthNoThrow(uint value) => new(_obj.SetCurrentLength(value));
	public uint CurrentLength
	{
		get => CurrentLengthNoThrow.Value;
		set => SetCurrentLengthNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> MaxLengthNoThrow => new(_obj.GetMaxLength(out var x), x);
	public uint MaxLength => MaxLengthNoThrow.Value;

	public static ComResult<MFMediaBuffer> CreateMemoryBufferNoThrow(uint maxLength)
	{
		[DllImport("mfplat.dll")]
		static extern int MFCreateMemoryBuffer(uint cbMaxLength, out IMFMediaBuffer ppBuffer);

		return new(MFCreateMemoryBuffer(maxLength, out var x), new(x));
	}

	public static MFMediaBuffer CreateMemoryBuffer(uint maxLength)
		=> CreateMemoryBufferNoThrow(maxLength).Value;
}
