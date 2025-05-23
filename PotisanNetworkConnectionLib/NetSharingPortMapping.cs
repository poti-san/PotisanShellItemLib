using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// ポート割り当て。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetSharingPortMapping</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class NetSharingPortMapping(object? o) : ComUnknownWrapperBase<INetSharingPortMapping>(o)
{
	public ComDispatch AsDispatch
		=> new(_obj);

	public ComResult DisableNoThrow()
		=> new(_obj.Disable());

	public void Disable()
		=> DisableNoThrow().ThrowIfError();

	public ComResult EnableNoThrow()
		=> new(_obj.Enable());

	public void Enable()
		=> EnableNoThrow().ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetSharingPortMappingProps> PropertiesNoThrow
		=> new(_obj.get_Properties(out var x), new(x));

	public NetSharingPortMappingProps Properties
		=> PropertiesNoThrow.Value;

	public ComResult DeleteNoThrow()
		=> new(_obj.Delete());

	public void Delete()
		=> DeleteNoThrow().ThrowIfError();

	public override string ToString()
		=> Properties.NameNoThrow.Or(null) ?? "";
}
