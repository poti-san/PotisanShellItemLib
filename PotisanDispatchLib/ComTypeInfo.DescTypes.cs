//
// ITypeInfoで使用される～Desc型のラッパー
// ラッパーが不要な型がほとんどですが、対称性のためにすべてラップします。
//

using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public sealed class ComTypeAttribute(in TYPEATTR source)
{
	public Guid Guid { get; } = source.guid;
	public Lcid Lcid { get; } = source.lcid;
	public uint Reserved { get; } = source.dwReserved;
	public ComMemberID ConstructorMemberID { get; } = source.memidConstructor;
	public ComMemberID DestructorMemberID { get; } = source.memidDestructor;
	public string? Schema { get; } = source.lpstrSchema;
	public uint InstanceSize { get; } = source.cbSizeInstance;
	public ComTypeKind TypeKind { get; } = source.typekind;
	public ushort FunctionCount { get; } = source.cFuncs;
	public ushort VariableCount { get; } = source.cVars;
	public ushort ImplementedTypeCount { get; } = source.cImplTypes;
	public ushort VftSize { get; } = source.cbSizeVft;
	public ushort AlignmentSize { get; } = source.cbAlignment;
	public ComTypeFlag Flags { get; } = source.wTypeFlags;
	public ushort MajorVersionNumber { get; } = source.wMajorVerNum;
	public ushort MinorVersionNumber { get; } = source.wMinorVerNum;
	public ComTypeDescription AliasTypeDesc { get; } = new(source.tdescAlias);
	public ComIdlDescription TypeIdleDesc { get; } = new(source.idldescType);
}

public sealed class ComIdlDescription(in IDLDESC source)
{
	public nuint Reserved { get; } = source.dwReserved;
	public ComIdlFlag Flags { get; } = source.wIDLFlags;
}

public sealed class ComTypeDescription(in TYPEDESC source)
{
	public ComTypeDescription? ArraySubTypeDescription { get; } = source.vt == VarType.SafeArray && source.u.lptdesc != 0
		? new(Marshal.PtrToStructure<TYPEDESC>(source.u.lptdesc)!) : null;

	public ComTypeDescription? PointerSubTypeDescription { get; } = source.vt == VarType.Ptr && source.u.lptdesc != 0
		? new(Marshal.PtrToStructure<TYPEDESC>(source.u.lptdesc)!) : null;

	public VarType VarType { get; } = source.vt;
}

public sealed class ComFunctionDescription(in FUNCDESC source)
{
	public ComMemberID MemberID { get; } = source.memid;
	public ImmutableArray<int> SCodes { get; } = ImmutableCollectionsMarshal.AsImmutableArray(
		InternalUtil.PtrToUnmanagedArray<int>(source.lprgscode, source.cScodes));
	public ComElementDescription? ParameterElementDescriptions { get; }
		= source.lprgelemdescParam != 0 ? new(Marshal.PtrToStructure<ELEMDESC>(source.lprgelemdescParam)) : null;
	public ComFunctionKind FunctionKind { get; } = source.funckind;
	public ComInvokeKind InvokeKind { get; } = source.invkind;
	public ComCallConversion CallConversion { get; } = source.callconv;
	public short ParamCount { get; } = source.cParams;
	public short OptionalParamCount { get; } = source.cParamsOpt;
	public short VftOffset { get; } = source.oVft;
	public ELEMDESC? ElementDescs { get; } = source.elemdescFunc;
	public ComFunctionFlag FunctionFlags { get; } = source.wFuncFlags;
}

public sealed class ComElementDescription(in ELEMDESC source)
{
	public ComTypeDescription? TypeDesc { get; } = new(source.tdesc);
	public ComParameterDescription? ParamDesc { get; } = new(source.paramdesc);
}

public sealed class ComParameterDescription(in PARAMDESC source)
{
	public ComParameterFlag ParamFlags { get; } = source.wParamFlags;
	public object? DefaultValue { get; } = source.pparamdescex != 0 ? Marshal.PtrToStructure<PARAMDESCEX>(source.pparamdescex).varDefaultValue : null;
}

public sealed class ComVariableDescription(in VARDESC source)
{
	public ComMemberID MemberID { get; } = source.memid;
	public string? Scheme { get; } = source.lpstrSchema;
	public uint? InstanceOffset { get; } = source.varkind == ComVariableKind.PerInstance ? source.u.oInstance : null;
	public object? ConstValue { get; } = source.varkind == ComVariableKind.Const ? source.u.lpvarValue : null;
	public ComElementDescription? ElemDesc { get; } = new(source.elemdescVar);
	public ComVariableFlag VarFlags { get; } = source.wVarFlags;
	public ComVariableKind VarType { get; } = source.varkind;
}

public sealed class ComArrayDescription
{
	public ComTypeDescription ElementTypeDesc { get; }
	public ushort Dim { get; }
	public ImmutableArray<(uint ElementCount, int LBound)> Bounds { get; }

	public ComArrayDescription(nint p)
	{
		ElementTypeDesc = new(Marshal.PtrToStructure<TYPEDESC>(p)!);
		Dim = (ushort)Marshal.ReadInt16(p + Marshal.OffsetOf<ARRAYDESC1>(nameof(ARRAYDESC1.cDims)));
		Bounds = ImmutableCollectionsMarshal.AsImmutableArray(
			InternalUtil.PtrToUnmanagedArray<(uint ElementCount, int LBound)>(
				p + Marshal.OffsetOf<ARRAYDESC1>(nameof(ARRAYDESC1.cDims)), Dim));
	}

	// Marshal.OffsetOfで参照します。
	private readonly struct ARRAYDESC1
	{
		public readonly TYPEDESC tdescElem;
		public readonly ushort cDims;
		public readonly nint/*SAFEARRAYBOUND*/ rgbounds0;
	}
}