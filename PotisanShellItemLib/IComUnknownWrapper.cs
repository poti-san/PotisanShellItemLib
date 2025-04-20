namespace PotisanShellItemLib;

/// <summary>
/// COMインターフェイスラッパーのインターフェイス。
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>
/// COMインターフェイスを隠蔽可能にするため、
/// このクラスは管理するCOMインターフェイスの型を受け取りません。
/// 実装クラスはRCWインスタンスを受け取るコンストラクタも実装してください。
/// </item>
/// <item>
/// 継承クラスのコンストラクタではCOMインターフェイスの型変換失敗で例外を送出してください。
/// 通常、COMインターフェイスの取得時点で失敗して<c>null</c>となるため、
/// 型変換失敗はライブラリ設計ミスの可能性があります。
/// </item>
/// </list>
/// </remarks>
public interface IComUnknownWrapper : IDisposable
{
	/// <summary>
	/// 保持しているRCWインスタンス。
	/// </summary>
	public object? WrappedObject { get; }

	/// <summary>
	/// 保持するCOMインターフェイスを解放します。
	/// 呼び出し後はCOMインターフェイスを使用するメソッドが使用不可能になります。
	/// </summary>
	public new void Dispose();
}
