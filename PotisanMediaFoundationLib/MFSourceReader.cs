using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <param name="o">RCWインスタンス。</param>
public class MFSourceReader(object? o) : ComUnknownWrapperBase<IMFSourceReader>(o)
{
	#region Functions for specific index

	public ComResult<bool> GetStreamSelectionNoThrow(uint index)
		=> new(_obj.GetStreamSelection(index, out var x), x);

	public bool GetStreamSelection(uint index)
		=> GetStreamSelectionNoThrow(index).Value;

	public ComResult SetStreamSelectionNoThrow(uint index, bool value)
		=> new(_obj.SetStreamSelection(index, value));

	public void SetStreamSelection(uint index, bool value)
		=> SetStreamSelectionNoThrow(index, value).ThrowIfError();

	public ComResult<bool> GetStreamSelectionNoThrow(MFSourceReaderIndex index)
		=> GetStreamSelectionNoThrow((uint)index);

	public bool GetStreamSelection(MFSourceReaderIndex index)
		=> GetStreamSelection((uint)index);

	public ComResult SetStreamSelectionNoThrow(MFSourceReaderIndex index, bool value)
		=> SetStreamSelectionNoThrow((uint)index, value);

	public void SetStreamSelection(MFSourceReaderIndex index, bool value)
		=> SetStreamSelection((uint)index, value);

	public ComResult<MFMediaType> GetNativeMediaTypeNoThrow(uint streamIndex, uint mediaTypeIndex)
		=> new(_obj.GetNativeMediaType(streamIndex, mediaTypeIndex, out var x), new(x));

	public MFMediaType GetNativeMediaType(uint streamIndex, uint mediaTypeIndex)
		=> GetNativeMediaTypeNoThrow(streamIndex, mediaTypeIndex).Value;

	public ComResult<MFMediaType> GetNativeMediaTypeNoThrow(MFSourceReaderIndex streamIndex, uint mediaTypeIndex)
		=> GetNativeMediaTypeNoThrow((uint)streamIndex, mediaTypeIndex);

	public MFMediaType GetNativeMediaType(MFSourceReaderIndex streamIndex, uint mediaTypeIndex)
		=> GetNativeMediaType((uint)streamIndex, mediaTypeIndex);

	public ImmutableArray<MFMediaType> GetNativeMediaTypes(uint streamIndex)
	{
		const int MF_E_NO_MORE_TYPES = unchecked((int)0xC00D36B9);
		var mediaTypes = new List<MFMediaType>();
		for (uint i = 0; i < uint.MaxValue; i++)
		{
			var cr = GetNativeMediaTypeNoThrow(streamIndex, i);
			if (!cr)
			{
				if (cr.HResult == MF_E_NO_MORE_TYPES) break;
				cr.Throw();
			}
			mediaTypes.Add(cr.ValueUnchecked);
		}
		return [.. mediaTypes];
	}

	public ImmutableArray<MFMediaType> GetNativeMediaTypes(MFSourceReaderIndex streamIndex)
		=> GetNativeMediaTypes((uint)streamIndex);

	public ComResult<MFMediaType> GetCurrentMediaTypeNoThrow(uint streamIndex)
		=> new(_obj.GetCurrentMediaType(streamIndex, out var x), new(x));

	public MFMediaType GetCurrentMediaType(uint streamIndex)
		=> GetCurrentMediaTypeNoThrow(streamIndex).Value;

	public ComResult SetCurrentMediaTypeNoThrow(uint streamIndex, MFMediaType mediaType)
		=> new(_obj.SetCurrentMediaType(streamIndex, 0, (IMFMediaType)mediaType.WrappedObject!));

	public void SetCurrentMediaType(uint streamIndex, MFMediaType value)
		=> SetCurrentMediaTypeNoThrow(streamIndex, value).ThrowIfError();

	public ComResult<MFMediaType> GetCurrentMediaTypeNoThrow(MFSourceReaderIndex streamIndex)
		=> new(_obj.GetCurrentMediaType((uint)streamIndex, out var x), new(x));

	public MFMediaType GetCurrentMediaType(MFSourceReaderIndex streamIndex)
		=> GetCurrentMediaTypeNoThrow((uint)streamIndex).Value;

	public ComResult SetCurrentMediaTypeNoThrow(MFSourceReaderIndex streamIndex, MFMediaType mediaType)
		=> new(_obj.SetCurrentMediaType((uint)streamIndex, 0, (IMFMediaType)mediaType.WrappedObject!));

	public void SetCurrentMediaType(MFSourceReaderIndex streamIndex, MFMediaType value)
		=> SetCurrentMediaTypeNoThrow((uint)streamIndex, value).ThrowIfError();

