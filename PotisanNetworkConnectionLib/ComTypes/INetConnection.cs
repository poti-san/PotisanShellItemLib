using System;

namespace Potisan.Windows.Network.ComTypes;

[ComImport]
[Guid("C08956A1-1CD3-11D1-B1C5-00805FC1270E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface INetConnection
{
	[PreserveSig]
	int Connect();

	[PreserveSig]
	int Disconnect();

	[PreserveSig]
	int Delete();

	[PreserveSig]
	int Duplicate(
		[MarshalAs(UnmanagedType.LPWStr)] string pszwDuplicateName,
		out INetConnection ppCon);

	[PreserveSig]
	int GetProperties(
		out nint ppProps);

	[PreserveSig]
	int GetUiObjectClassId(
		out Guid pclsid);

	[PreserveSig]
	int Rename(
		[MarshalAs(UnmanagedType.LPWStr)] string pszwNewName);
}

public readonly struct NETCON_PROPERTIES
{
	public readonly Guid guidId;
	public readonly nint pszwName;
	public readonly nint pszwDeviceName;
	public readonly NetConStatus Status;
	public readonly NetConMediaType MediaType;
	public readonly uint dwCharacter;
	public readonly Guid clsidThisObject;
	public readonly Guid clsidUiObject;
}