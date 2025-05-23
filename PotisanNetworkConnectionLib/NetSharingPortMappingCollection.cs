using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// ポート割り当てコレクション。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetSharingPortMappingCollection</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class NetSharingPortMappingCollection(object? o) : ComUnknownWrapperBase<INetSharingPortMappingCollection>(o)
{
	public ComDispatch AsDispatch
		=> new(_obj);

	public ComResult<NetSharingPortMappingEnumerable> EnumerableNoThrow
		=> new(_obj.get__NewEnum(out var x), new(x));

	public NetSharingPortMappingEnumerable Enumerable
		=> EnumerableNoThrow.Value;

	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;
}