	public ComResult SetCurrentPositionNoThrow(Guid? timeFormat, PropVariant value)
		=> new(_obj.SetCurrentPosition(timeFormat ?? Guid.Empty, value));

	public void SetCurrentPosition(Guid? timeFormat, PropVariant value)
		=> SetCurrentPositionNoThrow(timeFormat, value).ThrowIfError();

	public ComResult SetCurrentPositionNoThrow(long value100Nanoseconds)
		=> SetCurrentPositionNoThrow(Guid.Empty, PropVariant.InitInt64(value100Nanoseconds));

	public void SetCurrentPosition(long value100Nanoseconds)
		=> SetCurrentPositionNoThrow(value100Nanoseconds).ThrowIfError();

	public sealed record class ReadSampleResult(uint ActualStreamIndex, MFSourceReaderFlag StreamFlags, long TimeStamp, MFSample Sample);

	public ComResult<ReadSampleResult> ReadSampleNoThrow(uint streamIndex, MFSourceReaderControlFlag controlFlags = 0)
		=> new(_obj.ReadSample(
			streamIndex, (uint)controlFlags, out var x1, out var x2, out var x3, out var x4), new(x1, (MFSourceReaderFlag)x2, x3, new(x4)));

	public ReadSampleResult ReadSample(uint streamIndex, MFSourceReaderControlFlag controlFlags = 0)
		=> ReadSampleNoThrow(streamIndex, controlFlags).Value;

	public ComResult<ReadSampleResult> ReadSampleNoThrow(MFSourceReaderIndex streamIndex, MFSourceReaderControlFlag controlFlags = 0)
		=> ReadSampleNoThrow((uint)streamIndex, controlFlags);

	public ReadSampleResult ReadSample(MFSourceReaderIndex streamIndex, MFSourceReaderControlFlag controlFlags = 0)
		=> ReadSampleNoThrow((uint)streamIndex, controlFlags).Value;

	public ComResult FlushNoThrow(uint streamIndex)
		=> new(_obj.Flush(streamIndex));

	public void Flush(uint streamIndex)
		=> FlushNoThrow(streamIndex).ThrowIfError();

	public ComResult FlushNoThrow(MFSourceReaderIndex streamIndex)
		=> FlushNoThrow((uint)streamIndex);

	public void Flush(MFSourceReaderIndex streamIndex)
		=> Flush((uint)streamIndex);

