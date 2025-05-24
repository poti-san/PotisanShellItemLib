using System.Collections.Immutable;

using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// 共有設定。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetSharingConfiguration</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class NetSharingConfiguration(object? o) : ComUnknownWrapperBase<INetSharingConfiguration>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> SharingEnabledNoThrow
		=> new(_obj.get_SharingEnabled(out var x), x);

	public bool SharingEnabled
		=> SharingEnabledNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<SharingConnectionType> SharingConnectionTypeNoThrow
		=> new(_obj.get_SharingConnectionType(out var x), x);

	public SharingConnectionType SharingConnectionType
		=> SharingConnectionTypeNoThrow.Value;

	public ComResult DisableSharingNoThrow()
		=> new(_obj.DisableSharing());

	public void DisableSharing()
		=> DisableSharingNoThrow().ThrowIfError();

	public ComResult EnableSharingNoThrow(SharingConnectionType type)
		=> new(_obj.EnableSharing(type));

	public void EnableSharing(SharingConnectionType type)
		=> EnableSharingNoThrow(type).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> InternetFirewallEnabledNoThrow
		=> new(_obj.get_InternetFirewallEnabled(out var x), x);

	public bool InternetFirewallEnabled
		=> InternetFirewallEnabledNoThrow.Value;

	public ComResult DisableInternetFirewallNoThrow()
		=> new(_obj.DisableInternetFirewall());

	public void DisableInternetFirewall()
		=> DisableInternetFirewallNoThrow().ThrowIfError();

	public ComResult EnableInternetFirewallNoThrow()
		=> new(_obj.EnableInternetFirewall());

	public void EnableInternetFirewall()
		=> EnableInternetFirewallNoThrow().ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetSharingPortMappingCollection> PortMappingCollectionNoThrow
		=> new(_obj.get_EnumPortMappings(SharingConnectionEnumFlag.Default, out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public NetSharingPortMappingCollection PortMappingCollection
		=> PortMappingCollectionNoThrow.Value;

	public ImmutableArray<NetSharingPortMapping> PortMappings
		=> [.. PortMappingCollection.Enumerable];

	public ComResult<NetSharingPortMapping> AddPortMappingNoThrow(
		string name,
		NatIpProtocol ipProtocol,
		ushort externalPort,
		ushort internalPort,
		uint options,
		string targetNameOrIpAddress,
		IcsTargetType targetType)
	{
		return new(_obj.AddPortMapping(name, (byte)ipProtocol, externalPort, internalPort, options, targetNameOrIpAddress, targetType, out var x), new(x));
	}

	public NetSharingPortMapping AddPortMapping(
		string name,
		NatIpProtocol ipProtocol,
		ushort externalPort,
		ushort internalPort,
		uint options,
		string targetNameOrIpAddress,
		IcsTargetType targetType)
	{
		return AddPortMappingNoThrow(name, ipProtocol, externalPort, internalPort, options, targetNameOrIpAddress, targetType).Value;
	}

	public ComResult RemovePortMappingNoThrow(NetSharingPortMapping mapping)
		=> new(_obj.RemovePortMapping((INetSharingPortMapping)mapping.WrappedObject!));

	public void RemovePortMapping(NetSharingPortMapping mapping)
		=> RemovePortMappingNoThrow(mapping).ThrowIfError();
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>SHARINGCONNECTIONTYPE</c></remarks>
public enum SharingConnectionType
{
	Public = 0,
	Private = 1,
}

/// <summary>
/// 
/// </summary>
/// <remarks>
/// <c>SHARINGCONNECTION_ENUM_FLAGS</c>
/// </remarks>
[Flags]
public enum SharingConnectionEnumFlag
{
	Default = 0,
	Enabled = 1,
}

/// <summary>
/// 
/// </summary>
/// <remarks>
/// <c>ICS_TARGETTYPE</c>
/// </remarks>
public enum IcsTargetType
{
	Name = 0,
	IPAddress = 1,
}

public enum NatIpProtocol : byte
{
	Tcp = 6,
	Udp = 17,
}