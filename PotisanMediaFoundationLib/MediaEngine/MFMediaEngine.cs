#pragma warning disable CA1051 // 参照可能なインスタンス フィールドを宣言しません

using System.Diagnostics;

using Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

namespace Potisan.Windows.MediaFoundation.MediaEngine;

public class MFMediaEngine(object? o) : ComUnknownWrapperBase<IMFMediaEngine>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFMediaError> ErrorNoThrow
		=> new(_obj.GetError(out var x), new(x));

	public MFMediaError Error
		=> ErrorNoThrow.Value;

	public ComResult SetErrorCodeNoThrow(MFMediaEngineError error)
		=> new(_obj.SetErrorCode(error));

	public MFMediaEngineError ErrorCode
	{
		set => SetErrorCodeNoThrow(value).ThrowIfError();
	}

	public ComResult SetSourceElementsNoThrow(MFMediaEngineSrcElements srcElements)
		=> new(_obj.SetSourceElements((IMFMediaEngineSrcElements)srcElements.WrappedObject!));

	public MFMediaEngineSrcElements SourceElements
	{
		set => SetSourceElementsNoThrow(value);
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> SourceNoThrow
		=> new(_obj.GetCurrentSource(out var x), x);

	public ComResult SetSourceNoThrow(string url)
		=> new(_obj.SetSource(url));

	public string Source
	{
		get => SourceNoThrow.Value;
		set => SetSourceNoThrow(value);
	}

	public MFMediaEngineNetwork NetworkState
		=> (MFMediaEngineNetwork)_obj.GetNetworkState();

	public ComResult SetPreloadNoThrow(MFMediaEnginePreload preload)
		=> new(_obj.SetPreload(preload));

	public MFMediaEnginePreload Preload
	{
		get => _obj.GetPreload();
		set => SetPreloadNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFMediaTimeRange> BufferedNoThrow
		=> new(_obj.GetBuffered(out var x), new(x));

	public MFMediaTimeRange Buffered
		=> BufferedNoThrow.Value;

	public ComResult LoadNoThrow()
		=> new(_obj.Load());

	public void Load()
		=> LoadNoThrow().ThrowIfError();

	public ComResult<MFMediaEngineCanPlay> GetCanPlayTypeNoThrow(string type)
		=> new(_obj.CanPlayType(type, out var x), x);

	public MFMediaEngineCanPlay GetCanPlayType(string type)
		=> GetCanPlayTypeNoThrow(type).Value;

	public MFMediaEngineReady ReadyState
		=> (MFMediaEngineReady)_obj.GetReadyState();

	public bool IsSeeking
		=> _obj.IsSeeking();

	public ComResult SetCurrentTimeNoThrow(double seekTime)
		=> new(_obj.SetCurrentTime(seekTime));

	public double CurrentTime
	{
		get => _obj.GetCurrentTime();
		set => SetCurrentTimeNoThrow(value).ThrowIfError();
	}

	public double StartTime
		=> _obj.GetStartTime();

	public double Duration
		=> _obj.GetDuration();

	public bool IsPaused
		=> _obj.IsPaused();

	public ComResult SetDefaultPlaybackRateNoThrow(double seekTime)
		=> new(_obj.SetDefaultPlaybackRate(seekTime));

	public double DefaultPlaybackRate
	{
		get => _obj.GetDefaultPlaybackRate();
		set => SetDefaultPlaybackRateNoThrow(value).ThrowIfError();
	}

	public ComResult SetPlaybackRateNoThrow(double seekTime)
		=> new(_obj.SetPlaybackRate(seekTime));

	public double PlaybackRate
	{
		get => _obj.GetPlaybackRate();
		set => SetPlaybackRateNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFMediaTimeRange> PlayedNoThrow
		=> new(_obj.GetPlayed(out var x), new(x));

	public MFMediaTimeRange Played
		=> PlayedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFMediaTimeRange> SeekableNoThrow
		=> new(_obj.GetSeekable(out var x), new(x));

	public MFMediaTimeRange Seekable
		=> SeekableNoThrow.Value;

	public bool IsEnded
		=> _obj.IsEnded();

	public ComResult SetAutoPlayNoThrow(bool autoPlay)
		=> new(_obj.SetAutoPlay(autoPlay));

	public bool AutoPlay
	{
		get => _obj.GetAutoPlay();
		set => SetAutoPlayNoThrow(value).ThrowIfError();
	}

	public ComResult SetLoopNoThrow(bool autoPlay)
		=> new(_obj.SetLoop(autoPlay));

	public bool Loop
	{
		get => _obj.GetLoop();
		set => SetLoopNoThrow(value).ThrowIfError();
	}

	public ComResult PlayNoThrow()
		=> new(_obj.Play());

	public void Play()
		=> PlayNoThrow().ThrowIfError();

	public ComResult PauseNoThrow()
		=> new(_obj.Pause());

	public void Pause()
		=> PauseNoThrow().ThrowIfError();

	public ComResult SetMutedNoThrow(bool autoPlay)
		=> new(_obj.SetMuted(autoPlay));

	public bool Muted
	{
		get => _obj.GetMuted();
		set => SetMutedNoThrow(value).ThrowIfError();
	}

	public ComResult SetVolumeNoThrow(double seekTime)
		=> new(_obj.SetVolume(seekTime));

	/// <summary>
	/// 相対ボリュームを取得・設定します。範囲は0～1です。
	/// </summary>
	public double Volume
	{
		get => _obj.GetVolume();
		set => SetVolumeNoThrow(value).ThrowIfError();
	}

	public bool HasVideo
		=> _obj.HasVideo();

	public bool HasAudio
		=> _obj.HasAudio();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(uint Width, uint Height)> NativeVideoSizeNoThrow
		=> new(_obj.GetNativeVideoSize(out var x, out var y), (x, y));

	public (uint Width, uint Height) NativeVideoSize
		=> NativeVideoSizeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(uint Width, uint Height)> VideoAspectRatioNoThrow
		=> new(_obj.GetVideoAspectRatio(out var x, out var y), (x, y));

	public (uint Width, uint Height) VideoAspectRatio
		=> VideoAspectRatioNoThrow.Value;

	public ComResult ShutdownNoThrow()
		=> new(_obj.Shutdown());

	public void Shutdown()
		=> ShutdownNoThrow().ThrowIfError();

	public ComResult TransferVideoFrameNoThrow(object destinationSurface, in MFVideoNormalizedRect source, in Rect destination, MFARGB borderColor)
		=> new(_obj.TransferVideoFrame(destinationSurface, source, destination, borderColor));

	public void TransferVideoFrame(object destinationSurface, in MFVideoNormalizedRect source, in Rect destination, MFARGB borderColor)
		=> TransferVideoFrameNoThrow(destinationSurface, source, destination, borderColor).Throw();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<long> OnVideoStreamTickPointsNoThrow
		=> new(_obj.OnVideoStreamTick(out var x), x);

	public long OnVideoStreamTickPoints
		=> OnVideoStreamTickPointsNoThrow.Value;
}

/// <summary>
/// MF_MEDIA_ENGINE_NETWORK
/// </summary>
public enum MFMediaEngineNetwork : uint
{
	Empty = 0,
	Idle = 1,
	Loading = 2,
	NoSource = 3
}

/// <summary>
/// MF_MEDIA_ENGINE_READY
/// </summary>
public enum MFMediaEngineReady : uint
{
	HaveNothing = 0,
	HaveMetadata = 1,
	HaveCurrentData = 2,
	HaveFutureData = 3,
	HaveEnoughData = 4
}

/// <summary>
/// MF_MEDIA_ENGINE_CANPLAY
/// </summary>
public enum MFMediaEngineCanPlay : uint
{
	NotSupported = 0,
	Maybe = 1,
	Probably = 2
}

/// <summary>
/// MF_MEDIA_ENGINE_PRELOAD
/// </summary>
public enum MFMediaEnginePreload : uint
{
	Missing = 0,
	Empty = 1,
	None = 2,
	Metadata = 3,
	Automatic = 4
}

public struct Rect
{
	public int Left;
	public int Top;
	public int Right;
	public int Bottom;
}

public struct MFVideoNormalizedRect
{
	public float Left;
	public float Top;
	public float Right;
	public float Bottom;
}
