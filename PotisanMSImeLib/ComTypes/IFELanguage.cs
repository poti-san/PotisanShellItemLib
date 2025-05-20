using Potisan.Windows.Com.SafeHandles;

namespace Potisan.Windows.MSIme.ComTypes;

[ComImport]
[Guid("019F7152-E6DB-11d0-83C3-00C04FDDB82E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IFELanguage
{
	[PreserveSig]
	int Open();

	[PreserveSig]
	int Close();

	[PreserveSig]
	int GetJMorphResult(
		uint dwRequest,
		uint dwCMode,
		int cwchInput,
		[MarshalAs(UnmanagedType.I2)] in char pwchInput,
		[MarshalAs(UnmanagedType.LPArray)] FELanguageMorphologyInfo[]? pfCInfo,
		out SafeCoTaskMemHandle ppResult);

	[PreserveSig]
	int GetConversionModeCaps(
		out uint pdwCaps);

	[PreserveSig]
	int GetPhonetic(
		[MarshalAs(UnmanagedType.BStr)] string str,
		int start,
		int length,
		[MarshalAs(UnmanagedType.BStr)] out string phonetic);

	[PreserveSig]
	int GetConversion(
		[MarshalAs(UnmanagedType.BStr)] string str,
		int start,
		int length,
		[MarshalAs(UnmanagedType.BStr)] out string result);
}