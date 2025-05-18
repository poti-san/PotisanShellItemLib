using System.Collections;

using Potisan.Windows.Text.Tsf.ComTypes;

namespace Potisan.Windows.Text.Tsf;

public class TFFunctionProviderEnumerable(object? o) : ComUnknownWrapperBase<IEnumTfFunctionProviders>(o), IEnumerable<TFFunctionProvider>, ICloneable
{
	public IEnumerator<TFFunctionProvider> GetEnumerator()
	{
		for (; ; )
		{
			var hr = _obj.Next(1, out var x, out _);
			if (hr == 1) break;
			Marshal.ThrowExceptionForHR(hr);
			yield return new(x);
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public ComResult<TFFunctionProviderEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public TFFunctionProviderEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone() => Clone();
}
