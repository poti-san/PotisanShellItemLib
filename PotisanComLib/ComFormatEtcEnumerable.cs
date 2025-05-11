using System.Collections;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// FormatEtc構造体列挙子。IEnumFORMATETC COMインターフェイスのラッパーです。
/// </summary>
public class ComFormatEtcEnumerable(object? o) :
	ComUnknownWrapperBase<IEnumFORMATETC>(o),
	IEnumerable<ComFormatEtc>,
	ICloneable
{
	public IEnumerator<ComFormatEtc> GetEnumerator()
	{
		for (; ; )
		{
			var x = new ComFormatEtc();
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

	public ComResult<MonikerEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public MonikerEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}