	public ComResult<TWrapper> GetServiceForStreamNoThrow<TWrapper, TInterface>(uint streamIndex, in Guid serviceGuid)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetServiceForStream(streamIndex, serviceGuid, typeof(TInterface).GUID, out var x), x);

	public TWrapper GetServiceForStream<TWrapper, TInterface>(uint streamIndex, in Guid serviceGuid)
		where TWrapper : IComUnknownWrapper
		=> GetServiceForStreamNoThrow<TWrapper, TInterface>(streamIndex, serviceGuid).Value;

	public ComResult<TWrapper> GetServiceForStreamNoThrow<TWrapper, TInterface>(MFSourceReaderIndex streamIndex, in Guid serviceGuid)
		where TWrapper : IComUnknownWrapper
		=> GetServiceForStreamNoThrow<TWrapper, TInterface>((uint)streamIndex, serviceGuid);

	public TWrapper GetServiceForStream<TWrapper, TInterface>(MFSourceReaderIndex streamIndex, in Guid serviceGuid)
		where TWrapper : IComUnknownWrapper
		=> GetServiceForStream<TWrapper, TInterface>((uint)streamIndex, serviceGuid);

	public ComResult<PropVariant> GetPresentationAttributeNoThrow(uint streamIndex, in Guid attributeGuid)
	{
		var pv = new PropVariant();
		return new(_obj.GetPresentationAttribute(streamIndex, attributeGuid, pv), pv);
	}

	public void GetPresentationAttribute(uint streamIndex, in Guid attributeGuid)
		=> GetPresentationAttributeNoThrow(streamIndex, attributeGuid).ThrowIfError();

	public ComResult<PropVariant> GetPresentationAttributeNoThrow(MFSourceReaderIndex streamIndex, in Guid attributeGuid)
		=> GetPresentationAttributeNoThrow((uint)streamIndex, attributeGuid);

	public void GetPresentationAttribute(MFSourceReaderIndex streamIndex, in Guid attributeGuid)
		=> GetPresentationAttribute((uint)streamIndex, attributeGuid);

	#endregion

	#region Attributes for FirstVideo

	public ImmutableArray<MFMediaType> FirstVideoNativeMediaTypes
		=> GetNativeMediaTypes(MFSourceReaderIndex.FirstVideoStream);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> FirstVideoStreamSelectionNoThrow
		=> GetStreamSelectionNoThrow(MFSourceReaderIndex.FirstVideoStream);

	public ComResult SetFirstVideoStreamSelectionNoThrow(bool value)
		=> SetStreamSelectionNoThrow(MFSourceReaderIndex.FirstVideoStream, value);

	public bool FirstVideoStreamSelection
	{
		get => GetStreamSelection(MFSourceReaderIndex.FirstVideoStream);
		set => SetStreamSelection(MFSourceReaderIndex.FirstVideoStream, value);
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFMediaType> FirstVideoStreamCurrentMediaTypeNoThrow
		=> GetCurrentMediaTypeNoThrow(MFSourceReaderIndex.FirstVideoStream);

	public ComResult SetFirstVideoStreamCurrentMediaTypeNoThrow(MFMediaType mediaType)
		=> SetCurrentMediaTypeNoThrow(MFSourceReaderIndex.FirstVideoStream, mediaType);

	public MFMediaType FirstVideoStreamCurrentMediaType
	{
		get => GetCurrentMediaType(MFSourceReaderIndex.FirstVideoStream);
		set => SetCurrentMediaType(MFSourceReaderIndex.FirstVideoStream, value);
	}

	public MFSourceReaderPresentationAttributes FirstVideoStreamPresentationAttributes
		=> new(this, (uint)MFSourceReaderIndex.FirstVideoStream);

	#endregion

	#region プレゼンテーション記述子属性

	//public static Guid MF_PD_PMPHOST_CONTEXT => new(0x6c990d31, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
	//public static Guid MF_PD_APP_CONTEXT => new(0x6c990d32, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ulong> DurationNoThrow
	{
		get
		{
			var cr = GetPresentationAttributeNoThrow(MFSourceReaderIndex.MediaSource, MFPresentationDescriptorAttributeGuids.MF_PD_DURATION);
			return new(cr.HResult, cr.ValueUnchecked.GetUInt64WithDefault(0));
		}
	}

	public ulong Duration
		=> DurationNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ulong> TotalFileSizeNoThrow
	{
		get
		{
			var cr = GetPresentationAttributeNoThrow(MFSourceReaderIndex.MediaSource, MFPresentationDescriptorAttributeGuids.MF_PD_TOTAL_FILE_SIZE);
			return new(cr.HResult, cr.ValueUnchecked.GetUInt64WithDefault(0));
		}
	}

	public ulong TotalFileSize
		=> TotalFileSizeNoThrow.Value;

	//public static Guid MF_PD_AUDIO_ENCODING_BITRATE => new(0x6c990d35, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
	//public static Guid MF_PD_VIDEO_ENCODING_BITRATE => new(0x6c990d36, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> MimeTypeNoThrow
	{
		get
		{
			var cr = GetPresentationAttributeNoThrow(MFSourceReaderIndex.MediaSource, MFPresentationDescriptorAttributeGuids.MF_PD_MIME_TYPE);
			return new(cr.HResult, cr.ValueUnchecked.GetStringUniWithDefault(""));
		}
	}

	public string MimeType
		=> MimeTypeNoThrow.Value;

	//public static Guid PD_LAST_MODIFIED_TIME => new(0x6c990d38, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
	//public static Guid PD_PLAYBACK_ELEMENT_ID => new(0x6c990d39, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
	//public static Guid MF_PD_PREFERRED_LANGUAGE => new(0x6c990d3A, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
	//public static Guid PD_PLAYBACK_BOUNDARY_TIME => new(0x6c990d3b, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
	//public static Guid MF_PD_AUDIO_ISVARIABLEBITRATE => new(0x33026ee0, 0xe387, 0x4582, 0xae, 0x0a, 0x34, 0xa2, 0xad, 0x3b, 0xaa, 0x18);
	//public static Guid MF_PD_ADAPTIVE_STREAMING => new(0xEA0D5D97, 0x29F9, 0x488B, 0xAE, 0x6B, 0x7D, 0x6B, 0x41, 0x36, 0x11, 0x2B);
	#endregion

	public static ComResult<MFSourceReader> CreateNoThrow()
	{
		Guid CLSID_MFSourceReader = new(0x1777133c, 0x0881, 0x411b, 0xa5, 0x77, 0xad, 0x54, 0x5f, 0x07, 0x14, 0xc4);
		return ComHelper.CreateInstanceNoThrow<MFSourceReader, IMFSourceReader>(CLSID_MFSourceReader);
	}

	public static MFSourceReader Create()
		=> CreateNoThrow().Value;

	public static ComResult<MFSourceReader> CreateFromMediaSourceNoThrow(MFMediaSource source, MFAttributes? attrs = null)
	{
		[DllImport("mfreadwrite.dll")]
		static extern int MFCreateSourceReaderFromMediaSource(IMFMediaSource pMediaSource, IMFAttributes? pAttributes, out IMFSourceReader ppSourceReader);

		return new(MFCreateSourceReaderFromMediaSource(
			(IMFMediaSource)source.WrappedObject!, attrs?.WrappedObject as IMFAttributes, out var x), new(x));
	}

	public static MFSourceReader CreateFromMediaSource(MFMediaSource source, MFAttributes? attrs = null)
		=> CreateFromMediaSourceNoThrow(source, attrs).Value;

	public static MFSourceReader CreateFromMediaSourceWithAdvancedVideoProcessing(MFMediaSource source, MFAttributes? attrs = null)
	{
		attrs = attrs != null ? attrs.Clone() : MFAttributes.Create(1);
		attrs.ForSourceReader.EnableAdvancedVideoProcessing = true;
		return CreateFromMediaSource(source, attrs);
	}

	public static ComResult<MFSourceReader> CreateFromUrlNoThrow(string url, MFAttributes? attrs = null)
	{
		[DllImport("mfreadwrite.dll", CharSet = CharSet.Unicode)]
		static extern int MFCreateSourceReaderFromURL(string pwszURL, IMFAttributes? pAttributes, out IMFSourceReader ppSourceReader);

		return new(MFCreateSourceReaderFromURL(url, attrs?.WrappedObject as IMFAttributes, out var x), new(x));
	}

	public static MFSourceReader CreateFromUrl(string url, MFAttributes? attrs = null)
		=> CreateFromUrlNoThrow(url, attrs).Value;

	public static ComResult<MFSourceReader> CreateFromByteStreamNoThrow(MFByteStream stream, MFAttributes? attrs = null)
	{
		[DllImport("mfreadwrite.dll", CharSet = CharSet.Unicode)]
		static extern int MFCreateSourceReaderFromByteStream(IMFByteStream pByteStream, IMFAttributes? pAttributes, out IMFSourceReader ppSourceReader);

		return new(MFCreateSourceReaderFromByteStream((IMFByteStream)stream.WrappedObject!, attrs?.WrappedObject as IMFAttributes, out var x), new(x));
	}

	public static MFSourceReader CreateFromByteStream(MFByteStream stream, MFAttributes? attrs = null)
		=> CreateFromByteStreamNoThrow(stream, attrs).Value;

	public static MFSourceReader CreateFromByteStreamWithAdvancedVideoProcessing(MFByteStream stream, MFAttributes? attrs = null)
	{
		attrs = attrs != null ? attrs.Clone() : MFAttributes.Create(1);
		attrs.ForSourceReader.EnableAdvancedVideoProcessing = true;
		return CreateFromByteStream(stream, attrs);
	}
}

