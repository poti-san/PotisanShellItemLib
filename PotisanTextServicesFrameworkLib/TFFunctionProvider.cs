using Potisan.Windows.Text.Tsf.ComTypes;

namespace Potisan.Windows.Text.Tsf;

public class TFFunctionProvider(object? o) : ComUnknownWrapperBase<ITfFunctionProvider>(o)
{
	public ComResult<Guid> TypeNoThrow
		=> new(_obj.GetType(out var x), x);

	public Guid Type
		=> TypeNoThrow.Value;

	public ComResult<string> DescriptionNoThrow
		=> new(_obj.GetDescription(out var x), x!);

	public string Description
		=> DescriptionNoThrow.Value;

	public ComResult<object> GetFunctionNoThrow(in Guid guid, in Guid iid)
		=> new(_obj.GetFunction(guid, iid, out var x), x!);

	public object GetFunction(in Guid guid, in Guid iid)
		=> GetFunctionNoThrow(guid, iid).Value;

	public ComResult<TWrapper> GetFunctionNoThrow<TWrapper, TInterface>(in Guid guid)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(GetFunctionNoThrow(guid, typeof(TInterface).GUID));

	public TWrapper GetFunction<TWrapper, TInterface>(in Guid guid)
		where TWrapper : IComUnknownWrapper
		=> GetFunctionNoThrow<TWrapper, TInterface>(guid).Value;
}
