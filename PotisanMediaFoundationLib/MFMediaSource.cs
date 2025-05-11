using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <summary>
/// MFメディアソース。IMFMediaSource COMインターフェイスのラッパーです。
/// </summary>
public class MFMediaSource(object? o) : ComUnknownWrapperBase<IMFMediaSource>(o)
{
	public bool ShutdownsOnDelete { get; set; } = true;

	~MFMediaSource()
	{
		if (ShutdownsOnDelete)
			_obj?.Shutdown();
	}

#pragma warning disable CA1816 // Dispose メソッドは、SuppressFinalize を呼び出す必要があります
	/// <inheritdoc/>
	public override void Dispose()
	{
		if (ShutdownsOnDelete)
			_obj?.Shutdown();
		base.Dispose();
	}
#pragma warning restore CA1816

	// TODO
	#region IMFMediaEventGenerator

	//[PreserveSig]
	//int GetEvent(
	//	uint dwFlags,
	//	out IMFMediaEvent? ppEvent);

	//[PreserveSig]
	//int BeginGetEvent(
	//	IMFAsyncCallback pCallback,
	//	[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	//[PreserveSig]
	//int EndGetEvent(
	//	IMFAsyncResult pResult,
	//	out IMFMediaEvent? ppEvent);

	//[PreserveSig]
	//int QueueEvent(
	//	MediaEventType met,
	//	in Guid guidExtendedType,
	//	int hrStatus,
	//	PropVariant pvValue);

	#endregion

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFMediaSourceCharacteristic> CharacteristicsNoThrow

		=> new(_obj.GetCharacteristics(out var x), (MFMediaSourceCharacteristic)x);
	public MFMediaSourceCharacteristic Characteristics => CharacteristicsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFPresentationDescriptor> MFPresentationDescriptorNoThrow

		=> new(_obj.CreatePresentationDescriptor(out var x), new(x));
	public MFPresentationDescriptor MFPresentationDescriptor => MFPresentationDescriptorNoThrow.Value;

	public ComResult Start(MFPresentationDescriptor descriptor, Guid? timeFormat, PropVariant startPosition)
		=> new(_obj.Start((IMFPresentationDescriptor)descriptor.WrappedObject!, timeFormat ?? Unsafe.NullRef<Guid>(), startPosition));

	public void StartNoThrow(MFPresentationDescriptor descriptor, Guid? timeFormat, PropVariant startPosition)
		=> Start(descriptor, timeFormat, startPosition).ThrowIfError();

	public ComResult Start(MFPresentationDescriptor descriptor, long startPosition100NS)
		=> new(_obj.Start((IMFPresentationDescriptor)descriptor.WrappedObject!, in Unsafe.NullRef<Guid>(), PropVariant.InitInt64(startPosition100NS)));

	public void StartNoThrow(MFPresentationDescriptor descriptor, long startPosition100NS)
		=> Start(descriptor, startPosition100NS).ThrowIfError();

	public ComResult StopNoThrow()
		=> new(_obj.Stop());

	public void Stop()
		=> StopNoThrow().ThrowIfError();

	public ComResult PauseNoThrow()
		=> new(_obj.Pause());

	public void Pause()
		=> PauseNoThrow().ThrowIfError();

	public ComResult ShutdownNoThrow()
		=> new(_obj.Shutdown());

	public void Shutdown()
		=> ShutdownNoThrow().ThrowIfError();

	public static ComResult<MFMediaSource> CreateDeviceSourceNoThrow(MFAttributes attrs)
	{
		[DllImport("mf.dll")]
		static extern int MFCreateDeviceSource(IMFAttributes pAttributes, out IMFMediaSource ppSource);

		return new(MFCreateDeviceSource((IMFAttributes)attrs.WrappedObject!, out var x), new(x));
	}

	public static MFMediaSource CreateDeviceSource(MFAttributes attrs)
		=> CreateDeviceSourceNoThrow(attrs).Value;

	public static ComResult<MFMediaSource> CreateAudioCaptureSourceNoThrow(string symbolicLink, MFAttributes? attrs = null)
	{
		try
		{
			attrs = attrs != null ? attrs.Clone() : MFAttributes.Create(2);
			var srcDevAttrs = new MFDeviceSourceAttributes(attrs)
			{
				SourceType = MFSourceTypeGuids.AudioCapture,
				AudioCaptureSymbolicLink = symbolicLink
			};
			return CreateDeviceSourceNoThrow(attrs);
		}
		catch (Exception ex)
		{
			return new(ex.HResult, new(null));
		}
	}

	public static MFMediaSource CreateAudioCaptureSource(string symbolicLink, MFAttributes? attrs = null)
		=> CreateAudioCaptureSourceNoThrow(symbolicLink, attrs).Value;

	public static ComResult<MFMediaSource> CreateAudioCaptureSourceNoThrow(MFRole role, MFAttributes? attrs = null)
	{
		try
		{
			attrs = attrs != null ? attrs.Clone() : MFAttributes.Create(2);
			var srcDevAttrs = new MFDeviceSourceAttributes(attrs)
			{
				SourceType = MFSourceTypeGuids.AudioCapture,
				SourceTypeAudioCaptureRole = role
			};
			return CreateDeviceSourceNoThrow(attrs);
		}
		catch (Exception ex)
		{
			return new(ex.HResult, new(null));
		}
	}

	public static MFMediaSource CreateAudioCaptureSource(MFRole role, MFAttributes? attrs = null)
		=> CreateAudioCaptureSourceNoThrow(role, attrs).Value;

	public static ComResult<MFMediaSource> CreateVideoCaptureSourceNoThrow(string symbolicLink, MFAttributes? attrs = null)
	{
		try
		{
			attrs = attrs != null ? attrs.Clone() : MFAttributes.Create(2);
			var srcDevAttrs = attrs.ForDeviceSource;
			srcDevAttrs.SourceType = MFSourceTypeGuids.VideoCapture;
			srcDevAttrs.VideoCaptureSymbolicLink = symbolicLink;
			return CreateDeviceSourceNoThrow(attrs);
		}
		catch (Exception ex)
		{
			return new(ex.HResult, new(null));
		}
	}
	public static MFMediaSource CreateVideoCaptureSource(string symbolicLink, MFAttributes? attrs = null)
		=> CreateVideoCaptureSourceNoThrow(symbolicLink, attrs).Value;
}

/// <summary>
/// MFMEDIASOURCE_CHARACTERISTICS
/// </summary>
[Flags]
public enum MFMediaSourceCharacteristic : uint
{
	IsLive = 0x1,
	CanSeek = 0x2,
	CanPause = 0x4,
	HasSlowSeek = 0x8,
	HasMultiplePresentations = 0x10,
	CanSkipForward = 0x20,
	CanSkipBackward = 0x40,
	DoesNotUseNetwork = 0x80,
}
