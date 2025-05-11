using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.MediaFoundation.Mux.ComTypes;

namespace Potisan.Windows.MediaFoundation.Mux;

public class MFMuxStreamAttributesManager(object? o) : ComUnknownWrapperBase<IMFMuxStreamAttributesManager>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CountNoThrow
		=> new(_obj.GetStreamCount(out var x), x);

	public uint Count
		=> CountNoThrow.Value;

	public ComResult<MFAttributes> GetAttributesNoThrow(uint index)
		=> new(_obj.GetAttributes(index, out var x), new(x));

	public MFAttributes GetAttributes(uint index)
		=> GetAttributesNoThrow(index).Value;

	public IEnumerable<MFAttributes> AttributesArrayEnumerable
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetAttributes(i);
		}
	}

	public ImmutableArray<MFAttributes> AttributesArray
		=> [.. AttributesArrayEnumerable];
}
