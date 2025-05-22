using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// プロパティバッグ。IPropertyBag COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class PropertyBag(object? o) : ComUnknownWrapperBase<IPropertyBag>(o)
{
	public ComResult<object> ReadNoThrow(string propName, ComErrorLog? errorLog = null)
		=> new(_obj.Read(propName, out var x, errorLog?.WrappedObject as IErrorLog), x);

	public object Read(string propName, ComErrorLog? errorLog = null)
		=> ReadNoThrow(propName, errorLog).Value;

	public ComResult WriteNoThrow(string propName, object? x)
		=> new(_obj.Write(propName, x!));

	public void Write(string propName, object? x)
		=> WriteNoThrow(propName, x).ThrowIfError();
}
