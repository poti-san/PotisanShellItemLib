using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.MediaFoundation.Mux.ComTypes;

namespace Potisan.Windows.MediaFoundation.Mux;

public sealed class MFMuxStreamMediaTypeManager(object? o) : ComUnknownWrapperBase<IMFMuxStreamMediaTypeManager>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> StreamCountNoThrow
		=> new(_obj.GetStreamCount(out var x), x);

	public uint StreamCount
		=> StreamCountNoThrow.Value;

	public ComResult<MFMediaType> GetMediaTypeNoThrow(uint streamIndex)
		=> new(_obj.GetMediaType(streamIndex, out var x), new(x));

	public MFMediaType GetMediaType(uint streamIndex)
		=> GetMediaTypeNoThrow(streamIndex).Value;

	public IEnumerable<MFMediaType> MediaTypesEnumerable
	{
		get
		{
			var c = StreamCount;
			for (uint i = 0; i < c; i++)
				yield return GetMediaType(i);
		}
	}

	public ImmutableArray<MFMediaType> MediaTypes
		=> [.. MediaTypesEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> StreamConfigurationCountNoThrow
		=> new(_obj.GetStreamConfigurationCount(out var x), x);

	public uint StreamConfigurationCount
		=> StreamConfigurationCountNoThrow.Value;

	public ComResult AddStreamConfigurationNoThrow(uint streamMask)
		=> new(_obj.AddStreamConfiguration(streamMask));

	public void AddStreamConfiguration(uint streamMask)
		=> AddStreamConfigurationNoThrow(streamMask);

	public ComResult RemoveStreamConfigurationNoThrow(uint streamMask)
		=> new(_obj.RemoveStreamConfiguration(streamMask));

	public void RemoveStreamConfiguration(uint streamMask)
		=> RemoveStreamConfigurationNoThrow(streamMask);

	public ComResult<ulong> GetStreamConfigurationNoThrow(uint index)
		=> new(_obj.GetStreamConfiguration(index, out var x), x);

	public ulong GetStreamConfiguration(uint index)
		=> GetStreamConfigurationNoThrow(index).Value;

	public IEnumerable<ulong> StreamConfigurationEnumerable
	{
		get
		{
			var c = StreamConfigurationCount;
			for (uint i = 0; i < c; i++)
				yield return GetStreamConfiguration(c);
		}
	}

	public ImmutableArray<ulong> StreamConfigurations
		=> [.. StreamConfigurationEnumerable];
}
