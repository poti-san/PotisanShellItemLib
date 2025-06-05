using Potisan.Windows.Com;
using Potisan.Windows.Dxgi.ComTypes;

namespace Potisan.Windows.Dxgi;

/// <summary>
/// レンダリングデータを出力する前の１つ以上のサーフェス。
/// </summary>
/// <param name="o"></param>
public class DxgiSwapChain(object? o) : ComUnknownWrapperBase<IDXGISwapChain>(o)
{
	public const int MaxBuffers = 16;

	public DxgiDeviceSubObject DxgiDeviceSubObject { get; } = new(o);

	public ComResult PresentNoThrow(uint syncInterval, DxgiPresentFlag flags)
		=> new(_obj.Present(syncInterval, (uint)flags));

	public void Present(uint syncInterval, DxgiPresentFlag flags)
		=> PresentNoThrow(syncInterval, flags).ThrowIfError();

	public ComResult<object> GetBufferNoThrow(uint index, in Guid iid)
		=> new(_obj.GetBuffer(index, iid, out var x), x!);

	public object GetBuffer(uint index, in Guid iid)
		=> GetBufferNoThrow(index, iid).Value;

	public ComResult<TWrapper> GetBufferNoThrow<TWrapper, TInterface>(uint index)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(GetBufferNoThrow(index, typeof(TInterface).GUID));

	public TWrapper GetBuffer<TWrapper, TInterface>(uint index)
		where TWrapper : IComUnknownWrapper
		=> GetBufferNoThrow<TWrapper, TInterface>(index).Value;

	public ComResult SetFullscreenStateNoThrow(bool fullscreen, DxgiOutput target)
		=> new(_obj.SetFullscreenState(fullscreen, (IDXGIOutput)target.WrappedObject!));

	public void SetFullscreenState(bool fullscreen, DxgiOutput target)
		=> SetFullscreenStateNoThrow(fullscreen, target).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(bool Fullscreen, DxgiOutput? Target)> FullscreenStateNoThrow
		=> new(_obj.GetFullscreenState(out var fullscreen, out var x), (fullscreen, x != null ? new(x) : null));

	public (bool Fullscreen, DxgiOutput? Target) FullscreenState
		=> FullscreenStateNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DxgiSwapChainDescription> DescriptionNoThrow
		=> new(_obj.GetDesc(out var x), x);

	public DxgiSwapChainDescription Description
		=> DescriptionNoThrow.Value;

	public ComResult ResizeBuffersNoThrow(uint bufferCount, uint width = 0, uint height = 0, DxgiFormat format = DxgiFormat.Unknown, DxgiSwapChainFlag flags = 0)
		=> new(_obj.ResizeBuffers(bufferCount, width, height, format, (uint)flags));

	public void ResizeBuffers(uint bufferCount, uint width = 0, uint height = 0, DxgiFormat format = DxgiFormat.Unknown, DxgiSwapChainFlag flags = 0)
		=> ResizeBuffersNoThrow(bufferCount, width, height, format, flags).ThrowIfError();

	public ComResult ResizeTargetNoThrow(in DxgiModeDesc newTargetParameters)
		=> new(_obj.ResizeTarget(newTargetParameters));

	public void ResizeTarget(in DxgiModeDesc newTargetParameters)
		=> ResizeTargetNoThrow(newTargetParameters).ThrowIfError();

	public ComResult<DxgiOutput> ContainingOutputNoThrow
		=> new(_obj.GetContainingOutput(out var x), new(x));

	public DxgiOutput ContainingOutput
		=> ContainingOutputNoThrow.Value;

	public ComResult<DxgiFrameStatistics> FrameStatisticsNoThrow
		=> new(_obj.GetFrameStatistics(out var x), x);

	public DxgiFrameStatistics FrameStatistics
		=> FrameStatisticsNoThrow.Value;

	public ComResult<uint> LastPresentCountNoThrow
		=> new(_obj.GetLastPresentCount(out var x), x);

	public uint LastPresentCount
		=> LastPresentCountNoThrow.Value;
}

/// <summary>
/// DXGI_PRESENT_*
/// </summary>
[Flags]
public enum DxgiPresentFlag : uint
{
	Test = 0x00000001,
	DoNotSequence = 0x00000002,
	Restart = 0x00000004,
	DoNotWait = 0x00000008,
	StereoPreferRight = 0x00000010,
	StereoTemporaryMono = 0x00000020,
	RestrictToOutput = 0x00000040,
	UseDuration = 0x00000100,
	AllowTearing = 0x00000200,
}
