namespace Potisan.Windows.Com.ComTypes;

/// <summary>
/// BIND_OPTS
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class ComBindOptions
{
	public uint cbStruct;
	public uint grfFlags;
	public uint grfMode;
	public uint dwTickCountDeadline;
}

/// <summary>
/// BIND_OPTS2
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class ComBindOptions2 : ComBindOptions
{
	public uint dwTrackFlags;
	public uint dwClassContext;
	public Lcid locale;
	public nint/*COSERVERINFO **/  pServerInfo;
}

/// <summary>
/// BIND_OPTS3
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class ComBindOptions3 : ComBindOptions2
{
	public nint hwnd;
}

/// <summary>
/// BIND_FLAGS
/// </summary>
[Flags]
public enum ComBindFlag : uint
{
	MayBotherUser = 1,
	JustTestExistence = 2,
}

[ComImport]
[Guid("0000000e-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IBindCtx
{
	[PreserveSig]
	int RegisterObjectBound(
		[MarshalAs(UnmanagedType.IUnknown)] object punk);

	[PreserveSig]
	int RevokeObjectBound(
		[MarshalAs(UnmanagedType.IUnknown)] object punk);

	[PreserveSig]
	int ReleaseBoundObjects();

	[PreserveSig]
	int SetBindOptions(
		in ComBindOptions pbindopts);

	[PreserveSig]
	int GetBindOptions(
		out ComBindOptions pbindopts);

	[PreserveSig]
	int GetRunningObjectTable(
		out IRunningObjectTable pprot);

	[PreserveSig]
	int RegisterObjectParam(
		[MarshalAs(UnmanagedType.LPWStr)] string pszKey,
		[MarshalAs(UnmanagedType.IUnknown)] object? punk);

	[PreserveSig]
	int GetObjectParam(
		[MarshalAs(UnmanagedType.LPWStr)] string pszKey,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppunk);

	[PreserveSig]
	int EnumObjectParam(
		out IEnumString? ppenum);

	[PreserveSig]
	int RevokeObjectParam(
		[MarshalAs(UnmanagedType.LPWStr)] string pszKey);
}