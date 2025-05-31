using System.Collections;

using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

public sealed class StatPropSetStorageEnumerable(object? o)
	: ComUnknownWrapperBase<IEnumSTATPROPSETSTG>(o), IEnumerable<ComStatPropSetStorage>, ICloneable
{
	public IEnumerator<ComStatPropSetStorage> GetEnumerator()
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

	public ComResult<StatPropSetStorageEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public StatPropSetStorageEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}
