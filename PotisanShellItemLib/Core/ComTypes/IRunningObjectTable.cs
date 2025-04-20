using System.Runtime.InteropServices.ComTypes;

namespace PotisanShellItemLib.Core.ComTypes;

[ComImport]
[Guid("00000010-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IRunningObjectTable
{
	[PreserveSig]
	int Register(
		uint grfFlags,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkObject,
		IMoniker pmkObjectName,
		out uint pdwRegister);

	[PreserveSig]
	int Revoke(
		uint dwRegister);

	[PreserveSig]
	int IsRunning(
		IMoniker pmkObjectName);

	[PreserveSig]
	int GetObject(
		IMoniker pmkObjectName,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppunkObject);

	[PreserveSig]
	int NoteChangeTime(
		uint dwRegister,
		FILETIME pfiletime);

	[PreserveSig]
	int GetTimeOfLastChange(
		IMoniker pmkObjectName,
		out FILETIME pfiletime);

	[PreserveSig]
	int EnumRunning(
		out IEnumMoniker ppenumMoniker);
}