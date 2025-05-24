using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// 任意の接続コレクション。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetSharingEveryConnectionCollection</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class NetSharingEveryConnectionCollection(object? o) : ComUnknownWrapperBase<INetSharingEveryConnectionCollection>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public ComResult<NetSharingEveryConnectionEnumerable> EnumerableNoThrow
		=> new(_obj.get__NewEnum(out var x), new(x));

	public NetSharingEveryConnectionEnumerable Enumerable
		=> EnumerableNoThrow.Value;

	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;
}
