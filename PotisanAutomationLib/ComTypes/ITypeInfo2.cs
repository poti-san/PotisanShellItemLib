namespace Potisan.Windows.Com.Automation.ComTypes;

[ComImport]
[Guid("00020412-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITypeInfo2 // ITypeInfo
{
	#region ITypeInfo

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

	#endregion ITypeInfo

	[PreserveSig]
	int GetTypeKind(
		out ComTypeKind pTypeKind);

	[PreserveSig]
	int GetTypeFlags(
		out uint pTypeFlags);

	[PreserveSig]
	int GetFuncIndexOfMemId(
		ComMemberID memid,
		ComInvokeKind invKind,
		out uint pFuncIndex);

	[PreserveSig]
	int GetVarIndexOfMemId(
		ComMemberID memid,
		out uint pVarIndex);

	[PreserveSig]
	int GetCustData(
		in Guid guid,
		[MarshalAs(UnmanagedType.Struct)] out object? pVarVal);

	[PreserveSig]
	int GetFuncCustData(
		uint index,
		in Guid guid,
		[MarshalAs(UnmanagedType.Struct)] out object? pVarVal);

	[PreserveSig]
	int GetParamCustData(
		uint indexFunc,
		uint indexParam,
		in Guid guid,
		[MarshalAs(UnmanagedType.Struct)] out object? pVarVal);

	[PreserveSig]
	int GetVarCustData(
		uint index,
		in Guid guid,
		[MarshalAs(UnmanagedType.Struct)] out object? pVarVal);

	[PreserveSig]
	int GetImplTypeCustData(
		uint index,
		in Guid guid,
		[MarshalAs(UnmanagedType.Struct)] out object? pVarVal);

	[PreserveSig]
	int GetDocumentation2(
		ComMemberID memid,
		Lcid lcid,
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrHelpString,
		out uint pdwHelpStringContext,
		[MarshalAs(UnmanagedType.BStr)] out string? pbstrHelpStringDll);

	[PreserveSig]
	int GetAllCustData(
		out CUSTDATA pCustData);

	[PreserveSig]
	int GetAllFuncCustData(
		uint index,
		out CUSTDATA pCustData);

	[PreserveSig]
	int GetAllParamCustData(
		uint indexFunc,
		uint indexParam,
		out CUSTDATA pCustData);

	[PreserveSig]
	int GetAllVarCustData(
		uint index,
		out CUSTDATA pCustData);

	[PreserveSig]
	int GetAllImplTypeCustData(
		uint index,
		out CUSTDATA pCustData);
}

public struct CUSTDATAITEM
{
	public Guid guid;
	[MarshalAs(UnmanagedType.Struct)]
	public object varValue;
}

public struct CUSTDATA
{
	public uint cCustData;
	public nint prgCustData0;
}