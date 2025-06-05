using System.Collections.Immutable;

using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Com.Automation;

/// <summary>
/// タイプライブラリの型情報。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class ComTypeInfo2(object? o) : ComTypeInfo(o)
{
	private new readonly ITypeInfo2 _obj = o != null ? (ITypeInfo2)o : null!;

	public ComResult<ComTypeKind> TypeKindNoThrow
		=> new(_obj.GetTypeKind(out var x), x);

	public ComTypeKind TypeKind
		=> TypeKindNoThrow.Value;

	public ComResult<ComTypeFlag> TypeFlagsNoThrow
		=> new(_obj.GetTypeFlags(out var x), (ComTypeFlag)x);

	public ComTypeFlag TypeFlags
		=> TypeFlagsNoThrow.Value;

	public ComResult<uint> GetFuncIndexNoThrow(ComMemberID memberId, ComInvokeKind kind)
		=> new(_obj.GetFuncIndexOfMemId(memberId, kind, out var x), x);

	public uint GetFuncIndex(ComMemberID memberId, ComInvokeKind kind)
		=> GetFuncIndexNoThrow(memberId, kind).Value;

	public ComResult<uint> GetVarIndexNoThrow(ComMemberID memberId)
		=> new(_obj.GetVarIndexOfMemId(memberId, out var x), x);

	public uint GetVarIndex(ComMemberID memberId)
		=> GetVarIndexNoThrow(memberId).Value;

	public ComResult<object?> GetCustomDataNoThrow(in Guid guid)
		=> new(_obj.GetCustData(guid, out var x), x);

	public object? GetCustomData(in Guid guid)
		=> GetCustomDataNoThrow(guid).Value;

	public ComResult<object?> GetFunctionCustomDataNoThrow(uint index, in Guid guid)
		=> new(_obj.GetFuncCustData(index, guid, out var x), x);

	public object? GetFunctionCustomData(uint index, in Guid guid)
		=> GetFunctionCustomDataNoThrow(index, guid).Value;

	public ComResult<object?> GetParameterCustomDataNoThrow(uint indexFunc, uint indexParam, in Guid guid)
		=> new(_obj.GetParamCustData(indexFunc, indexParam, guid, out var x), x);

	public object? GetParameterCustomData(uint indexFunc, uint indexParam, in Guid guid)
		=> GetParameterCustomDataNoThrow(indexFunc, indexParam, guid).Value;

	public ComResult<object?> GetVariableCustomDataNoThrow(uint index, in Guid guid)
		=> new(_obj.GetVarCustData(index, guid, out var x), x);

	public object? GetVariableCustomData(uint index, in Guid guid)
		=> GetVariableCustomDataNoThrow(index, guid).Value;

	public ComResult<object?> GetImplementedTypeCustomDataNoThrow(uint index, in Guid guid)
		=> new(_obj.GetVarCustData(index, guid, out var x), x);

	public object? GetImplementedTypeCustomData(uint index, in Guid guid)
		=> GetImplementedTypeCustomDataNoThrow(index, guid).Value;

	public ComResult<ComTypeInfoDocumentation2> GetDocumentation2NoThrow(ComMemberID memberId, Lcid lcid)
		=> new(_obj.GetDocumentation2(memberId, lcid, out var x1, out var x2, out var x3), new(x1!, x2, x3!));

	public ComTypeInfoDocumentation2 GetDocumentation2(ComMemberID memberId, Lcid lcid)
		=> GetDocumentation2NoThrow(memberId, lcid).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ImmutableArray<ComTypeCustomDataItem>> AllCustomDatasNoThrow
		=> new(_obj.GetAllCustData(out var x), ComTypeCustomDataItem.FromTypeCustomData(x));

	public ImmutableArray<ComTypeCustomDataItem> AllCustomDatas
		=> AllCustomDatasNoThrow.Value;

	public ComResult<ImmutableArray<ComTypeCustomDataItem>> GetAllFunctionCustomDatasNoThrow(uint index)
		=> new(_obj.GetAllFuncCustData(index, out var x), ComTypeCustomDataItem.FromTypeCustomData(x));

	public ImmutableArray<ComTypeCustomDataItem> GetAllFunctionCustomDatas(uint index)
		=> GetAllFunctionCustomDatasNoThrow(index).Value;

	public ComResult<ImmutableArray<ComTypeCustomDataItem>> GetAllParameterCustomDatasNoThrow(uint indexFunc, uint indexParam)
		=> new(_obj.GetAllParamCustData(indexFunc, indexParam, out var x), ComTypeCustomDataItem.FromTypeCustomData(x));

	public ImmutableArray<ComTypeCustomDataItem> GetAllParameterCustomDatas(uint indexFunc, uint indexParam)
		=> GetAllParameterCustomDatasNoThrow(indexFunc, indexParam).Value;

	public ComResult<ImmutableArray<ComTypeCustomDataItem>> GetAllVariableCustomDatasNoThrow(uint index)
		=> new(_obj.GetAllVarCustData(index, out var x), ComTypeCustomDataItem.FromTypeCustomData(x));

	public ImmutableArray<ComTypeCustomDataItem> GetAllVariableCustomDatas(uint index)
		=> GetAllVariableCustomDatasNoThrow(index).Value;

	public ComResult<ImmutableArray<ComTypeCustomDataItem>> GetAllImplementedTypeCustomDatasNoThrow(uint index)
		=> new(_obj.GetAllImplTypeCustData(index, out var x), ComTypeCustomDataItem.FromTypeCustomData(x));

	public ImmutableArray<ComTypeCustomDataItem> GetAllImplementedTypeCustomDatas(uint index)
		=> GetAllImplementedTypeCustomDatasNoThrow(index).Value;

	// TODO All～CustomDatasをより便利に
}

public readonly struct ComTypeCustomDataItem
{
	public readonly Guid Guid;
	public readonly object? Value;

	public unsafe static ImmutableArray<ComTypeCustomDataItem> FromTypeCustomData(in CUSTDATA data)
	{
		// 公式ドキュメントに解放の記載がないので、解放処理は行いません。
		if (data.cCustData == 0) return [];
		return new ReadOnlySpan<ComTypeCustomDataItem>((void*)data.prgCustData0, (int)data.cCustData)
			.ToImmutableArray();
	}
}

public record struct ComTypeInfoDocumentation2(string HelpString, uint HelpStringContext, string HelpStringDll);