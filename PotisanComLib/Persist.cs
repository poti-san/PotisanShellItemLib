namespace Potisan.Windows.Com;

using ComTypes;

/// <summary>
/// 永続化基底クラス。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IPersist</c> COMインターフェイスのラッパーです。
/// </remarks>
public class Persist(object? o) : ComUnknownWrapperBase<IPersist>(o)
{
	public ComResult<Guid> ClassIDNoThrow
		=> new(_obj.GetClassID(out var x), x);

	public Guid ClassID
		=> ClassIDNoThrow.Value;
}