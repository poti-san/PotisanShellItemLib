using Potisan.Windows.Com.SafeHandles;

namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("000214F9-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellLinkW
{
	[PreserveSig]
	int GetPath(
		[MarshalAs(UnmanagedType.I2)] ref char pszFile,
		int cch,
		nint pfd,
		uint fFlags);

	[PreserveSig]
	int GetIDList(
		out SafeCoTaskMemHandle ppidl);

	[PreserveSig]
	int SetIDList(
		SafeCoTaskMemHandle pidl);

	[PreserveSig]
	int GetDescription(
		[MarshalAs(UnmanagedType.I2)] ref char pszName,
		int cch);

	[PreserveSig]
	int SetDescription(
		[MarshalAs(UnmanagedType.LPWStr)] string pszName);

	[PreserveSig]
	int GetWorkingDirectory(
		[MarshalAs(UnmanagedType.I2)] ref char pszDir,
		int cch);

	[PreserveSig]
	int SetWorkingDirectory(
		[MarshalAs(UnmanagedType.LPWStr)] string pszDir);

	[PreserveSig]
	int GetArguments(
		[MarshalAs(UnmanagedType.I2)] ref char pszArgs,
		int cch);

	[PreserveSig]
	int SetArguments(
		[MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

	[PreserveSig]
	int GetHotkey(
		out ushort pwHotkey);

	[PreserveSig]
	int SetHotkey(
		ushort wHotkey);

	[PreserveSig]
	int GetShowCmd(
		out int piShowCmd);

	[PreserveSig]
	int SetShowCmd(
		int iShowCmd);

	[PreserveSig]
	int GetIconLocation(
		[MarshalAs(UnmanagedType.I2)] ref char pszIconPath,
		int cch,
		out int piIcon);

	[PreserveSig]
	int SetIconLocation(
		[MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
		int iIcon);

	[PreserveSig]
	int SetRelativePath(
		[MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
		uint dwReserved);

	[PreserveSig]
	int Resolve(
		nint hwnd,
		uint fFlags);

	[PreserveSig]
	int SetPath(
		[MarshalAs(UnmanagedType.LPWStr)] string pszFile);
}