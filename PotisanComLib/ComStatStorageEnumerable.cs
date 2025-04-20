using System.Collections;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public class ComStatStorageEnumerable(object? o) :
	ComUnknownWrapperBase<IEnumSTATSTG>(o),
	IEnumerable<ComStatStorage>,
	ICloneable
{
	public IEnumerator<ComStatStorage> GetEnumerator()
	{
		for (; ; )
		{
			var hr = _obj.Next(1, out var x, out _);
			if (hr != 0)
			{
				if (hr == 1)
					break;
				Marshal.ThrowExceptionForHR(hr);
			}
			yield return x.GetAndFree();
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult ResetNoThrow()
		=> new(_obj.Reset());

	public void Reset()
		=> ResetNoThrow().ThrowIfError();

	public ComResult<ComStatStorageEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ComStatStorageEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}
