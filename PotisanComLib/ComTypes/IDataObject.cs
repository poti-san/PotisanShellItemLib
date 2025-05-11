// TODO
namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000010e-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IDataObject
{
	[PreserveSig]
	int GetData(
		ComFormatEtc pformatetcIn,
		[Out] ComStorageMedium pmedium);

	[PreserveSig]
	int GetDataHere(
		ComFormatEtc pformatetc,
		[In][Out] ComStorageMedium pmedium);

	[PreserveSig]
	int QueryGetData(
		ComFormatEtc pformatetc);

	[PreserveSig]
	int GetCanonicalFormatEtc(
		ComFormatEtc pformatectIn,
		[Out] ComFormatEtc pformatetcOut);

	[PreserveSig]
	int SetData(
		ComFormatEtc pformatetc,
		ComStorageMedium pmedium,
		[MarshalAs(UnmanagedType.Bool)] bool fRelease);

	[PreserveSig]
	int EnumFormatEtc(
		uint dwDirection,
		out IEnumFORMATETC ppenumFormatEtc);

	[PreserveSig]
	int DAdvise(
		ComFormatEtc pformatetc,
		uint advf,
		IAdviseSink pAdvSink,
		out uint pdwConnection);

	[PreserveSig]
	int DUnadvise(
		uint dwConnection);

	[PreserveSig]
	int EnumDAdvise(
		out IEnumSTATDATA ppenumAdvise);
}