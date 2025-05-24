using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// ネットワーク接続。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <para>作成には<see cref="NetSharingManager"/>や<see cref="NetConnectionManager"/>を使用します。</para>
/// <para><c>INetConnection</c> COMインターフェイスのラッパーです。</para>
/// </remarks>
public sealed class NetConnection(object? o) : ComUnknownWrapperBase<INetConnection>(o)
{
	public ComResult ConnectNoThrow()
		=> new(_obj.Connect());

	public void Connect()
		=> ConnectNoThrow().ThrowIfError();

	public ComResult DisconnectNoThrow()
		=> new(_obj.Disconnect());

	public void Disconnect()
		=> DisconnectNoThrow().ThrowIfError();

	public ComResult DeleteNoThrow()
		=> new(_obj.Delete());

	public void Delete()
		=> DeleteNoThrow().ThrowIfError();

	public ComResult<NetConnection> DuplicateNoThrow(string duplicateName)
		=> new(_obj.Duplicate(duplicateName, out var x), new(x));

	public NetConnection Duplicate(string duplicateName)
		=> DuplicateNoThrow(duplicateName).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetConProperties> PropertiesNoThrow
	{
		get
		{
			var hr = _obj.GetProperties(out var pprops);
			if (hr < 0) return new(hr, null!);
			return new(hr, NetConProperties.GetAndFree(ref pprops));
		}
	}

	public NetConProperties Properties
		=> PropertiesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid> UIObjectClsidNoThrow
		=> new(_obj.GetUiObjectClassId(out var x), x);

	public Guid UIObjectClassId
		=> UIObjectClsidNoThrow.Value;

	public ComResult RenameNoThrow(string newName)
		=> new(_obj.Rename(newName));

	public void Rename(string newName)
		=> RenameNoThrow(newName).ThrowIfError();

	public override string ToString()
		=> Properties.Name;
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>NETCON_CHARACTERISTIC_FLAGS</c></remarks>
[Flags]
public enum NetConCharacteristicFlag : uint
{
	None = 0,
	AllUsers = 0x1,
	AllowDuplication = 0x2,
	AllowRemoval = 0x4,
	AllowRename = 0x8,
	IncomingOnly = 0x20,
	OutgoingOnly = 0x40,
	Branded = 0x80,
	Shared = 0x100,
	Bridged = 0x200,
	Firewalled = 0x400,
	Default = 0x800,
	HomeNetCapale = 0x1000,
	SharedPrivate = 0x2000,
	Quarantined = 0x4000,
	Reserved = 0x8000,
	HostedNetwork = 0x10000,
	VirtualStation = 0x20000,
	WifiDirect = 0x40000,
	BluetoothMask = 0xf0000,
	LanMask = 0xf00000,
}

/// <summary>
/// 
/// </summary>
/// <remarks>
/// <c>NETCON_STATUS</c>
/// </remarks>
public enum NetConStatus
{
	Disconnected = 0,
	Connecting = Disconnected + 1,
	Connected = Connecting + 1,
	Disconnecting = Connected + 1,
	HardwareNotPresentNCS_HARDWARE_NOT_PRESENT = Disconnecting + 1,
	HardwareDisabled = HardwareNotPresentNCS_HARDWARE_NOT_PRESENT + 1,
	HardwareMalfunction = HardwareDisabled + 1,
	MediaDisconnected = HardwareMalfunction + 1,
	Authencating = MediaDisconnected + 1,
	AuthencationSucceeded = Authencating + 1,
	AuthencationFailed = AuthencationSucceeded + 1,
	InvalidAccess = AuthencationFailed + 1,
	CredentialsRequired = InvalidAccess + 1,
	ActionRequired = CredentialsRequired + 1,
	ActionRequiredRetry = ActionRequired + 1,
	ConnectFailed = ActionRequiredRetry + 1,
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>NETCON_TYPE</c></remarks>
public enum NetConType
{
	DirectConnect = 0,
	Inbound = 1,
	Internet = Inbound + 1,
	Lan = Internet + 1,
	Phone = Lan + 1,
	Tunnel = Phone + 1,
	Bridge = Tunnel + 1,
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>NETCON_MEDIATYPE</c></remarks>
public enum NetConMediaType
{
	None = 0,
	Direct = None + 1,
	Isdn = 2,
	Lan = Isdn + 1,
	Phone = Lan + 1,
	Tunnel = Phone + 1,
	Pppoe = Tunnel + 1,
	Bridge = Pppoe + 1,
	SharedAccessHostLan = Bridge + 1,
	SharedAccessHostRas = SharedAccessHostLan + 1,
}

/// <summary>
/// 接続のプロパティ。
/// </summary>
/// <remarks><c>NETCON_PROPERTIES</c></remarks>
[StructLayout(LayoutKind.Sequential)]
public record class NetConProperties(
	Guid ID,
	string Name,
	string DeviceName,
	NetConStatus Status,
	NetConMediaType MediaType,
	NetConCharacteristicFlag Characteristics,
	Guid ThisObjectClsid,
	Guid UIObjectClsid)
{
	/// <summary>
	/// <see cref="NETCON_PROPERTIES"/>へのポインタからオブジェクトを作成します。
	/// 通常は内部でのみ使用します。
	/// </summary>
	/// <param name="p"></param>
	/// <returns></returns>
	public unsafe static NetConProperties GetAndFree(ref nint p)
	{
		[DllImport("netshell.dll")]
		static extern void NcFreeNetconProperties(nint props);

		try
		{
			NETCON_PROPERTIES* props = (NETCON_PROPERTIES*)p;
			var x = new NetConProperties(
				props->guidId,
				Marshal.PtrToStringUni(props->pszwName) ?? "",
				Marshal.PtrToStringUni(props->pszwDeviceName) ?? "",
				props->Status,
				props->MediaType,
				(NetConCharacteristicFlag)props->dwCharacter,
				props->clsidThisObject,
				props->clsidUiObject);
			return x;
		}
		finally
		{
			NcFreeNetconProperties(p);
		}
	}
}