using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public class ComContext(object? o) : ComUnknownWrapperBase<IContext>(o)
{
	public static ComResult<ComContext> GetCurrentContextNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int CoGetContextToken(out nint pToken);

		var hr = CoGetContextToken(out var x);
		return new(hr, hr == CommonHResults.SOK ? new(Marshal.GetObjectForIUnknown(x)) : null!);
	}

	public static ComContext GetCurrentContext()
		=> GetCurrentContextNoThrow().Value;

	public ComResult SetPropertyNoThrow(in Guid policyId, object value)
		=> new(_obj.SetProperty(policyId, 0, value));

	public void SetProperty(in Guid policyId, object value)
		=> SetPropertyNoThrow(policyId, value).ThrowIfError();

	public ComResult RemovePropertyNoThrow(in Guid policyId)
		=> new(_obj.RemoveProperty(policyId));

	public void RemoveProperty(in Guid policyId)
		=> RemovePropertyNoThrow(policyId).ThrowIfError();

	public ComResult<object> GetPropertyNoThrow(in Guid policyId)
		=> new(_obj.GetProperty(policyId, out _, out var x), x);

	public object GetProperty(in Guid policyId)
		=> GetPropertyNoThrow(policyId).Value;

	public ComResult<ComContextPropertyEnumerable> EnumerableNoThrow
		=> new(_obj.EnumContextProps(out var x), new(x));

	public ComContextPropertyEnumerable Enumerable
		=> EnumerableNoThrow.Value;

	public ImmutableArray<ComContextProperty> Items
		=> [.. Enumerable];
}

/// <summary>
/// <c>ContextProperty</c>構造体に対応します。
/// </summary>
public readonly struct ComContextProperty
{
	public readonly Guid PolicyId;
	// 現在は使用されないので隠します。
	private readonly uint Flags;
	[MarshalAs(UnmanagedType.IUnknown)]
	public readonly object Value;
}
