namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00020401-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITypeInfo
{
	[PreserveSig]
	int GetTypeAttr(
		out nint ppTypeAttr);

	[PreserveSig]
	int GetTypeComp(
		out ITypeComp ppTComp);

	[PreserveSig]
	int GetFuncDesc(
		uint index,
		out nint ppFuncDesc);

	[PreserveSig]
	int GetVarDesc(
		uint index,
		out nint ppVarDesc);

	[PreserveSig]
	int GetNames(
		ComMemberID memid,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.BStr)][Out] string[] rgBstrNames,
		uint cMaxNames,
		out uint pcNames);

	[PreserveSig]
	int GetRefTypeOfImplType(
		uint index,
		out uint pRefType);

	[PreserveSig]
	int GetImplTypeFlags(
		uint index,
		out int pImplTypeFlags);

	[PreserveSig]
	int GetIDsOfNames(
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rgszNames,
		uint cNames,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out ComMemberID[] pMemId);

	[PreserveSig]
	int Invoke(
		[MarshalAs(UnmanagedType.IUnknown)] object pvInstance,
		ComMemberID memid,
		ushort wFlags,
		[Out, In] DISPPARAMS pDispParams,
		[MarshalAs(UnmanagedType.Struct)] out object? pVarResult,
		[Out] ComExceptionInfo pExcepInfo,
		out uint puArgErr);

	[PreserveSig]
	int GetDocumentation(
		ComMemberID memid,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrName,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrDocString,
		out uint pdwHelpContext,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrHelpFile);

	[PreserveSig]
	int GetDllEntry(
		ComMemberID memid,
		ComInvokeKind invKind,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrDllName,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrName,
		out ushort pwOrdinal);

	[PreserveSig]
	int GetRefTypeInfo(
		uint hRefType,
		out ITypeInfo ppTInfo);

	[PreserveSig]
	int AddressOfMember(
		ComMemberID memid,
		ComInvokeKind invKind,
		out nint ppv);

	[PreserveSig]
	int CreateInstance(
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnkOuter,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvObj);

	[PreserveSig]
	int GetMops(
		ComMemberID memid,
		[MarshalAs(UnmanagedType.BStr)] out string? pBstrMops);

	[PreserveSig]
	int GetContainingTypeLib(
		out ITypeLib ppTLib,
		out uint pIndex);

	[PreserveSig]
	void ReleaseTypeAttr(
		nint pTypeAttr);

	[PreserveSig]
	void ReleaseFuncDesc(
		nint pFuncDesc);

	[PreserveSig]
	void ReleaseVarDesc(
		nint pVarDesc);
}