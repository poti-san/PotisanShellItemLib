using System.Collections;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// 文字列列挙子。IEnumString COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public sealed class ComStringEnumerable(object? o) : ComUnknownWrapperBase<IEnumString>(o), IEnumerable<string>, ICloneable
{
	public IEnumerator<string> GetEnumerator()
	{
		int hr;
		while ((hr = _obj.Next(1, out var s, out _)) == 0)
		{
			yield return s;
		}
		Marshal.ThrowExceptionForHR(hr);
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult ResetNoThrow()
		=> new(_obj.Reset());

	public void Reset()
		=> ResetNoThrow().ThrowIfError();

	public ComResult<ComStringEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ComStringEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();

	/// <summary>
	/// <c>IEnumString</c> COMインターフェイスの保持する文字列を<c>string[]</c>として返します。
	/// <c><see cref="ComResult{T}"/></c>が失敗を保持する場合は<c>null</c>を返します。
	/// </summary>
	public static ComResult<string[]> GetStrings(ComResult<IEnumString> enumstr)
	{
		if (!enumstr) return new(enumstr.HResult, null!);
		using var strEnum = new ComStringEnumerable(enumstr);
		return new(enumstr.HResult, [.. strEnum]);
	}

	/// <summary>
	/// <c>IEnumString</c> COMインターフェイスの保持する文字列を<c>string[]</c>として返します。
	/// <paramref name="hr"/>が失敗を保持する場合は<c>null</c>を返します。
	/// </summary>
	public static ComResult<string[]> GetStrings(int hr, IEnumString enumstr)
	{
		if (hr < 0) return new(hr, null!);
		using var strEnum = new ComStringEnumerable(enumstr);
		return new(hr, [.. strEnum]);
	}
}
