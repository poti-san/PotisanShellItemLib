namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("5c6c44bf-1db6-435b-9249-e8cd10fdec96")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFPluginControl
{
	[PreserveSig]
	int GetPreferredClsid(
		uint pluginType,
		string selector,
		out Guid clsid);

	[PreserveSig]
	int GetPreferredClsidByIndex(
		uint pluginType,
		uint index,
		[MarshalAs(UnmanagedType.LPWStr)] out string selector,
		out Guid clsid);

	[PreserveSig]
	int SetPreferredClsid(
		uint pluginType,
		string selector,
		in Guid clsid);

	[PreserveSig]
	int IsDisabled(
		uint pluginType,
		in Guid clsid);

	[PreserveSig]
	int GetDisabledByIndex(
		uint pluginType,
		uint index,
		out Guid clsid);

	[PreserveSig]
	int SetDisabled(
		uint pluginType,
		in Guid clsid,
		[MarshalAs(UnmanagedType.Bool)] bool disabled);
}

[ComImport]
[Guid("C6982083-3DDC-45CB-AF5E-0F7A8CE4DE77")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFPluginControl2 // IMFPluginControl
{
	#region IMFPluginControl

	[PreserveSig]
	int GetPreferredClsid(
	uint pluginType,
	string selector,
	out Guid clsid);

	[PreserveSig]
	int GetPreferredClsidByIndex(
		uint pluginType,
		uint index,
		[MarshalAs(UnmanagedType.LPWStr)] out string selector,
		out Guid clsid);

	[PreserveSig]
	int SetPreferredClsid(
		uint pluginType,
		string selector,
		in Guid clsid);

	[PreserveSig]
	int IsDisabled(
		uint pluginType,
		in Guid clsid);

	[PreserveSig]
	int GetDisabledByIndex(
		uint pluginType,
		uint index, out Guid clsid);

	[PreserveSig]
	int SetDisabled(
		uint pluginType,
		in Guid clsid,
		[MarshalAs(UnmanagedType.Bool)] bool disabled);

	#endregion // IMGPlguinControl

	[PreserveSig]
	int SetPolicy(
		MFPluginControlPolicy policy);
}
