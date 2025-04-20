namespace Potisan.Windows.Com.ComTypes;

//
// 以下の構造体はクラスとして定義できません。
// structとして他のstructに含まれる場合があり、
// クラスとして定義するとポインタと解釈されエラーとなるためです。
//

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct IDLDESC
{
	public nuint dwReserved;
	public ComIdlFlag wIDLFlags;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct TYPEATTR
{
	public Guid guid;
	public Lcid lcid;
	public uint dwReserved;
	public ComMemberID memidConstructor;
	public ComMemberID memidDestructor;
	public string? lpstrSchema;
	public uint cbSizeInstance;
	public ComTypeKind typekind;
	public ushort cFuncs;
	public ushort cVars;
	public ushort cImplTypes;
	public ushort cbSizeVft;
	public ushort cbAlignment;
	public ComTypeFlag wTypeFlags;
	public ushort wMajorVerNum;
	public ushort wMinorVerNum;
	public TYPEDESC tdescAlias;
	public IDLDESC idldescType;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct TYPEDESC
{
	[StructLayout(LayoutKind.Explicit)]
	public struct DummyUnion
	{
		[FieldOffset(0)]
		public nint lptdesc; // TYPEDESC*
		[FieldOffset(0)]
		public nint lpadesc; // ARRAYDESC*
		[FieldOffset(0)]
		public uint hreftype; // HREFTYPE
	}
	public DummyUnion u;
	public VarType vt;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct FUNCDESC
{
	public ComMemberID memid;
	public nint lprgscode; // SCODE*
	public nint lprgelemdescParam;
	public ComFunctionKind funckind;
	public ComInvokeKind invkind;
	public ComCallConversion callconv;
	public short cParams;
	public short cParamsOpt;
	public short oVft;
	public short cScodes;
	public ELEMDESC elemdescFunc;
	public ComFunctionFlag wFuncFlags;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct ELEMDESC
{
	public TYPEDESC tdesc;
	public PARAMDESC paramdesc;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct PARAMDESCEX
{
	public uint cBytes;
	[MarshalAs(UnmanagedType.Struct)]
	public object varDefaultValue;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct PARAMDESC
{
	public nint pparamdescex; // PARAMDESCEX
	public ComParameterFlag wParamFlags;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct VARDESC
{
	[StructLayout(LayoutKind.Explicit)]
	public struct DummyUnion
	{
		[FieldOffset(0)]
		public uint oInstance;
		[FieldOffset(0)]
		public nint lpvarValue;
	}

	public ComMemberID memid;
	public string? lpstrSchema;
	public DummyUnion u;
	public ELEMDESC elemdescVar;
	public ComVariableFlag wVarFlags;
	public ComVariableKind varkind;
}

//public struct ARRAYDESC
//{
//	TYPEDESC tdescElem;
//	USHORT cDims;
//	SAFEARRAYBOUND rgbounds[1];
//}