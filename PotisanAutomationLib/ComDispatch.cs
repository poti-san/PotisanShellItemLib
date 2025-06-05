using System.Collections.Immutable;

using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Com.Automation;

/// <summary>
/// タイプライブラリの型情報と紐づけられたオブジェクト。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class ComDispatch(object? o) : ComUnknownWrapperBase<IDispatch>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> HasTypeInfoNoThrow
		=> new(_obj.GetTypeInfoCount(out var x), x != 0);

	public bool HasTypeInfo
		=> HasTypeInfoNoThrow.Value;

	public ComResult<ComTypeInfo> GetTypeInfoNoThrow(Lcid? lcid = null)
		=> new(_obj.GetTypeInfo(0, lcid ?? Lcid.UserDefault, out var x), new(x));

	public ComTypeInfo GetTypeInfo(Lcid? lcid = null)
		=> GetTypeInfoNoThrow(lcid).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComTypeInfo> TypeInfoNoThrow
		=> GetTypeInfoNoThrow();

	public ComTypeInfo TypeInfo
		=> TypeInfoNoThrow.Value;

	public ComResult<int[]> GetIDsOfNamesNoThrow(in Guid iid, string[] names, Lcid? lcid = null)
		=> new(_obj.GetIDsOfNames(iid, names, (uint)names.Length, lcid ?? Lcid.UserDefault, out var x), x);

	public int[] GetIDsOfNames(in Guid iid, string[] names, Lcid? lcid = null)
		=> GetIDsOfNamesNoThrow(iid, names, lcid).Value;

	/// <summary>
	/// メソッドやプロパティを実行します。
	/// </summary>
	/// <param name="memberId">メソッドやプロパティのメンバーID。</param>
	/// <param name="kind">メソッドまたはプロパティの指定。</param>
	/// <param name="arguments">引数。逆順で与えられることに注意してください。</param>
	/// <param name="namedArgumentIds">名前付き引数の識別子。</param>
	/// <param name="lcid">言語ID。複数言語IDをサポートしない場合は無視されます。</param>
	/// <returns>実行結果を表す<see cref="ComDispatchInvokeResult"/>オブジェクト。</returns>
	public ComResult<ComDispatchInvokeResult> InvokeNoThrow(
		ComMemberID memberId,
		ComDispatchKind kind,
		object?[]? arguments,
		ComMemberID[]? namedArgumentIds,
		Lcid? lcid = null)
	{
		const int DISP_E_TYPEMISMATCH = unchecked((int)0x80020005);
		const int DISP_E_PARAMNOTFOUND = unchecked((int)0x80020004);

		var excepInfo = new ComExceptionInfo();
		using var varArray = arguments != null ? new NativeVariantArrayOnCoTaskMem(arguments!) : null;
		using var namedArgs = namedArgumentIds != null ? ComHelper.SafeStructureArrayToPtr<ComMemberID>(namedArgumentIds, true) : null;
		var dispParams = new DISPPARAMS
		{
			cArgs = (uint)(varArray?.Length ?? 0),
			cNamedArgs = (uint)(namedArgumentIds?.Length ?? 0),
			rgvarg = varArray?.Pointer ?? 0,
			rgdispidNamedArgs = namedArgs?.DangerousGetHandle() ?? 0,
		};
		var hr = _obj.Invoke(memberId, Guid.Empty, lcid ?? Lcid.UserDefault, (ushort)kind, dispParams, out var x, excepInfo, out var errArgIndex);
		return new(hr, new(x, excepInfo, (hr == DISP_E_TYPEMISMATCH || hr == DISP_E_PARAMNOTFOUND) ? errArgIndex : null));
	}

	/// <inheritdoc cref="InvokeNoThrow(ComMemberID, ComDispatchKind, object?[]?, ComMemberID[]?, Lcid?)"/>
	public ComDispatchInvokeResult Invoke(
		ComMemberID memberId,
		ComDispatchKind flags,
		object?[]? arguments,
		ComMemberID[]? namedArguments,
		Lcid? lcid = null)
		=> InvokeNoThrow(memberId, flags, arguments, namedArguments, lcid).Value;

	public ComResult<ComDispatchInvokeResult> GetPropertyNoThrow(ComMemberID memberId, Lcid? lcid = null)
		=> InvokeNoThrow(memberId, ComDispatchKind.PropertyGet, null, null, lcid);

	public ComDispatchInvokeResult GetProperty(ComMemberID memberId, Lcid? lcid = null)
		=> GetPropertyNoThrow(memberId, lcid).Value;

	public ComResult<ComDispatchInvokeResult> PutPropertyNoThrow(ComMemberID memberId, object? value, Lcid? lcid = null)
		=> InvokeNoThrow(memberId, ComDispatchKind.PropertyPut, [value], [ComMemberID.PropertyPutID], lcid);

	public ComDispatchInvokeResult PutProperty(ComMemberID memberId, object? value, Lcid? lcid = null)
		=> PutPropertyNoThrow(memberId, value, lcid).Value;

	public ComResult<ComDispatchInvokeResult> PutPropertyRefNoThrow(ComMemberID memberId, object? value, Lcid? lcid = null)
		=> InvokeNoThrow(memberId, ComDispatchKind.PropertyPutRef, [value], [ComMemberID.PropertyPutID], lcid);

	public ComDispatchInvokeResult PutPropertyRef(ComMemberID memberId, object? value, Lcid? lcid = null)
		=> PutPropertyNoThrow(memberId, value, lcid).Value;

	//
	// ユーティリティ
	//

	public override string ToString()
	{
		if (HasTypeInfo)
			return TypeInfo.Documentation.Name;
		return base.ToString() ?? "";
	}

	// 本来は「GetPropertyValues」の方が良いかもしれませんが、
	// 取得用メソッドと紛らわしいので「PropertyGetValues」としています。
	public ImmutableDictionary<string, ComDispatchInvokeResult> PropertyGetValues
	{
		get
		{
			if (!HasTypeInfo) return ImmutableDictionary.Create<string, ComDispatchInvokeResult>();
			using var typeInfo = TypeInfo;
			var builder = ImmutableDictionary.CreateBuilder<string, ComDispatchInvokeResult>();
			foreach (var desc in TypeInfo.EnumerateFunctionDescriptions([ComInvokeKind.PropertyGet]))
			{
				builder[typeInfo.GetDocumentation(desc.MemberID).Name] = GetProperty(desc.MemberID);
			}
			return builder.ToImmutable();
		}
	}
}

/// <summary>
/// メソッドの種類。
/// </summary>
/// <remarks><c>DISPATCH_*</c></remarks>
public enum ComDispatchKind : ushort
{
	Method = 0x1,
	PropertyGet = 0x2,
	PropertyPut = 0x4,
	PropertyPutRef = 0x8,
}

/// <summary>
/// メソッドの実行結果。
/// </summary>
/// <param name="Result">戻り値。</param>
/// <param name="ExceptionInfo">例外情報。</param>
/// <param name="ErrorArgumentIndex">エラーが発生した最初の引数のインデックス。引数は逆順で与えられるので一番大きな値になります。</param>
public record struct ComDispatchInvokeResult(object? Result, ComExceptionInfo? ExceptionInfo, uint? ErrorArgumentIndex);
