using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// サービスIDの取得機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <para><c>IGetServiceIds</c> COMインターフェイスのラッパーです。</para>
/// <para>定義が存在するので実装しますが、使用は見つけられていません。</para>
/// </remarks>
public class ComGetServiceIds(object? o) : ComUnknownWrapperBase<IGetServiceIds>(o)
{
	public ComResult<Guid[]> GetServiceIDsNoThrow()
		=> new(_obj.GetServiceIds(out _, out var x), x);

	public Guid[] GetServiceIDs()
		=> GetServiceIDsNoThrow().Value;
}
