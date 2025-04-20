using System.Runtime.InteropServices.ComTypes;

namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00000010-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IRunningObjectTable
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
		FileTime pfiletime);

	[PreserveSig]
	int GetTimeOfLastChange(
		IMoniker pmkObjectName,
		out FileTime pfiletime);

	[PreserveSig]
	int EnumRunning(
		out IEnumMoniker ppenumMoniker);
}