#pragma warning disable CA1707 // 識別子はアンダースコアを含むことはできません

using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <param name="o">RCWオブジェクト。</param>
public class MFTransform(object? o) : ComUnknownWrapperBase<IMFTransform>(o)
{
	// TODO タプルの分解版
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(uint InputMin, uint InputMax, uint OutputMin, uint OutputMax)> StreamLimitsNoThrow
		=> new(_obj.GetStreamLimits(out var x1, out var x2, out var x3, out var x4), (x1, x2, x3, x4));

	public (uint InputMin, uint InputMax, uint OutputMin, uint OutputMax) StreamLimits
		=> StreamLimitsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(uint Input, uint Output)> StreamCountsNoThrow
		=> new(_obj.GetStreamCount(out var x1, out var x2), (x1, x2));

	public (uint Input, uint Output) StreamCounts
		=> StreamCountsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> InputStreamCountNoThrow
		=> new(_obj.GetStreamCount(out var x, out Unsafe.NullRef<uint>()), x);

	public uint InputStreamCount
		=> InputStreamCountNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> OutputStreamCountNoThrow
		=> new(_obj.GetStreamCount(out var x, out Unsafe.NullRef<uint>()), x);

	public uint OutputStreamCount
		=> OutputStreamCountNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(uint[] Input, uint[] Output)> StreamIDsNoThrow
	{
		get
		{
			var (inputCount, outputCount) = StreamCounts;
			var inputIds = GC.AllocateUninitializedArray<uint>(checked((int)inputCount));
			var outputIds = GC.AllocateUninitializedArray<uint>(checked((int)outputCount));
			return new(_obj.GetStreamIDs(inputCount, inputIds, outputCount, outputIds), (inputIds, outputIds));
		}
	}

	public (uint[] Input, uint[] Output) StreamIDs
		=> StreamIDsNoThrow.Value;

	public uint[] InputStreamIDs
		=> StreamIDs.Input;

	public uint[] OutputStreamIDs
		=> StreamIDs.Output;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint[]> InputStreamIDsNoThrow
		=> (StreamIDsNoThrow is { } cr && cr.Succeeded) ? new(cr.HResult, cr.ValueUnchecked.Input) : new(cr.HResult, []);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint[]> OutputStreamIDsNoThrow
		=> (StreamIDsNoThrow is { } cr && cr.Succeeded) ? new(cr.HResult, cr.ValueUnchecked.Output) : new(cr.HResult, []);

	public ComResult<MFTInputStreamInfo> GetInputStreamInfoNoThrow(uint id)
	{
		var x = new MFTInputStreamInfo();
		return new(_obj.GetInputStreamInfo(id, x), x);
	}

	public MFTInputStreamInfo GetInputStreamInfo(uint id)
		=> GetInputStreamInfoNoThrow(id).Value;

	public IEnumerable<(uint ID, MFTInputStreamInfo Info)> InputStreamInfoEnumerable
	{
		get
		{
			foreach (var id in InputStreamIDs)
			{
				yield return (id, GetInputStreamInfo(id));
			}
		}
	}
	public ImmutableArray<(uint ID, MFTInputStreamInfo Info)> InputStreamInfos
		=> [.. InputStreamInfoEnumerable];

	public ComResult<MFTOutputStreamInfo> GetOutputStreamInfoNoThrow(uint id)
	{
		var x = new MFTOutputStreamInfo();
		return new(_obj.GetOutputStreamInfo(id, x), x);
	}

	public MFTOutputStreamInfo GetOutputStreamInfo(uint id)
		=> GetOutputStreamInfoNoThrow(id).Value;

