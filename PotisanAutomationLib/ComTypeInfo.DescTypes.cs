//
// ITypeInfoで使用される～Desc型のラッパー
// ラッパーが不要な型がほとんどですが、対称性のためにすべてラップします。
//

using System.Collections.Immutable;

using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Com.Automation;

/// <summary>
/// タイプライブラリの型属性。
/// </summary>
/// <param name="source"></param>
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

/// <summary>
/// タイプライブラリのIDL記述。
/// </summary>
/// <param name="source"></param>
public sealed class ComIdlDescription(in IDLDESC source)
{
	public nuint Reserved { get; } = source.dwReserved;
	public ComIdlFlag Flags { get; } = source.wIDLFlags;
}


/// <summary>
/// タイプライブラリの型記述。
/// </summary>
/// <param name="source"></param>
public sealed class ComTypeDescription(in TYPEDESC source)
{
	public ComTypeDescription? ArraySubTypeDescription { get; } = source.vt == VarType.SafeArray && source.u.lptdesc != 0
		? new(Marshal.PtrToStructure<TYPEDESC>(source.u.lptdesc)!) : null;

	public ComTypeDescription? PointerSubTypeDescription { get; } = source.vt == VarType.Ptr && source.u.lptdesc != 0
		? new(Marshal.PtrToStructure<TYPEDESC>(source.u.lptdesc)!) : null;

	public VarType VarType { get; } = source.vt;
}

/// <summary>
/// タイプライブラリの関数記述。
/// </summary>
/// <remarks>
/// 詳細は<seealso href="https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-oaut/d3349d25-e11d-4095-ba86-de3fda178c4e">公開仕様</seealso>を確認してください。
/// </remarks>
public sealed class ComFunctionDescription
{
	public ComMemberID MemberID { get; }
	public ImmutableArray<int> SCodes { get; }
	public ImmutableArray<ComElementDescription> ParameterElementDescriptions { get; }
	public ComFunctionKind FunctionKind { get; }
	public ComInvokeKind InvokeKind { get; }
	public ComCallConversion CallConversion { get; }
	public short ParamCount { get; }
	public short OptionalParamCount { get; }
	public short VftOffset { get; }
	public ELEMDESC? ElementDescs { get; }
	public ComFunctionFlag FunctionFlags { get; }

	public ComFunctionDescription(in FUNCDESC source)
	{
		MemberID = source.memid;
		SCodes = ImmutableCollectionsMarshal.AsImmutableArray(InternalUtil.PtrToUnmanagedArray<int>(source.lprgscode, source.cScodes));
		if (source.cParams != 0)
		{
			var p = source.lprgelemdescParam;
			var structSize = Marshal.SizeOf<ELEMDESC>();
			var descs = new ComElementDescription[source.cParams];
			for (var i = 0; i < source.cParams; i++)
			{
				descs[i] = new(Marshal.PtrToStructure<ELEMDESC>(p));
				p += structSize;
			}
			ParameterElementDescriptions = ImmutableCollectionsMarshal.AsImmutableArray(descs);
		}
		else
		{
			ParameterElementDescriptions = [];
		}
		FunctionKind = source.funckind;
		InvokeKind = source.invkind;
		CallConversion = source.callconv;
		ParamCount = source.cParams;
		OptionalParamCount = source.cParamsOpt;
		VftOffset = source.oVft;
		ElementDescs = source.elemdescFunc;
		FunctionFlags = source.wFuncFlags;
	}
}

/// <summary>
/// タイプライブラリの要素記述。
/// </summary>
/// <param name="source"></param>
public sealed class ComElementDescription(in ELEMDESC source)
{
	public ComTypeDescription? TypeDesc { get; } = new(source.tdesc);
	public ComParameterDescription? ParamDesc { get; } = new(source.paramdesc);
}

/// <summary>
/// タイプライブラリの引数記述。
/// </summary>
/// <param name="source"></param>
public sealed class ComParameterDescription(in PARAMDESC source)
{
	public ComParameterFlag ParamFlags { get; } = source.wParamFlags;
	public object? DefaultValue { get; } = source.pparamdescex != 0 ? Marshal.PtrToStructure<PARAMDESCEX>(source.pparamdescex).varDefaultValue : null;
}

/// <summary>
/// タイプライブラリの変数記述。
/// </summary>
/// <param name="source"></param>
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

/// <summary>
/// タイプライブラリの配列記述。
/// </summary>
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