using System.Diagnostics.CodeAnalysis;

namespace Potisan.Windows.Com;

/// <summary>
/// 戻り値のないCOM関数の実行結果。
/// </summary>
/// <param name="hr"></param>
public readonly struct ComResult(int hr)
{
	/// <summary>
	/// 終了コード。
	/// </summary>
	public readonly int HResult = hr;
	/// <summary>
	/// 正常終了時は真。
	/// </summary>
	public readonly bool Succeeded => HResult >= 0;

	/// <summary>
	/// エラー終了時はエラーを発生します。
	/// </summary>
	public readonly void ThrowIfError()
	{
		if (!Succeeded)
			Throw();
	}

	/// <summary>
	/// エラーコードに対応するエラーを発生します。
	/// </summary>
	[DoesNotReturn]
	public readonly void Throw()
	{
#pragma warning disable CA2201 // 予約された例外の種類を発生させません
		throw new COMException(Marshal.GetPInvokeErrorMessage(HResult), HResult);
#pragma warning restore CA2201 // 予約された例外の種類を発生させません
	}

	/// <summary>
	/// 正常終了した場合は真。
	/// </summary>
	public static implicit operator bool(in ComResult cr) => cr.Succeeded;

	/// <summary>
	/// COM関数が成功終了したら真、エラー終了したら偽を戻り値とした<c>ComResult&gt;bool&lt;</c>。
	/// </summary>
	/// <param name="hr"></param>
	/// <returns></returns>
	public static ComResult<bool> HRSuccess(int hr) => new(hr, hr == 0);
}

/// <summary>
/// 戻り値のあるCOM関数の実行結果。
/// </summary>
public readonly struct ComResult<T>(int hr, T value)
{
	/// <summary>
	/// 終了コード。
	/// </summary>
	public readonly int HResult = hr;
	/// <summary>
	/// 戻り値。
	/// </summary>
	public readonly T ValueUnchecked = value;
	/// <summary>
	/// 正常終了時は真。
	/// </summary>
	public readonly bool Succeeded => HResult >= 0;

	/// <summary>
	/// 戻り値。エラー終了時は例外を発生します。
	/// </summary>
	public readonly T Value
	{
		get
		{
			ThrowIfError();
			return ValueUnchecked;
		}
	}

	/// <summary>
	/// エラー終了時はエラーを発生します。
	/// </summary>
	public readonly void ThrowIfError()
	{
		if (!Succeeded)
			Throw();
	}

	/// <summary>
	/// エラーコードに対応するエラーを発生します。
	/// </summary>
	[DoesNotReturn]
	public readonly void Throw()
	{
#pragma warning disable CA2201 // 予約された例外の種類を発生させません
		throw new COMException(Marshal.GetPInvokeErrorMessage(HResult), HResult);
#pragma warning restore CA2201
	}

	/// <summary>
	/// 正常終了した場合は真。
	/// </summary>
	public static implicit operator bool(in ComResult<T> cr) => cr.Succeeded;

	/// <summary>
	/// 正常終了時は戻り値、エラー終了時は既定値を返します。
	/// </summary>
	/// <param name="defaultValue"></param>
	/// <returns></returns>
	public T Or(in T defaultValue) => Succeeded ? ValueUnchecked : defaultValue;

	/// <summary>
	/// 正常終了時は戻り値、エラー終了時は既定値を返します。
	/// </summary>
	/// <param name="defaultValue"></param>
	/// <returns></returns>
	public T? Or(T? defaultValue)
		=> Succeeded ? ValueUnchecked : defaultValue;

	/// <summary>
	/// 使用されません。パターンマッチングで置き換えられました。
	/// </summary>
	[Obsolete("この機能はパターンマッチングで代替可能です。")]
	public ComResult<TOutput> Invoke<TOutput>(Converter<T, TOutput> converter)
	{
		return new(HResult, converter(ValueUnchecked));
	}

	/// <summary>
	/// 使用されません。パターンマッチングで置き換えられました。
	/// </summary>
	[Obsolete("この機能はパターンマッチングで代替可能です。")]
	public ComResult<TOutput> InvokeOr<TOutput>(Converter<T, TOutput> converter, in TOutput valueIfError)
	{
		return new(HResult, Succeeded ? converter(ValueUnchecked) : valueIfError);
	}
}
