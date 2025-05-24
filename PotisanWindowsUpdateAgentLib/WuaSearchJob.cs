using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaSearchJob(object? o) : ComUnknownWrapperBase<ISearchJob>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	/// <summary>
	/// 作成時に指定したステートオブジェクトを取得します。
	/// </summary>
	public ComResult<object?> AsyncStateNoThrow
		=> new(_obj.get_AsyncState(out var x), x);

	/// <inheritdoc cref="AsyncStateNoThrow"/>
	public object? AsyncState
		=> AsyncStateNoThrow.Value;

	public ComResult<bool> IsCompletedNoThrow
		=> new(_obj.get_IsCompleted(out var x), x);

	public bool IsCompleted
		=> IsCompletedNoThrow.Value;

	public ComResult CleanUpNoThrow()
		=> new(_obj.CleanUp());

	public void CleanUp()
		=> CleanUpNoThrow().ThrowIfError();

	public ComResult RequestAbortNoThrow()
		=> new(_obj.RequestAbort());

	public void RequestAbort()
		=> RequestAbortNoThrow().ThrowIfError();
}
