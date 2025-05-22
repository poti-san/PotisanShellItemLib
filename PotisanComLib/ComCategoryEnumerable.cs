using System.Collections;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMのカテゴリ列挙機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IEnumCATEGORYINFO</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class ComCategoryEnumerable(object? o) :
	ComUnknownWrapperBase<IEnumCATEGORYINFO>(o),
	IEnumerable<ComCategoryInfo>,
	ICloneable
{
	public IEnumerator<ComCategoryInfo> GetEnumerator()
	{
		for (; ; )
		{
			var info = new ComCategoryInfo();
			var hr = _obj.Next(1, info, out _);
			if (hr == 1) break;
			Marshal.ThrowExceptionForHR(hr);
			yield return info;
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public ComResult ResetNoThrow()
		=> new(_obj.Reset());

	public void Reset()
		=> ResetNoThrow().ThrowIfError();

	public ComResult<ComCategoryEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ComCategoryEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone() => Clone();
}
