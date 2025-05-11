using System.Collections;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public sealed class ComGuidEnumerable(object? o) :
	ComUnknownWrapperBase<IEnumGUID>(o),
	IEnumerable<Guid>,
	ICloneable
{
	public IEnumerator<Guid> GetEnumerator()
	{
		for (; ; )
		{
			var hr = _obj.Next(1, out var x, out _);
			if (hr == 1) break;
			Marshal.ThrowExceptionForHR(hr);
			yield return x;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult ResetNoThrow()
		=> new(_obj.Reset());

	public void Reset()
		=> ResetNoThrow().ThrowIfError();

	public ComResult<ComGuidEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ComGuidEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}
