using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// パブリック接続コレクション。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks><c>INetSharingPublicConnectionCollection</c> COMインターフェイスのラッパーです。</remarks>
public sealed class NetSharingPublicConnectionCollection(object? o) : ComUnknownWrapperBase<INetSharingPublicConnectionCollection>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public ComResult<NetSharingPublicConnectionEnumerable> EnumerableNoThrow
		=> new(_obj.get__NewEnum(out var x), new(x));

	public NetSharingPublicConnectionEnumerable Enumerable
		=> EnumerableNoThrow.Value;

	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;
}