	public IEnumerable<(uint ID, MFTOutputStreamInfo Info)> OutputStreamInfoEnumerable
	{
		get
		{
			foreach (var id in OutputStreamIDs)
			{
				yield return (id, GetOutputStreamInfo(id));
			}
		}
	}
	public ImmutableArray<(uint ID, MFTOutputStreamInfo Info)> OutputStreamInfos
		=> [.. OutputStreamInfoEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFAttributes> AttributesNoThrow
		=> new(_obj.GetAttributes(out var x), new(x));

	public MFAttributes Attributes
		=> AttributesNoThrow.Value;

	public ComResult<MFAttributes> GetInputStreamAttributesNoThrow(uint streamId)
		=> new(_obj.GetInputStreamAttributes(streamId, out var x), new(x));

	public MFAttributes GetInputStreamAttributes(uint streamId)
		=> GetInputStreamAttributesNoThrow(streamId).Value;

	public IEnumerable<(uint ID, MFAttributes Attributes)> InputStreamAttributeEnumerable
	{
		get
		{
			foreach (var id in InputStreamIDs)
			{
				yield return (id, GetInputStreamAttributes(id));
			}
		}
	}
	public ImmutableArray<(uint ID, MFAttributes Attributes)> InputStreamAttributesArray
		=> [.. InputStreamAttributeEnumerable];

	public ComResult<MFAttributes> GetOutputStreamAttributesNoThrow(uint streamId)
		=> new(_obj.GetOutputStreamAttributes(streamId, out var x), new(x));

	public MFAttributes GetOutputStreamAttributes(uint streamId)
		=> GetOutputStreamAttributesNoThrow(streamId).Value;

	public IEnumerable<(uint ID, MFAttributes Attributes)> OutputStreamAttributeEnumerable
	{
		get
		{
			foreach (var id in OutputStreamIDs)
			{
				yield return (id, GetOutputStreamAttributes(id));
			}
		}
	}
	public ImmutableArray<(uint ID, MFAttributes Attributes)> OutputStreamAttributesArray
		=> [.. OutputStreamAttributeEnumerable];

	public ComResult DeleteInputStreamNoThrow(uint streamId)
		=> new(_obj.DeleteInputStream(streamId));

	public void DeleteInputStream(uint streamId)
		=> DeleteInputStreamNoThrow(streamId).ThrowIfError();

	public ComResult AddInputStreamsNoThrow(ReadOnlySpan<uint> ids)
		=> new(_obj.AddInputStreams(checked((uint)ids.Length), in MemoryMarshal.GetReference(ids)));

	public void AddInputStreams(ReadOnlySpan<uint> ids)
		=> AddInputStreamsNoThrow(ids).ThrowIfError();

	public ComResult<MFMediaType> GetInputAvailableTypeNoThrow(uint streamId, uint typeIndex)
		=> new(_obj.GetInputAvailableType(streamId, typeIndex, out var x), new(x));

	public MFMediaType GetInputAvailableType(uint streamId, uint typeIndex)
		=> GetInputAvailableTypeNoThrow(streamId, typeIndex).Value;

	// TODO Enumerate

	public ComResult<MFMediaType> GetOutputAvailableTypeNoThrow(uint streamId, uint typeIndex)
		=> new(_obj.GetOutputAvailableType(streamId, typeIndex, out var x), new(x));

	public MFMediaType GetOutputAvailableType(uint streamId, uint typeIndex)
		=> GetOutputAvailableTypeNoThrow(streamId, typeIndex).Value;

	// TODO Enumerate

	public ComResult SetInputTypeNoThrow(uint streamId, MFMediaType mediaType, MFTSetTypeFlag flags)
		=> new(_obj.SetInputType(streamId, (IMFMediaType)mediaType.WrappedObject!, (uint)flags));

	public void SetInputType(uint streamId, MFMediaType mediaType, MFTSetTypeFlag flags)
		=> SetInputTypeNoThrow(streamId, mediaType, flags).ThrowIfError();

	public ComResult SetOutputTypeNoThrow(uint streamId, MFMediaType mediaType, MFTSetTypeFlag flags)
		=> new(_obj.SetOutputType(streamId, (IMFMediaType)mediaType.WrappedObject!, (uint)flags));

	public void SetOutputType(uint streamId, MFMediaType mediaType, MFTSetTypeFlag flags)
		=> SetOutputTypeNoThrow(streamId, mediaType, flags).ThrowIfError();

	public ComResult<MFMediaType> GetInputCurrentTypeNoThrow(uint streamId)
		=> new(_obj.GetInputCurrentType(streamId, out var x), new(x));

	public MFMediaType GetInputCurrentType(uint streamId)
		=> GetInputCurrentTypeNoThrow(streamId).Value;

	// TODO Enumerate

	public ComResult<MFMediaType> GetOutputCurrentTypeNoThrow(uint streamId)
		=> new(_obj.GetOutputCurrentType(streamId, out var x), new(x));

	public MFMediaType GetOutputCurrentType(uint streamId)
		=> GetOutputCurrentTypeNoThrow(streamId).Value;

	// TODO Enumerate

	public ComResult<MFTInputStatusFlag> GetInputStatusNoThrow(uint streamId)
		=> new(_obj.GetInputStatus(streamId, out var x), (MFTInputStatusFlag)x);

	public MFTInputStatusFlag GetInputStatus(uint streamId)
		=> GetInputStatusNoThrow(streamId).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFTInputStatusFlag> OutputStatusNoThrow
		=> new(_obj.GetOutputStatus(out var x), (MFTInputStatusFlag)x);

	public MFTInputStatusFlag OutputStatus
		=> OutputStatusNoThrow.Value;

	public ComResult SetOutputBoundsNoThrow(uint LowerBound, uint UpperBound)
		=> new(_obj.SetOutputBounds(LowerBound, UpperBound));

	public void SetOutputBounds(uint LowerBound, uint UpperBound)
		=> SetOutputBoundsNoThrow(LowerBound, UpperBound).ThrowIfError();

	// TODO
	//[PreserveSig]
	//int ProcessEvent(
	//	uint dwInputStreamID,//	IMFMediaEvent pEvent);

	public ComResult ProcessMessageNoThrow(MFTMessageType message, nuint param)
		=> new(_obj.ProcessMessage(message, param));

	public void ProcessMessage(MFTMessageType message, nuint param)
		=> ProcessMessageNoThrow(message, param).ThrowIfError();

	public ComResult ProcessInputNoThrow(uint streamId, MFSample sample)
		=> new(_obj.ProcessInput(streamId, (IMFSample)sample.WrappedObject!, 0));

	public void ProcessInput(uint streamId, MFSample sample)
		=> ProcessInputNoThrow(streamId, sample).ThrowIfError();

	public ComResult<MFTProcessOutputStatus> ProcessOutputNoThrow(uint streamId, MFTOutputDataBuffer[] outputSamples)
		=> new(_obj.ProcessOutput(streamId, checked((uint)outputSamples.Length), outputSamples, out var x), (MFTProcessOutputStatus)x);

	public MFTProcessOutputStatus ProcessOutput(uint streamId, MFTOutputDataBuffer[] outputSamples)
		=> ProcessOutputNoThrow(streamId, outputSamples).Value;
}

/// <summary>
/// MFT_INPUT_DATA_BUFFER_FLAGS
/// </summary>
[Flags]
public enum MFTInputDataBufferFlag : uint
{
	Placeholder = 0xffffffff
}

/// <summary>
/// MFT_OUTPUT_DATA_BUFFER_FLAGS
/// </summary>
[Flags]
public enum MFTOutputDataBufferFlag : uint
{
	Incomplete = 0x1000000,
	FormatChange = 0x100,
	StreamEnd = 0x200,
	NoSample = 0x300
}

/// <summary>
/// MFT_INPUT_STATUS_FLAGS
/// </summary>
[Flags]
public enum MFTInputStatusFlag : uint
{
	AcceptData = 0x1
}

/// <summary>
/// MFT_OUTPUT_STATUS_FLAGS
/// </summary>
[Flags]
public enum MFTOutputStatusFlag : uint
{
	SampleReady = 0x1
}

/// <summary>
/// MFT_INPUT_STREAM_INFO_FLAGS
/// </summary>
[Flags]
public enum MFTInputStreamInfoFlag : uint
{
	WholeSamples = 0x1,
	SingleSamplePerBuffer = 0x2,
	FixedSampleSize = 0x4,
	HoldsBuffers = 0x8,
	DoesNotAddRef = 0x100,
	Removable = 0x200,
	Optional = 0x400,
	ProcessesInPlace = 0x800
}

/// <summary>
/// MFT_OUTPUT_STREAM_INFO_FLAGS
/// </summary>
[Flags]
public enum MFTOutputStreamInfoFlag : uint
{
	WholeSamples = 0x1,
	SingleSamplePerBuffer = 0x2,
	FixedSampleSize = 0x4,
	Discardable = 0x8,
	Optional = 0x10,
	ProvidersSamples = 0x100,
	CanProvideSamples = 0x200,
	LazyRead = 0x400,
	Removable = 0x800
}

/// <summary>
/// MFT_SET_TYPE_FLAGS
/// </summary>
[Flags]
public enum MFTSetTypeFlag : uint
{
	TestOnly = 0x1
}

/// <summary>
/// MFT_PROCESS_OUTPUT_FLAGS
/// </summary>
[Flags]
public enum MFTProcessOutputFlag : uint
{
	DiscardWhenNoBuffer = 0x1,
	RegenerateLastOutput = 0x2
}

/// <summary>
/// MFT_PROCESS_OUTPUT_STATUS
/// </summary>
[Flags]
public enum MFTProcessOutputStatus : uint
{
	NewStreams = 0x100
}

/// <summary>
/// MFT_DRAIN_TYPE
/// </summary>
[Flags]
public enum MFTDrainType : uint
{
	ProduceTails = 0,
	NoTails = 0x1
}

// TODO
// #define MFT_STREAMS_UNLIMITED       0xFFFFFFFF
// #define MFT_OUTPUT_BOUND_LOWER_UNBOUNDED MINLONGLONG
// #define MFT_OUTPUT_BOUND_UPPER_UNBOUNDED MAXLONGLONG

/// <summary>
/// MFT_MESSAGE_TYPE
/// </summary>
public enum MFTMessageType : uint
{
	CommandFlush = 0,
	CommandDrain = 0x1,
	SetD3DManager = 0x2,
	DropSamples = 0x3,
	CommandTick = 0x4,
	NotifyBeginStreaming = 0x10000000,
	NotifyEndStreaming = 0x10000001,
	NotifyEndOfStream = 0x10000002,
	NotifyStartOfStream = 0x10000003,
	NotifyReleaseResources = 0x10000004,
	NotifyReacquireResources = 0x10000005,
	NotifyEvent = 0x10000006,
	SetOutputStreamState = 0x10000007,
	FlushOutputStream = 0x10000008,
	Marker = 0x20000000,
}

/// <summary>
/// MFT_INPUT_STREAM_INFO
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public record class MFTInputStreamInfo
{
	public long hnsMaxLatency;
	public uint dwFlags;
	public uint cbSize;
	public uint cbMaxLookahead;
	public uint cbAlignment;
}

/// <summary>
/// MFT_OUTPUT_STREAM_INFO
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public record class MFTOutputStreamInfo
{
	public uint dwFlags;
	public uint cbSize;
	public uint cbAlignment;
}

/// <summary>
/// MFT_OUTPUT_DATA_BUFFER
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public record class MFTOutputDataBuffer
{
	public uint StreamID;
	public IMFSample? Sample;
	public uint Status;
	public IMFCollection? Events;
}
