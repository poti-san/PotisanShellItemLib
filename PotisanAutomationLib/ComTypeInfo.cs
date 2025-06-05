using System.Collections.Immutable;

using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Com.Automation;

/// <summary>
/// タイプライブラリの型情報。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class ComTypeInfo(object? o) : ComUnknownWrapperBase<ITypeInfo>(o)
{
	public ComTypeInfo2? AsTypeInfo2 => this.As<ComTypeInfo2, ITypeInfo2>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComTypeAttribute> TypeAttributeNoThrow
	{
		get
		{
			var cr = new ComResult(_obj.GetTypeAttr(out var x));
			try
			{
				if (!cr) return new(cr.HResult, null!);
				return new(cr.HResult, new(Marshal.PtrToStructure<TYPEATTR>(x)!));
			}
			finally
			{
				_obj.ReleaseTypeAttr(x);
			}
		}
	}

	public ComTypeAttribute TypeAttribute
		=> TypeAttributeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComTypeComp> TypeCompNoThrow
		=> new(_obj.GetTypeComp(out var x), new(x));

	public ComTypeComp TypeComp
		=> TypeCompNoThrow.Value;

	public ComResult<ComFunctionDescription> GetFunctionDescriptionNoThrow(uint index)
	{
		var cr = new ComResult(_obj.GetFuncDesc(index, out var x));
		try
		{
			if (!cr) return new(cr.HResult, null!);
			return new(cr.HResult, new(Marshal.PtrToStructure<FUNCDESC>(x)!));
		}
		finally
		{
			_obj.ReleaseFuncDesc(x);
		}
	}

	public ComFunctionDescription GetFunctionDescription(uint index)
		=> GetFunctionDescriptionNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ComFunctionDescription> FunctionDescriptionEnumerable
	{
		get
		{
			var c = TypeAttribute.FunctionCount;
			for (uint i = 0; i < c; i++)
			{
				yield return GetFunctionDescription(i);
			}
		}
	}

	public ImmutableArray<ComFunctionDescription> FunctionDescriptions
		=> [.. FunctionDescriptionEnumerable];

	public ComResult<ComVariableDescription> GetVariableDescriptionNoThrow(uint index)
	{
		var cr = new ComResult(_obj.GetVarDesc(index, out var x));
		try
		{
			if (!cr) return new(cr.HResult, null!);
			return new(cr.HResult, new(Marshal.PtrToStructure<VARDESC>(x)!));
		}
		finally
		{
			_obj.ReleaseVarDesc(x);
		}
	}

	public ComVariableDescription GetVariableDescription(uint index)
		=> GetVariableDescriptionNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ComVariableDescription> VariableDescriptionEnumerable
	{
		get
		{
			var c = TypeAttribute.VariableCount;
			for (uint i = 0; i < c; i++)
			{
				yield return GetVariableDescription(i);
			}
		}
	}

	public ImmutableArray<ComVariableDescription> VariableDescriptions
		=> [.. VariableDescriptionEnumerable];

	public ComResult<string[]> GetNamesNoThrow(ComMemberID memberId, uint maxLength)
	{
		var names = new string[maxLength];
		return new(_obj.GetNames(memberId, names, maxLength, out var cNames), names[..(int)cNames]);
	}

	public string[] GetNames(ComMemberID memberId, uint maxLength)
		=> GetNamesNoThrow(memberId, maxLength).Value;

	public ComResult<uint> GetRefTypeOfImplementedTypeNoThrow(uint index)
		=> new(_obj.GetRefTypeOfImplType(index, out var x), x);

	public uint GetRefTypeOfImplementedType(uint index)
		=> GetRefTypeOfImplementedTypeNoThrow(index).Value;

	public ComResult<ComImplementedTypeFlag> GetImplementedTypeFlagsNoThrow(uint index)
		=> new(_obj.GetImplTypeFlags(index, out var x), (ComImplementedTypeFlag)x);

	public ComImplementedTypeFlag GetImplementedTypeFlags(uint index)
		=> GetImplementedTypeFlagsNoThrow(index).Value;

	public ComResult<ImmutableArray<ComMemberID>> GetIDsOfNamesNoThrow(string[] names)
		=> new(_obj.GetIDsOfNames(names, (uint)names.Length, out var x), ImmutableCollectionsMarshal.AsImmutableArray(x));

	public ImmutableArray<ComMemberID> GetIDsOfNames(string[] names)
		=> GetIDsOfNamesNoThrow(names).Value;

	public ComResult<ComDispatchInvokeResult> InvokeNoThrow(
		object instance,
		ComMemberID memberId,
		ComDispatchKind flags,
		object?[]? arguments,
		ComMemberID[]? namedArguments)
	{
		var excepInfo = new ComExceptionInfo();
		using var varArray = arguments != null ? new NativeVariantArrayOnCoTaskMem(arguments!) : null;
		using var namedArgs = namedArguments != null ? ComHelper.SafeStructureArrayToPtr<ComMemberID>(namedArguments, true) : null;
		var dispParams = new DISPPARAMS
		{
			cArgs = (uint)(varArray?.Length ?? 0),
			cNamedArgs = (uint)(namedArguments?.Length ?? 0),
			rgvarg = varArray?.Pointer ?? 0,
			rgdispidNamedArgs = namedArgs?.DangerousGetHandle() ?? 0,
		};
		return new(_obj.Invoke(instance, memberId, (ushort)flags,
			dispParams, out var x, excepInfo, out var errArgIndex), new(x, excepInfo, errArgIndex));
	}

	public ComDispatchInvokeResult Invoke(
		object instance,
		ComMemberID memberId,
		ComDispatchKind flags,
		object?[]? arguments,
		ComMemberID[]? namedArguments)
		=> InvokeNoThrow(instance, memberId, flags, arguments, namedArguments).Value;

	public ComResult<ComTypeInfoDocumentation> GetDocumentationNoThrow(ComMemberID memberId)
		=> new(_obj.GetDocumentation(memberId, out var name, out var docStr, out var helpCtx, out var helpFile),
			new(name ?? "", docStr ?? "", helpCtx, helpFile ?? ""));

	public ComTypeInfoDocumentation GetDocumentation(ComMemberID memberId)
		=> GetDocumentationNoThrow(memberId).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComTypeInfoDocumentation> DocumentationNoThrow
		=> GetDocumentationNoThrow(new(-1));

	public ComTypeInfoDocumentation Documentation
		=> DocumentationNoThrow.Value;

	public ComResult<(string DllName, string Name, ushort Ordinal)> GetDllEntryNoThrow(ComMemberID memberId, ComInvokeKind kind)
		=> new(_obj.GetDllEntry(memberId, kind, out var x1, out var x2, out var x3), (x1 ?? "", x2 ?? "", x3));

	public (string DllName, string Name, ushort Ordinal) GetDllEntry(ComMemberID memberId, ComInvokeKind kind)
		=> GetDllEntryNoThrow(memberId, kind).Value;

	public ComResult<ComTypeInfo> GetRefTypeInfoNoThrow(uint refTypeHandle)
		=> new(_obj.GetRefTypeInfo(refTypeHandle, out var x), new(x));

	public ComResult<nint> AddressOfMemberNoThrow(ComMemberID memberId, ComInvokeKind kind)
		=> new(_obj.AddressOfMember(memberId, kind, out var x), x);

	public nint AddressOfMember(ComMemberID memberId, ComInvokeKind kind)
		=> AddressOfMemberNoThrow(memberId, kind).Value;

	public ComResult<object> CreateInstanceNoThrow(in Guid iid, object? outerObject = null)
		=> new(_obj.CreateInstance(outerObject, iid, out var x), x!);

	public object CreateInstance(in Guid iid, object? outerObject = null)
		=> CreateInstanceNoThrow(iid, outerObject).Value;

	public ComResult<TWrapper> CreateInstanceNoThrow<TWrapper, TInterface>(object? outerObject = null)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.CreateInstance(outerObject, typeof(TInterface).GUID, out var x), x);

	public TWrapper CreateInstance<TWrapper, TInterface>(object? outerObject = null)
		where TWrapper : IComUnknownWrapper
		=> CreateInstanceNoThrow<TWrapper, TInterface>(outerObject).Value;

	public ComResult<string> GetMopsNoThrow(ComMemberID memberId)
		=> new(_obj.GetMops(memberId, out var x), x ?? "");

	public string GetMops(ComMemberID memberId)
		=> GetMopsNoThrow(memberId).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(ComTypeLib TypeLib, uint TypeDescIndex)> ContainingTypeLibNoThrow
		=> new(_obj.GetContainingTypeLib(out var tlib, out var index), (new(tlib), index));

	public (ComTypeLib TypeLib, uint TypeDescIndex) ContainingTypeLib
		=> ContainingTypeLibNoThrow.Value;

	//
	// ユーティリティメソッド
	//

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ComMemberID> FunctionMemberIDEnumerable
		=> FunctionDescriptions.Select(desc => desc.MemberID);

	public ImmutableArray<ComMemberID> FunctionMemberIDs
		=> [.. FunctionMemberIDEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ComMemberID> VariableMemberIDEnumerable
		=> VariableDescriptions.Select(desc => desc.MemberID);

	public ImmutableArray<ComMemberID> VariableMemberIDs
		=> [.. VariableMemberIDEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ComTypeInfoDocumentation> FunctionDocumentationEnumerable
		=> FunctionMemberIDEnumerable.Select(GetDocumentation);

	public ImmutableArray<ComTypeInfoDocumentation> FunctionDocumentations
		=> [.. FunctionDocumentationEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ComTypeInfoDocumentation> VariableDocumentationEnumerable
		=> VariableMemberIDEnumerable.Select(GetDocumentation);

	public ImmutableArray<ComTypeInfoDocumentation> VariableDocumentations
		=> [.. VariableDocumentationEnumerable];

	public IEnumerable<ComFunctionDescription> EnumerateFunctionDescriptions(
		ComFunctionKind[] functionKinds,
		ComInvokeKind[] invokeKinds)
	{
		foreach (var desc in FunctionDescriptionEnumerable)
		{
			if (Array.IndexOf(functionKinds, desc.FunctionKind) != -1 && Array.IndexOf(invokeKinds, desc.InvokeKind) != -1)
				yield return desc;
		}
	}

	public IEnumerable<ComFunctionDescription> EnumerateFunctionDescriptions(
		ComFunctionKind[] functionKinds)
	{
		foreach (var desc in FunctionDescriptionEnumerable)
		{
			if (Array.IndexOf(functionKinds, desc.FunctionKind) != -1)
				yield return desc;
		}
	}

	public IEnumerable<ComFunctionDescription> EnumerateFunctionDescriptions(
		ComInvokeKind[] invokeKinds)
	{
		foreach (var desc in FunctionDescriptionEnumerable)
		{
			if (Array.IndexOf(invokeKinds, desc.InvokeKind) != -1)
				yield return desc;
		}
	}

	public IEnumerable<ComFunctionDescription> GetFunctionDescriptions(
		ComFunctionKind[] functionKinds,
		ComInvokeKind[] invokeKinds)
		=> [.. EnumerateFunctionDescriptions(functionKinds, invokeKinds)];

	public IEnumerable<ComFunctionDescription> GetFunctionDescriptions(
		ComFunctionKind[] functionKinds)
		=> [.. EnumerateFunctionDescriptions(functionKinds)];

	public IEnumerable<ComFunctionDescription> GetFunctionDescriptions(
		ComInvokeKind[] invokeKinds)
		=> [.. EnumerateFunctionDescriptions(invokeKinds)];

	public IEnumerable<ComVariableDescription> EnumerateVariableDescriptions(ComVariableKind[] kinds)
	{
		foreach (var desc in VariableDescriptionEnumerable)
		{
			if (Array.IndexOf(kinds, desc.VarType) != -1)
			{
				yield return desc;
			}
		}
	}

	public ImmutableArray<ComVariableDescription> GetVariableDescriptions(ComVariableKind[] kinds)
		=> [.. EnumerateVariableDescriptions(kinds)];
}

public record struct ComTypeInfoDocumentation(string Name, string DocString, uint HelpContext, string HelpFile);

/// <summary>
/// CALLCONV
/// </summary>
public enum ComCallConversion : uint
{
	FastCall = 0,
	Cdecl = 1,
	MSCPascal = Cdecl + 1,
	Pascal = MSCPascal,
	MacPascal = Pascal + 1,
	StdCall = MacPascal + 1,
	FPFastCall = StdCall + 1,
	SysCall = FPFastCall + 1,
	MpwCdecl = SysCall + 1,
	MpwPascal = MpwCdecl + 1,
}

/// <summary>
/// FUNCKIND
/// </summary>
public enum ComFunctionKind : uint
{
	Virtual = 0,
	PureVirtual = Virtual + 1,
	NonVirtual = PureVirtual + 1,
	Static = NonVirtual + 1,
	Dispatch = Static + 1,
}

/// <summary>
/// INVOKEKIND
/// </summary>
public enum ComInvokeKind : uint
{
	Function = 1,
	PropertyGet = 2,
	PropertyPut = 4,
	PropertyPutRef = 8,
}

/// <summary>
/// TYPEFLAGS
/// </summary>
[Flags]
public enum ComTypeFlag : ushort
{
	AppObject = 0x1,
	CanCreate = 0x2,
	Licensed = 0x4,
	Predeclid = 0x8,
	Hidden = 0x10,
	Control = 0x20,
	Dual = 0x40,
	NonExtensible = 0x80,
	OleAutomation = 0x100,
	Restricted = 0x200,
	Aggregatable = 0x400,
	Replaceable = 0x800,
	Dispatchable = 0x1000,
	ReverseBind = 0x2000,
	Proxy = 0x4000,
}

/// <summary>
/// FUNCFLAGS
/// </summary>
[Flags]
public enum ComFunctionFlag : ushort
{
	Restricted = 0x1,
	Source = 0x2,
	Bindable = 0x4,
	RequestEdit = 0x8,
	DisplayBind = 0x10,
	DefaultBind = 0x20,
	Hidden = 0x40,
	UsesGetLastError = 0x80,
	DefaultCollElem = 0x100,
	UIDefault = 0x200,
	NonBrowsable = 0x400,
	Replaceable = 0x800,
	ImmediateBind = 0x1000,
}

/// <summary>
/// VARFLAGS
/// </summary>
[Flags]
public enum ComVariableFlag : ushort
{
	ReadOnly = 0x1,
	Source = 0x2,
	Bindable = 0x4,
	RequestEdit = 0x8,
	DisplayBind = 0x10,
	DefaultBind = 0x20,
	Hidden = 0x40,
	Restricted = 0x80,
	DefaultCollElem = 0x100,
	UIDefault = 0x200,
	NonBrowsable = 0x400,
	Replaceable = 0x800,
	ImmediateBind = 0x1000
}

/// <summary>
/// TYPEKIND
/// </summary>
public enum ComTypeKind : uint
{
	Enum = 0,
	Record = Enum + 1,
	Module = Record + 1,
	Interface = Module + 1,
	Dispatch = Interface + 1,
	CoClass = Dispatch + 1,
	Alias = CoClass + 1,
	Union = Alias + 1,
}

/// <summary>
/// PARAMFLAG
/// </summary>
[Flags]
public enum ComParameterFlag : ushort
{
	None = 0,
	In = 0x1,
	Out = 0x2,
	Lcid = 0x4,
	ReturnValue = 0x8,
	HasDefault = 0x20,
	HasCustomData = 0x40,
}

/// <summary>
/// IDLFLAG
/// </summary>
[Flags]
public enum ComIdlFlag : ushort
{
	None = 0,
	In = 0x1,
	Out = 0x2,
	Lcid = 0x4,
	ReturnValue = 0x8,
}

/// <summary>
/// VARKIND
/// </summary>
public enum ComVariableKind : uint
{
	PerInstance = 0,
	Static = 1,
	Const = 2,
	Dispatch = 3,
}

/// <summary>
/// IMPLTYPEFLAGS
/// </summary>
public enum ComImplementedTypeFlag : ushort
{
	Default = 0x1,
	Source = 0x2,
	Restricted = 0x4,
	DefaultVTable = 0x8,
}
