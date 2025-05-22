using System.Collections;

using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// シェルアイテム<see cref="ShellItem"/>の列挙子。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <para><see cref="ShellItem2"/>を列挙する場合は<see cref="ShellItem2Enumerable"/>を使用します。</para>
/// <para><c>IEnumShellItems</c> COMインターフェイスのラッパーです。</para>
/// </remarks>
public sealed class ShellItemEnumerable(object? o) : ComUnknownWrapperBase<IEnumShellItems>(o), IEnumerable<ShellItem>, ICloneable
{
	public IEnumerator<ShellItem> GetEnumerator()
	{
		int hr;
		while ((hr = _obj.Next(1, out var x, out _)) == 0)
		{
			yield return new(x);
		}
		Marshal.ThrowExceptionForHR(hr);
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult ResetNoThrow()
		=> new(_obj.Reset());

	public void Reset()
		=> ResetNoThrow().ThrowIfError();

	public ComResult<ShellItemEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ShellItemEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}

/// <summary>
/// シェルアイテム(ShellItem2)の列挙子。IEnumShellItems COMインターフェイスのラッパーです。
/// </summary>
/// <remarks>
/// <see cref="ShellItem"/>を列挙する場合は<see cref="ShellItemEnumerable"/>を使用します。
/// </remarks>
public sealed class ShellItem2Enumerable(object? o) : ComUnknownWrapperBase<IEnumShellItems>(o), IEnumerable<ShellItem2>, ICloneable
{
	public IEnumerator<ShellItem2> GetEnumerator()
	{
		int hr;
		while ((hr = _obj.Next(1, out var x, out _)) == 0)
		{
			yield return new(x);
		}
		Marshal.ThrowExceptionForHR(hr);
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult ResetNoThrow()
		=> new(_obj.Reset());

	public void Reset()
		=> ResetNoThrow().ThrowIfError();

	public ComResult<ShellItem2Enumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public ShellItem2Enumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}
