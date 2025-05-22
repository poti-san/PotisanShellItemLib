using System.Collections;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// ComStatData構造体列挙子。
/// </summary>
/// <remarks>
/// <c>IEnumSTATDATA</c> COMインターフェイスのラッパーです。
/// </remarks>
public class ComStatDataEnumerable(object? o) :
	ComUnknownWrapperBase<IEnumSTATDATA>(o),
	IEnumerable<ComStatData>,
	ICloneable
{
	public IEnumerator<ComStatData> GetEnumerator()
	{
		for (; ; )
		{
			var x = new ComStatData();
			var hr = _obj.Next(1, x, out _);
			if (hr != 0)
			{
				if (hr == 1)
					break;
				Marshal.ThrowExceptionForHR(hr);
			}
			yield return x;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult ResetNoThrow()
		=> new(_obj.Reset());

	public void Reset()
		=> ResetNoThrow().ThrowIfError();

	public ComResult<ComStatDataEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ComStatDataEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}