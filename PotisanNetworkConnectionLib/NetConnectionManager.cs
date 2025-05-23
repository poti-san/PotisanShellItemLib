using System.Collections.Immutable;

using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// ネットワーク接続の管理機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetConnectionManager</c> COMインターフェイスのラッパーです。
/// </remarks>
/// <example>
/// <code>
/// <![CDATA[using Potisan.Windows.Network;
/// 
/// var connectionManager = NetConnectionManager.Create();
/// foreach (var connection in connectionManager.DefaultConnections)
/// {
/// 	Console.WriteLine(connection.Properties.Name);
/// }]]>
/// </code>
/// </example>
public sealed class NetConnectionManager(object? o) : ComUnknownWrapperBase<INetConnectionManager>(o)
{
	public static ComResult<NetConnectionManager> CreateNoThrow()
	{
		// {ba126ad1-2166-11d1-b1d0-00805fc1270e}
		Guid CLSID_NetConnectionManager = new(0xba126ad1, 0x2166, 0x11d1, 0xb1, 0xd0, 0x00, 0x80, 0x5f, 0xc1, 0x27, 0x0e);
		return ComHelper.CreateInstanceNoThrow<NetConnectionManager, INetConnectionManager>(CLSID_NetConnectionManager);
	}

	public static NetConnectionManager Create()
		=> CreateNoThrow().Value;

	public ComResult<NetConnectionEnumerable> GetConnectionEnumerableNoThrow(NetConManagerEnumFlag flags)
		=> new(_obj.EnumConnections(flags, out var x), new(x));

	public NetConnectionEnumerable GetConnectionEnumerable(NetConManagerEnumFlag flags)
		=> GetConnectionEnumerableNoThrow(flags).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetConnectionEnumerable> DefaultConnectionEnumerableNoThrow
		=> GetConnectionEnumerableNoThrow(NetConManagerEnumFlag.Default);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public NetConnectionEnumerable DefaultConnectionEnumerable
		=> GetConnectionEnumerable(NetConManagerEnumFlag.Default);

	public ImmutableArray<NetConnection> DefaultConnections
		=> [.. DefaultConnectionEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetConnectionEnumerable> HiddenConnectionEnumerableNoThrow
		=> GetConnectionEnumerableNoThrow(NetConManagerEnumFlag.Hidden);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public NetConnectionEnumerable HiddenConnectionEnumerable
		=> GetConnectionEnumerable(NetConManagerEnumFlag.Hidden);

	public ImmutableArray<NetConnection> HiddenConnections
		=> [.. HiddenConnectionEnumerable];
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>NETCONMGR_ENUM_FLAGS</c></remarks>
public enum NetConManagerEnumFlag : uint
{
	Default = 0,
	Hidden = 0x1,
}