/// <summary>
/// MF_SOURCE_READER_CONTROL_FLAG
/// </summary>
[Flags]
public enum MFSourceReaderControlFlag : uint
{
	Drain = 0x1,
}

/// <summary>
/// MF_SOURCE_READER_FLAG
/// </summary>
[Flags]
public enum MFSourceReaderFlag : uint
{
	Error = 0x1,
	EndOfStream = 0x2,
	NewStream = 0x4,
	NativeMediaTypeChanged = 0x10,
	CurrentMediaTypeChanged = 0x20,
	StreamTick = 0x100,
	AllEffectsRemoved = 0x200,
}

public enum MFSourceReaderIndex : uint
{
	/// <summary>
	/// MF_SOURCE_READER_FIRST_VIDEO_STREAM
	/// </summary>
	FirstVideoStream = 0xFFFFFFFC,

	/// <summary>
	/// MF_SOURCE_READER_FIRST_AUDIO_STREAM
	/// </summary>
	FirstAudioStream = 0xFFFFFFFD,

	/// <summary>
	/// MF_SOURCE_READER_ANY_STREAMS
	/// </summary>
	AnyStreams = 0xfffffffe,

	/// <summary>
	/// MF_SOURCE_READER_ALL_STREAMS
	/// </summary>
	AllStreams = 0xfffffffe,

	/// <summary>
	/// MF_SOURCE_READER_INVALID_STREAM_INDEX
	/// </summary>
	InvalidStream = 0xffffffff,

	/// <summary>
	/// MF_SOURCE_READER_MEDIASOURCE
	/// </summary>
	MediaSource = 0xffffffff,
}

//	MF_SOURCE_READER_CURRENT_TYPE_INDEX = 0xffffffff
