namespace Potisan.Windows.MSIme.ComTypes;

[ComImport]
[Guid("019F7153-E6DB-11d0-83C3-00C04FDDB82E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IFEDictionary
{
	[PreserveSig]
	int Open(
		[MarshalAs(UnmanagedType.LPWStr)] string pchDictPath,
		out ImeUserDictionaryFileHeader pshf);

	[PreserveSig]
	int Close();

	[PreserveSig]
	int GetHeader(
		[MarshalAs(UnmanagedType.LPStr)] string pchDictPath,
		out ImeUserDictionaryFileHeader pshf,
		out ImeDictionaryFormat pjfmt,
		out int pulType);

	[PreserveSig]
	int DisplayProperty(
		nint hwnd);

	[PreserveSig]
	int GetPosTable(
		out nint/*POSTBL*/ prgPosTbl,
		out int pcPosTbl);

	[PreserveSig]
	int GetWords(
		[MarshalAs(UnmanagedType.LPWStr)] string pwchFirst,
		[MarshalAs(UnmanagedType.LPWStr)] string pwchLast,
		[MarshalAs(UnmanagedType.LPWStr)] string pwchDisplay,
		uint ulPos,
		uint ulSelect,
		uint ulWordSrc,
		[MarshalAs(UnmanagedType.I2)] ref byte pchBuffer, // IMEWRD
		uint cbBuffer,
		out uint pcWrd);
	[PreserveSig]
	int NextWords(
		ref byte pchBuffer,
		uint cbBuffer,
		out uint pcWrd);

	[PreserveSig]
	int Create(
		[MarshalAs(UnmanagedType.LPStr)] string pchDictPath,
		in ImeUserDictionaryFileHeader pshf);

	[PreserveSig]
	int SetHeader(
		in ImeUserDictionaryFileHeader pshf);

	[PreserveSig]
	int ExistWord(
		ImeWord pwrd);

	[PreserveSig]
	int ExistDependency(
		IMEDP pdp);

	[PreserveSig]
	int RegisterWord(
		ImeWordRegistringPlace reg,
		in ImeWord pwrd);

	[PreserveSig]
	int RegisterDependency(
		ImeWordRegistringPlace reg,
		in IMEDP pdp);

	[PreserveSig]
	int GetDependencies(
		[MarshalAs(UnmanagedType.LPWStr)] string pwchKakariReading,
		[MarshalAs(UnmanagedType.LPWStr)] string pwchKakariDisplay,
		uint ulKakariPos,
		[MarshalAs(UnmanagedType.LPWStr)] string pwchUkeReading,
		[MarshalAs(UnmanagedType.LPWStr)] string pwchUkeDisplay,
		uint ulUkePos,
		ImeRelationType jrel,
		uint ulWordSrc,
		ref byte pchBuffer,
		uint cbBuffer,
		out uint pcdp);

	[PreserveSig]
	int NextDependencies(
		ref byte pchBuffer,
		uint cbBuffer,
		out uint pcDp);

	[PreserveSig]
	int ConvertFromOldMSIME(
		[MarshalAs(UnmanagedType.LPStr)] string pchDic,
		ImeLogFunc pfnLog,
		ImeWordRegistringPlace reg);

	[PreserveSig]
	int ConvertFromUserToSys();
}

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
[return: MarshalAs(UnmanagedType.Bool)]
public delegate bool ImeLogFunc(in IMEDP p, int hr);

// PosTableData
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct POSTBL
{
	public ushort nPos;           // pos番号
	public nint /*BYTE**/ szName; // posの名前
}
