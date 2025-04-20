using System.Collections;

using PotisanShellItemLib.Shell;
using PotisanShellItemLib.Shell.ComTypes;

namespace PotisanShellItemLib.Core;

public sealed class ShellItemEnumerator : IComUnknownWrapper, IEnumerable<ShellItem>, ICloneable
{
	private readonly IEnumShellItems _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public ShellItemEnumerator(object? o)
	{
		_obj = o == null ? null! : (IEnumShellItems)o;
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

	public IEnumerator<ShellItem> GetEnumerator()
	{
		int hr;
		while ((hr = _obj.Next(1, out var x, out _)) == 0)
		{
			yield return new(x);
		}
		Marshal.ThrowExceptionForHR(hr);
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public ComResult ResetNoThrow() => new(_obj.Reset());
	public void Reset() => ResetNoThrow().ThrowIfError();

	public ComResult<ShellItemEnumerator> CloneNoThrow() => new(_obj.Clone(out var x), new(x!));
	public ShellItemEnumerator Clone() => CloneNoThrow().Value;

	object ICloneable.Clone() => Clone();
}

public sealed class ShellItem2Enumerator : IComUnknownWrapper, IEnumerable<ShellItem2>, ICloneable
{
	private readonly IEnumShellItems _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public ShellItem2Enumerator(object? o)
	{
		_obj = o == null ? null! : (o as IEnumShellItems ?? throw new InvalidCastException())!;
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

	public IEnumerator<ShellItem2> GetEnumerator()
	{
		int hr;
		while ((hr = _obj.Next(1, out var x, out _)) == 0)
		{
			yield return new(x);
		}
		Marshal.ThrowExceptionForHR(hr);
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public ComResult ResetNoThrow() => new(_obj.Reset());
	public void Reset() => ResetNoThrow().ThrowIfError();

	public ComResult<ShellItem2Enumerator> CloneNoThrow() => new(_obj.Clone(out var x), new(x!));
	public ShellItem2Enumerator Clone() => CloneNoThrow().Value;

	object ICloneable.Clone() => Clone();
}
