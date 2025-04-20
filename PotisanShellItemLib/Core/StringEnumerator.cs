using System.Collections;

using PotisanShellItemLib.Core.ComTypes;

namespace PotisanShellItemLib.Core;

/// <summary>
/// 文字列の列挙子。IEnumString COMインターフェイスのラッパーです。
/// </summary>
public sealed class StringEnumerator : IComUnknownWrapper, IEnumerable<string>, ICloneable, IDisposable
{
	private readonly IEnumString _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public StringEnumerator(object? o)
	{
		_obj = o == null ? null! : (o as IEnumString)!;
	}

	/// <inheritdoc/>
	public object? WrappedObject => _obj;

	/// <inheritdoc/>
	public void Dispose()
	{
		if (_obj != null)
			Marshal.FinalReleaseComObject(_obj);
		GC.SuppressFinalize(this);
	}

	public IEnumerator<string> GetEnumerator()
	{
		int hr;
		while ((hr = _obj.Next(1, out var s, out _)) == 0)
		{
			yield return s;
		}
		Marshal.ThrowExceptionForHR(hr);
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public ComResult ResetNoThrow() => new(_obj.Reset());
	public void Reset() => ResetNoThrow().ThrowIfError();

	public ComResult<StringEnumerator> CloneNoThrow() => new(_obj.Clone(out var x), new(x));
	public StringEnumerator Clone() => CloneNoThrow().Value;

	object ICloneable.Clone() => Clone();
}
