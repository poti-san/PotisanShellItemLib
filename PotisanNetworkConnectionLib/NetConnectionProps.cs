using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// ネットワーク接続のプロパティ。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>INetConnectionProps</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class NetConnectionProps(object? o) : ComUnknownWrapperBase<INetConnectionProps>(o)
{
	public ComDispatch AsDispatch
		=> new(_obj);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid> GuidNoThrow
		=> new(_obj.get_Guid(out var x), new(x!));

	public Guid Guid
		=> GuidNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> NameNoThrow
		=> new(_obj.get_Name(out var x), x!);

	public string Name
		=> NameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DeviceNameNoThrow
		=> new(_obj.get_DeviceName(out var x), x!);

	public string DeviceName
		=> DeviceNameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetConStatus> StatusNoThrow
		=> new(_obj.get_Status(out var x), x);

	public NetConStatus Status
		=> StatusNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<NetConMediaType> MediaTypeNoThrow
		=> new(_obj.get_MediaType(out var x), x);

	public NetConMediaType MediaType
		=> MediaTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CharacteristicsNoThrow
		=> new(_obj.get_Characteristics(out var x), x);

	public uint Characteristics
		=> CharacteristicsNoThrow.Value;

	public override string ToString()
		=> NameNoThrow.Or(null) ?? "";
}
