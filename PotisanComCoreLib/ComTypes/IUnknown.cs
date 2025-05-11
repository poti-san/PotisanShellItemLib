namespace Potisan.Windows.Com.ComTypes;

/// <summary>
/// <c>IUnknown</c> COMインターフェイスのラッパーです。型が不明なRCWオブジェクトのラップ等に使用します。
/// </summary>
[ComImport]
[Guid("00000000-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUnknown;