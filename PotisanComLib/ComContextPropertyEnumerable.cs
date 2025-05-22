using System.Collections;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMコンテキストプロパティの列挙機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IEnumContextProps</c> COMインターフェイスのラッパーです。
/// </remarks>
public class ComContextPropertyEnumerable(object? o) : ComUnknownWrapperBase<IEnumContextProps>(o), IReadOnlyCollection<ComContextProperty>, ICloneable
{
	public IEnumerator<ComContextProperty> GetEnumerator()
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

	public ComResult<uint> CountNoThrow
		=> new(_obj.Count(out var x), x);

	public uint Count
		=> CountNoThrow.Value;

	int IReadOnlyCollection<ComContextProperty>.Count
		=> checked((int)Count);

	public ComResult<ComContextPropertyEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ComContextPropertyEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}
