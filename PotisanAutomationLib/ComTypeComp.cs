using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Com.Automation;

/// <summary>
/// コンパイラ用の型情報。
/// </summary>
/// <param name="o"></param>
public class ComTypeComp(object? o) : ComUnknownWrapperBase<ITypeComp>(o)
{
	public ComResult<(ComTypeInfo? TypeInfo, ComDescriptionKind DescKind, object? BindedObject)> BindNoThrow(string name, uint hashValue, ComInvokeKind flags = 0)
	{
		var cr = new ComResult(_obj.Bind(name, hashValue, flags, out var typeInfo, out var descKind, out var bindPtr));
		if (!cr) return new(cr.HResult, (null, 0, null));

		object? binded = descKind switch
		{
			ComDescriptionKind.FunctionDescription => new ComFunctionDescription(Marshal.PtrToStructure<FUNCDESC>(bindPtr.lpfuncdesc)),
			ComDescriptionKind.TypeComp => new ComTypeComp((ITypeComp)Marshal.GetObjectForIUnknown(bindPtr.lptcomp)),
			ComDescriptionKind.VariableDescription => new ComVariableDescription(Marshal.PtrToStructure<VARDESC>(bindPtr.lpvardesc)),
			_ => null,
		};

		if (descKind == ComDescriptionKind.TypeComp)
			Marshal.Release(bindPtr.lptcomp);

		return new(cr.HResult, (new(typeInfo), descKind, binded));
	}

	public (ComTypeInfo? TypeInfo, ComDescriptionKind DescKind, object? BindedObject) Bind(string name, uint hashValue, ComInvokeKind flags = 0)
		=> BindNoThrow(name, hashValue, flags).Value;

	public ComResult<(ComTypeInfo? TypeInfo, ComTypeComp? TypeComp)> BindTypeNoThrow(string name, uint hashValue)
		=> new(_obj.BindType(name, hashValue, out var x1, out var x2), (new(x1), new(x2)));

	public (ComTypeInfo? TypeInfo, ComTypeComp? TypeComp) BindType(string name, uint hashValue)
		=> BindTypeNoThrow(name, hashValue).Value;
}

/// <summary>
/// DESCKIND
/// </summary>
public enum ComDescriptionKind : uint
{
	None = 0,
	FunctionDescription = (None + 1),
	VariableDescription = (FunctionDescription + 1),
	TypeComp = (VariableDescription + 1),
	ImplicitAppObj = (TypeComp + 1),
}
