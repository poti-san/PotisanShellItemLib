namespace Potisan.Windows.Com;

using ComTypes;

/// <summary>
/// 永続化基底クラス。IPersist COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class Persist(object? o) : ComUnknownWrapperBase<IPersist>(o)
{
	public ComResult<Guid> ClassIDNoThrow
		=> new(_obj.GetClassID(out var x), x);

	public Guid ClassID
		=> ClassIDNoThrow.Value;
}