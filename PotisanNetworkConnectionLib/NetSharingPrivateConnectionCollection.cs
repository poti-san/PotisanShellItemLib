using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// プライベート接続コレクション。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetSharingPrivateConnectionCollection</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class NetSharingPrivateConnectionCollection(object? o) : ComUnknownWrapperBase<INetSharingPrivateConnectionCollection>(o)
{
	public ComDispatch AsDispatch
		=> new(_obj);

	public ComResult<NetSharingPrivateConnectionEnumerable> EnumerableNoThrow
		=> new(_obj.get__NewEnum(out var x), new(x));

	public NetSharingPrivateConnectionEnumerable Enumerable
		=> EnumerableNoThrow.Value;

	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;
}
