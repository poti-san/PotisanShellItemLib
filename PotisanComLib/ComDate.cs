namespace Potisan.Windows.Com;

/// <summary>
/// <c>double</c>形式の日付表現です。
/// </summary>
/// <param name="Value">
/// 1899年12月30日00:00からの経過日時。
/// 整数部分は経過日、小数部分は24時間における経過時間割合を示します。
/// </param>
/// <remarks>
/// <c>DATE</c>型に対応します。
/// </remarks>
public record struct ComDate(double Value)
{
	public readonly DateTime ToDateTime()
		=> DateTime.FromOADate(Value);

	public ComDate(DateTime dt) : this(dt.ToOADate()) { }
}
