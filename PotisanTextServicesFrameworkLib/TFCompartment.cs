using Potisan.Windows.Text.Tsf.ComTypes;

namespace Potisan.Windows.Text.Tsf;

public class TFCompartment(object? o) : ComUnknownWrapperBase<ITfCompartment>(o)
{
	/// <summary>
	/// 有効な型は<see cref="VarType.I4"/>、<see cref="VarType.BStr"/>、<see cref="VarType.Unknown"/>のみです。
	/// </summary>
	public ComResult SetValueNoThrow(TFClientID id, object value)
		=> new(_obj.SetValue(id, value));

	public void SetValue(TFClientID id, object value)
		=> SetValueNoThrow(id, value).ThrowIfError();

	public ComResult<object> GetValueNoThrow()
		=> new(_obj.GetValue(out var x), x);

	public object GetValue()
		=> GetValueNoThrow().Value;
}
