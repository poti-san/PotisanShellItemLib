#pragma warning disable CA1716 // 識別子はキーワードと同一にすることはできません

namespace Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

[ComImport]
[Guid("98a1b0bb-03eb-4935-ae7c-93c1fa0e1c93")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaEngine
{
	[PreserveSig]
	int GetError(
		out IMFMediaError ppError);

	[PreserveSig]
	int SetErrorCode(
		MFMediaEngineError error);

	[PreserveSig]
	int SetSourceElements(
		IMFMediaEngineSrcElements pSrcElements);

	[PreserveSig]
	int SetSource(
		[MarshalAs(UnmanagedType.BStr)] string pUrl);

	[PreserveSig]
	int GetCurrentSource(
		[MarshalAs(UnmanagedType.BStr)] out string ppUrl);

	[PreserveSig]
	ushort GetNetworkState();

	[PreserveSig]
	MFMediaEnginePreload GetPreload();

	[PreserveSig]
	int SetPreload(
		MFMediaEnginePreload Preload);

	[PreserveSig]
	int GetBuffered(
		out IMFMediaTimeRange ppBuffered);

	[PreserveSig]
	int Load();

	[PreserveSig]
	int CanPlayType(
		[MarshalAs(UnmanagedType.BStr)] string type,
		out MFMediaEngineCanPlay pAnswer);

	[PreserveSig]
	ushort GetReadyState();

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsSeeking();

	[PreserveSig]
	double GetCurrentTime();

	[PreserveSig]
	int SetCurrentTime(
		double seekTime);

	[PreserveSig]
	double GetStartTime();

	[PreserveSig]
	double GetDuration();

	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsPaused();

	[PreserveSig]
	double GetDefaultPlaybackRate();

	[PreserveSig]
	int SetDefaultPlaybackRate(
		double Rate);

	[PreserveSig]
	double GetPlaybackRate();

	[PreserveSig]
	int SetPlaybackRate(
		double Rate);

	[PreserveSig]
	int GetPlayed(
		out IMFMediaTimeRange ppPlayed);

	[PreserveSig]
	int GetSeekable(
		out IMFMediaTimeRange ppSeekable);

	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsEnded();

	[return: MarshalAs(UnmanagedType.Bool)]
	bool GetAutoPlay();

	[PreserveSig]
	int SetAutoPlay(
		[MarshalAs(UnmanagedType.Bool)] bool AutoPlay);

	[return: MarshalAs(UnmanagedType.Bool)]
	bool GetLoop();

	[PreserveSig]
	int SetLoop(
		[MarshalAs(UnmanagedType.Bool)] bool Loop);

	[PreserveSig]
	int Play();

	[PreserveSig]
	int Pause();

	[return: MarshalAs(UnmanagedType.Bool)]
	bool GetMuted();

	[PreserveSig]
	int SetMuted(
		[MarshalAs(UnmanagedType.Bool)] bool Muted);

	[PreserveSig]
	double GetVolume();

	[PreserveSig]
	int SetVolume(
		double Volume);

	[return: MarshalAs(UnmanagedType.Bool)]
	bool HasVideo();

	[return: MarshalAs(UnmanagedType.Bool)]
	bool HasAudio();

	[PreserveSig]
	int GetNativeVideoSize(
		out uint cx,
		out uint cy);

	[PreserveSig]
	int GetVideoAspectRatio(
		out uint cx,
		out uint cy);

	[PreserveSig]
	int Shutdown();

	[PreserveSig]
	int TransferVideoFrame(
		[MarshalAs(UnmanagedType.IUnknown)] object pDstSurf,
		in MFVideoNormalizedRect pSrc,
		in Rect pDst,
		in MFARGB pBorderClr);

	[PreserveSig]
	int OnVideoStreamTick(
		out long pPts);
}