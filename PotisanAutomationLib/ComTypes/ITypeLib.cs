namespace Potisan.Windows.Com.Automation.ComTypes;

[ComImport]
[Guid("00020402-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITypeLib
{
	[PreserveSig]
	uint GetTypeInfoCount();

	[PreserveSig]
	int GetTypeInfo(
		uint index,
		out ITypeInfo ppTInfo);

	[PreserveSig]
	int GetTypeInfoType(
		uint index,
		out ComTypeKind pTKind);

	[PreserveSig]
	int GetTypeInfoOfGuid(
		in Guid guid,
		out ITypeInfo ppTinfo);

	[PreserveSig]
	int GetLibAttr(
		out nint ppTLibAttr);

	[PreserveSig]
	int GetTypeComp(
		out ITypeComp ppTComp);

	[PreserveSig]
	int GetDocumentation(
		int index,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrName,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrDocString,
		out uint pdwHelpContext,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrHelpFile);

	[PreserveSig]
	int IsName(
		[MarshalAs(UnmanagedType.LPWStr)] string szNameBuf,
		uint lHashVal,
		[MarshalAs(UnmanagedType.Bool)] out bool pfName);

	[PreserveSig]
	int FindName(
		[MarshalAs(UnmanagedType.LPWStr)] string szNameBuf,
		uint lHashVal,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] out ITypeInfo[] ppTInfo,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] out ComMemberID[] rgMemId,
		out ushort pcFound);

	[PreserveSig]
	void ReleaseTLibAttr(
		nint pTLibAttr);
}