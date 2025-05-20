namespace Potisan.Windows.MSIme.ComTypes;

[ComImport]
[Guid("019F7151-E6DB-11d0-83C3-00C04FDDB82E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IFECommon
{
	[PreserveSig]
	int IsDefaultIME(
		[MarshalAs(UnmanagedType.LPStr)] string? szName,
		int cszName);

	[PreserveSig]
	int SetDefaultIME();

	[PreserveSig]
	int InvokeWordRegDialog(
		in IMEDLG pimedlg);

	[PreserveSig]
	int InvokeDictToolDialog(
		in IMEDLG pimedlg);
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct IMEDLG
{
	public int cbIMEDLG;
	public nint hwnd;
	[MarshalAs(UnmanagedType.LPWStr)]
	public string? lpwstrWord;
	public int nTabId;
}