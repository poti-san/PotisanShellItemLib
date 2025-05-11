using System.Collections;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// モニカ列挙子。IEnumMoniker COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class MonikerEnumerable(object? o) : ComUnknownWrapperBase<IEnumMoniker>(o), IEnumerable<Moniker>, ICloneable
{
	public IEnumerator<Moniker> GetEnumerator()
	{
		int hr;
		while ((hr = _obj.Next(1, out var s, out _)) == 0)
			yield return new Moniker(s);
		Marshal.ThrowExceptionForHR(hr);
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult ResetNoThrow()
		=> new(_obj.Reset());

	public void Reset()
		=> ResetNoThrow().ThrowIfError();

	public ComResult<MonikerEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public MonikerEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}
