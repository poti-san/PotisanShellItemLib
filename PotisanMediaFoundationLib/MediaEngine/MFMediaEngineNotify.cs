using System.Diagnostics;

using Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

namespace Potisan.Windows.MediaFoundation.MediaEngine;

public delegate void MFMediaEngineNotifyCallback(MFMediaEngineEvent eventType, nuint param1, uint param2);

/// <summary>
/// MFMediaEngineの作成時に使用する通知オブジェクトです。
/// コールバックをWinFormsで使用する場合はスレッドコンテキストに注意してください。
/// </summary>
/// <param name="onNotify"></param>
public class MFMediaEngineNotify(MFMediaEngineNotifyCallback onNotify) : IMFMediaEngineNotify
{
	private readonly MFMediaEngineNotifyCallback _onNotify = onNotify;

	int IMFMediaEngineNotify.EventNotify(uint eventType, nuint param1, uint param2)
	{
		try
		{
			_onNotify((MFMediaEngineEvent)eventType, param1, param2);
			return CommonHResults.SOK;
		}
		catch (Exception ex)
		{
			return ex.HResult;
		}
	}

	public static MFMediaEngineNotify CreateDebugWriter()
		=> new((eventType, param1, param2) => Debug.WriteLine($"{DateTime.Now:HH:mm:ss}: {eventType},{param1},{param2}"));

	public static MFMediaEngineNotify CreateConsoleWriter()
		=> new((eventType, param1, param2) => Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {eventType},{param1},{param2}"));
}

/// <summary>
/// MF_MEDIA_ENGINE_EVENT
/// </summary>
public enum MFMediaEngineEvent : uint
{
	LoadStart = 1,
	Progress = 2,
	Suspend = 3,
	Abort = 4,
	Error = 5,
	Emptied = 6,
	Stalled = 7,
	Play = 8,
	Pause = 9,
	LoadedMetadata = 10,
	LoadedData = 11,
	Waiting = 12,
	Playing = 13,
	CanPlay = 14,
	CanPlayThrough = 15,
	Seeking = 16,
	Seeked = 17,
	TimeUpdate = 18,
	Ended = 19,
	RateChange = 20,
	DurationChange = 21,
	VolumeChange = 22,
	FormatChange = 1000,
	PurgeQueuedEvents = 1001,
	TimeLineMarker = 1002,
	BalanceChange = 1003,
	DownloadComplete = 1004,
	BufferingStarted = 1005,
	BuggeringEnded = 1006,
	FrameStepCompleted = 1007,
	NotigyStableState = 1008,
	FirstFrameReady = 1009,
	TracksChange = 1010,
	OpmInfo = 1011,
	ResourceLost = 1012,
	DelayLoadEventChanged = 1013,
	StreamRenderingError = 1014,
	SupportedRatesChanged = 1015,
	AudioEndpointChange = 1016,
}
