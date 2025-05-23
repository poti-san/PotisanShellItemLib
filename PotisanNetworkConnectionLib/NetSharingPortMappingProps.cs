using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// ポート割り当てプロパティ。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetSharingPortMappingProps</c> COMインターフェイスのラッパーです。
/// </remarks>
public class NetSharingPortMappingProps(object? o) : ComUnknownWrapperBase<INetSharingPortMappingProps>(o)
{
	public ComDispatch AsDispatch
		=> new(_obj);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> NameNoThrow
		=> new(_obj.get_Name(out var x), x!);

	public string Name
		=> NameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NatIpProtocol> IPProtocolNoThrow
		=> new(_obj.get_IPProtocol(out var x), (NatIpProtocol)x);

	public NatIpProtocol IPProtocol
		=> IPProtocolNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> ExternalPortNoThrow
		=> new(_obj.get_ExternalPort(out var x), x);

	public int ExternalPort
		=> ExternalPortNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> InternalPortNoThrow
		=> new(_obj.get_InternalPort(out var x), x);

	public int InternalPort
		=> InternalPortNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> OptionsNoThrow
		=> new(_obj.get_Options(out var x), x);

	public uint Options
		=> OptionsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> TargetNameNoThrow
		=> new(_obj.get_TargetName(out var x), x!);

	public string TargetName
		=> TargetNameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> TargetIPAddressNoThrow
		=> new(_obj.get_TargetIPAddress(out var x), x!);

	public string TargetIPAddress
		=> TargetIPAddressNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> EnabledNoThrow
		=> new(_obj.get_Enabled(out var x), x);

	public bool Enabled
		=> EnabledNoThrow.Value;
}