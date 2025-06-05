using System.Collections;

using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Com.Automation;

/// <summary>
/// <c>object</c>型（COMの<c>VARIANT</c>型）列挙オブジェクト。
/// </summary>
/// <param name="o"></param>
public sealed class ComVariantEnumerable(object? o) : ComUnknownWrapperBase<IEnumVARIANT>(o), IEnumerable<object>, ICloneable
{
	public IEnumerator<object> GetEnumerator()
	{
		for (; ; )
		{
			var hr = _obj.Next(1, out var x, out _);
			if (hr == 1) break;
			Marshal.ThrowExceptionForHR(hr);
			yield return x!;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult<ComVariantEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ComVariantEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone() => Clone();
}
