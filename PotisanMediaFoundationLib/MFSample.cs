using System.Collections.Immutable;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <param name="o">RCWインスタンス。</param>
public class MFSample(object? o) : ComUnknownWrapperWithMFAttribute<IMFSample>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<long> SampleTimeNoThrow
		=> new(_obj.GetSampleTime(out var x), x);

	public ComResult SetSampleTimeNoThrow(long value)
		=> new(_obj.SetSampleTime(value));

	public long SampleTime
	{
		get => SampleTimeNoThrow.Value;
		set => SetSampleTimeNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<long> SampleDurationNoThrow
		=> new(_obj.GetSampleDuration(out var x), x);

	public ComResult SetSampleDurationNoThrow(long value)
		=> new(_obj.SetSampleDuration(value));

	public long SampleDuration
	{
		get => SampleDurationNoThrow.Value;
		set => SetSampleDurationNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> BufferCountNoThrow
		=> new(_obj.GetBufferCount(out var x), x);

	public uint BufferCount
		=> BufferCountNoThrow.Value;

	public ComResult<MFMediaBuffer> GetBufferNoThrow(uint index)
		=> new(_obj.GetBufferByIndex(index, out var x), new(x));

	public MFMediaBuffer GetBuffer(uint index)
		=> GetBufferNoThrow(index).Value;

	public IEnumerable<MFMediaBuffer> EnumerateBuffers()
	{
		var c = BufferCount;
		for (uint i = 0; i < c; i++)
			yield return GetBuffer(i);
	}

	public ImmutableArray<MFMediaBuffer> Buffers
		=> [.. EnumerateBuffers()];

	public ComResult<MFMediaBuffer> ConvertToContiguousBufferNoThrow()
		=> new(_obj.ConvertToContiguousBuffer(out var x), new(x));

	public MFMediaBuffer ConvertToContiguousBuffer()
		=> ConvertToContiguousBufferNoThrow().Value;

	public ComResult AddBufferNoThrow(MFMediaBuffer buffer)
		=> new(_obj.AddBuffer((IMFMediaBuffer)buffer.WrappedObject!));

	public void AddBuffer(MFMediaBuffer buffer)
		=> AddBufferNoThrow(buffer).ThrowIfError();

	public ComResult RemoveBufferNoThrow(uint index)
		=> new(_obj.RemoveBufferByIndex(index));

	public void RemoveBuffer(uint index)
		=> RemoveBufferNoThrow(index).ThrowIfError();

	public ComResult RemoveAllBuffersNoThrow()
		=> new(_obj.RemoveAllBuffers());

	public void RemoveAllBuffers()
		=> RemoveAllBuffersNoThrow().ThrowIfError();

	public ComResult<uint> TotalLengthNoThrow
		=> new(_obj.GetTotalLength(out var x), x);

	public uint TotalLength
		=> TotalLengthNoThrow.Value;

	public ComResult CopyToBufferNoThrow(MFMediaBuffer buffer)
		=> new(_obj.CopyToBuffer((IMFMediaBuffer)buffer.WrappedObject!));

	public void CopyToBuffer(MFMediaBuffer buffer)
		=> CopyToBufferNoThrow(buffer).ThrowIfError();

	public static ComResult<MFSample> CreateNoThrow()
	{
		[DllImport("mfplat.dll")]
		static extern int MFCreateSample(out IMFSample ppIMFSample);

		return new(MFCreateSample(out var x), new(x));
	}

	public static MFSample Create()
		=> CreateNoThrow().Value;

	/// <summary>
	/// 32ビットRGBビットマップを表すサンプルからビットマップを作成します。
	/// サンプルが32ビットRGBビットマップでない場合の呼び出しは想定外です。
	/// </summary>
	/// <param name="width"><see cref="MFMediaType"/>等から取得したビットマップの幅。</param>
	/// <param name="height"><see cref="MFMediaType"/>等から取得したビットマップの高さ。</param>
	/// <returns></returns>
	public Bitmap CreateBitmap32bppRgbFromMFSample(int width, int height)
	{
		var buffer = ConvertToContiguousBuffer();
		using var pixels = buffer.Lock();

		var bmp = new Bitmap(width, height, PixelFormat.Format32bppRgb);
		var bmpData = bmp.LockBits(new(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
		try
		{
			unsafe
			{
				Unsafe.CopyBlock((void*)bmpData.Scan0, (void*)pixels.Pointer, pixels.CurrentLength);
			}
			return bmp;
		}
		finally
		{
			bmp.UnlockBits(bmpData);
		}
	}
}
