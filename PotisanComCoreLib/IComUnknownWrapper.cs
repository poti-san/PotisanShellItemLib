namespace Potisan.Windows.Com;

/// <summary>
/// COMインターフェイスラッパーのインターフェイス。
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>
/// COMインターフェイスを隠蔽可能にするため、
/// このインターフェイスは管理するCOMインターフェイスの型を受け取りません。
/// 実装クラスはRCWインスタンスを受け取るコンストラクタも実装してください。
/// </item>
/// <item>
/// 実装クラスのコンストラクタではCOMインターフェイスの型変換失敗で例外を送出してください。
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

	public static ComResult<TWrapper> Wrap<TWrapper>(int hr, object? o)
		where TWrapper : IComUnknownWrapper
	{
		return new(hr, (TWrapper)typeof(TWrapper).GetConstructor([typeof(object)])!.Invoke([o]));
	}

	public static ComResult<TWrapper> Wrap<TWrapper>(ComResult<object> cr)
		where TWrapper : IComUnknownWrapper
	{
		return Wrap<TWrapper>(cr.HResult, cr.ValueUnchecked);
	}

	public static ComResult<TWrapperTo> Wrap<TWrapperTo, TWrapperFrom>(ComResult<TWrapperFrom> cr)
		where TWrapperTo : IComUnknownWrapper
		where TWrapperFrom : IComUnknownWrapper
	{
		return Wrap<TWrapperTo>(cr.HResult, cr.ValueUnchecked.WrappedObject);
	}

	public static ComResult<TWrapper> Casted<TWrapper, TInterface>(object o)
		where TWrapper : IComUnknownWrapper
		where TInterface : class
	{
		var p = o as TInterface;
		return Wrap<TWrapper>(p != null ? 0 : -1, p);
	}
}

/// <summary>
/// 公開COMインターフェイスを内部利用するCOMインターフェイスラッパー基底クラス。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
/// <remarks>
/// <list type="bullet">
/// <item>
/// COMインターフェイス型が公開されている場合に<c>WrappedObject</c>や<c>Dispose</c>を実装するクラスです。
/// 型を公開しない場合は<see cref="IComUnknownWrapper"/>を使用してください。
/// </item>
/// <item>
/// クラスの継承により扱う方が変わる可能性があるため、クラスは扱う型の情報を公開しません。
/// </item>
/// </list>
/// </remarks>
public class ComUnknownWrapperBase<TIUnknown>(object? o) : IComUnknownWrapper
	where TIUnknown : class
{
	protected readonly TIUnknown _obj = o == null ? null! : (TIUnknown)o;

	/// <inheritdoc/>
	public object? WrappedObject => _obj;

	/// <inheritdoc/>
	public virtual void Dispose()
	{
		if (_obj != null)
			Marshal.FinalReleaseComObject(_obj);
		GC.SuppressFinalize(this);
	}
}