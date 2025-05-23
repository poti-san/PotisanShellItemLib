namespace Potisan.Windows.Network.ComTypes;

[ComImport]
[Guid("C08956A2-1CD3-11D1-B1C5-00805FC1270E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface INetConnectionManager
{
	[PreserveSig]
	int EnumConnections(
		NetConManagerEnumFlag Flags,
		out IEnumNetConnection ppEnum);
}