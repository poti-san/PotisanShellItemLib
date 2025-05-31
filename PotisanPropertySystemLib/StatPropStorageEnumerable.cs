using System.Collections;

using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

public sealed class StatPropStorageEnumerable(object? o)
	: ComUnknownWrapperBase<IEnumSTATPROPSTG>(o), IEnumerable<ComStatPropStorage>, ICloneable
{
	public IEnumerator<ComStatPropStorage> GetEnumerator()
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

	public ComResult<StatPropStorageEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public StatPropStorageEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}
