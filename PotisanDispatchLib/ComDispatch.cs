using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

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

	public ComResult<ComDispatchInvokeResult> InvokeNoThrow(
		ComMemberID memberId,
		ComDispatchKind flags,
		object?[]? arguments,
		ComMemberID[]? namedArguments,
		Lcid? lcid = null)
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
		return new(_obj.Invoke(memberId, Guid.Empty, lcid ?? Lcid.UserDefault, (ushort)flags, dispParams, out var x, excepInfo, out var errArgIndex),
			new(x, excepInfo, errArgIndex));
	}

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
/// DISPATCH_*
/// </summary>
public enum ComDispatchKind : ushort
{
	Method = 0x1,
	PropertyGet = 0x2,
	PropertyPut = 0x4,
	PropertyPutRef = 0x8,
}

public record struct ComDispatchInvokeResult(object? Result, ComExceptionInfo? ExceptionInfo, uint ErrorArgumentIndex);
