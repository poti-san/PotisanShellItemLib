using System.Collections.Immutable;

using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// ネットワーク共有の管理機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetSharingManager</c> COMインターフェイスのラッパーです。
/// </remarks>
/// <example>
/// 任意の接続のプロパティと設定を取得する。
/// <code>
/// <![CDATA[
/// using Potisan.Windows.Network;
/// 
/// var sharingManager = NetSharingManager.Create();
/// var coll = sharingManager.EveryConnectionCollection;
/// foreach (var connection in coll.Enumerable)
/// {
///		var props = sharingManager.GetNetConnectionProps(connection);
///		var config = sharingManager.GetConfiguration(connection);
/// 
///		// TODO: ここで接続やプロパティ、設定を操作します。
/// 
///		Console.WriteLine();
/// }]]>
/// </code>
/// </example>
public sealed class NetSharingManager(object? o) : ComUnknownWrapperBase<INetSharingManager>(o)
{
	public static ComResult<NetSharingManager> CreateNoThrow()
	{
		Guid CLSID_NetSharingManager = new("5C63C1AD-3956-4FF8-8486-40034758315B");
		return ComHelper.CreateInstanceNoThrow<NetSharingManager, INetSharingManager>(CLSID_NetSharingManager);
	}

	public static NetSharingManager Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> SharingInstalledNoThrow
		=> new(_obj.get_SharingInstalled(out var x), x);

	public bool SharingInstalled
		=> SharingInstalledNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetSharingPublicConnectionCollection> PublicConnectionCollectionNoThrow
		=> new(_obj.get_EnumPublicConnections(SharingConnectionEnumFlag.Default, out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public NetSharingPublicConnectionCollection PublicConnectionCollection
		=> PublicConnectionCollectionNoThrow.Value;

	public ImmutableArray<NetConnection> PublicConnections
		=> [.. PublicConnectionCollection.Enumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetSharingPrivateConnectionCollection> PrivateConnectionCollectionNoThrow
		=> new(_obj.get_EnumPrivateConnections(SharingConnectionEnumFlag.Default, out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public NetSharingPrivateConnectionCollection PrivateConnectionCollection
		=> PrivateConnectionCollectionNoThrow.Value;

	public ImmutableArray<NetConnection> PrivateConnections
		=> [.. PrivateConnectionCollection.Enumerable];

	public ComResult<NetSharingConfiguration> GetConfigurationNoThrow(NetConnection connection)
		=> new(_obj.get_INetSharingConfigurationForINetConnection((INetConnection)connection.WrappedObject!, out var x), new(x));

	public NetSharingConfiguration GetConfiguration(NetConnection connection)
		=> GetConfigurationNoThrow(connection).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetSharingEveryConnectionCollection> EveryConnectionCollectionNoThrow
		=> new(_obj.get_EnumEveryConnection(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public NetSharingEveryConnectionCollection EveryConnectionCollection
		=> EveryConnectionCollectionNoThrow.Value;

	public ImmutableArray<NetConnection> EveryConnections
		=> [.. EveryConnectionCollection.Enumerable];

	public ComResult<NetConnectionProps> GetNetConnectionPropsNoThrow(NetConnection connection)
		=> new(_obj.get_NetConnectionProps((INetConnection)connection.WrappedObject!, out var x), new(x));

	public NetConnectionProps GetNetConnectionProps(NetConnection connection)
		=> GetNetConnectionPropsNoThrow(connection).Value;
}
