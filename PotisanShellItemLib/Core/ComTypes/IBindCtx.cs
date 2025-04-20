namespace PotisanShellItemLib.Core.ComTypes;

/// <summary>
/// BIND_OPTS
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class BIND_OPTS
{
	public uint cbStruct;
	public uint grfFlags;
	public uint grfMode;
	public uint dwTickCountDeadline;
}

[StructLayout(LayoutKind.Sequential)]
public class BIND_OPTS2 : BIND_OPTS
{
	public uint dwTrackFlags;
	public uint dwClassContext;
	public Lcid locale;
	public nint/*COSERVERINFO **/  pServerInfo;
}

[StructLayout(LayoutKind.Sequential)]
public class BIND_OPTS3 : BIND_OPTS2
{
	public nint hwnd;
}

public enum BIND_FLAGS : uint
{
	MayBotherUser = 1,
	JustTestExistence = 2
}

[ComImport]
[Guid("0000000e-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IBindCtx
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
		in BIND_OPTS pbindopts);

	[PreserveSig]
	int GetBindOptions(
		out BIND_OPTS pbindopts);

	[PreserveSig]
	int GetRunningObjectTable(
		out IRunningObjectTable pprot);

	[PreserveSig]
	int RegisterObjectParam(
		[MarshalAs(UnmanagedType.LPWStr)] string pszKey,
		[MarshalAs(UnmanagedType.IUnknown)] out object? punk);

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