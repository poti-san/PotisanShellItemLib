using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.MediaFoundation.Mux.ComTypes;

namespace Potisan.Windows.MediaFoundation.Mux;

public sealed class MFMuxStreamSampleManager(object? o) : ComUnknownWrapperBase<IMFMuxStreamSampleManager>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> StreamCountNoThrow
		=> new(_obj.GetStreamCount(out var x), x);

	public uint StreamCount
		=> StreamCountNoThrow.Value;

	public ComResult<MFSample> GetSampleNoThrow(uint streamIndex)
		=> new(_obj.GetSample(streamIndex, out var x), new(x));

	public MFSample GetSample(uint streamIndex)
		=> GetSampleNoThrow(streamIndex).Value;

	public IEnumerable<MFSample> SampleEnumerable
	{
		get
		{
			var c = StreamCount;
			for (uint i = 0; i < c; i++)
				yield return GetSample(i);
		}
	}

	public ImmutableArray<MFSample> Samples => [..SampleEnumerable];

	/// <summary>
	/// アクティブなストリーム構成を取得します。
	/// 詳細は<see cref="IMFMuxStreamMediaTypeManager"/>を確認してください。
	/// </summary>
	/// <remarks>
	/// このメソッドは対応するCOMメソッドにNoThrow版がありません。
	/// </remarks>
	public ulong StreamConfiguration
		=> _obj.GetStreamConfiguration();
}
