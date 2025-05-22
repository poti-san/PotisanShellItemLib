using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// 任意のCOMインターフェイス。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IUnknown</c> COMインターフェイスのラッパーです。
/// </remarks>
public class ComUnknown(object? o) : ComUnknownWrapperBase<IUnknown>(o);