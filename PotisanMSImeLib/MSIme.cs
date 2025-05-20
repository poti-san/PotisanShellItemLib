using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.MSIme.ComTypes;

namespace Potisan.Windows.MSIme;

/// <summary>
/// Microsoft IMEのクラスインスタンスです。
/// ここから他のCOMインターフェイスを取得できます。
/// </summary>
public sealed class MSIme(object? o) : ComUnknownWrapperBase<IUnknown>(o)
{
	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private delegate bool CreateIFEDictionaryInstanceType([MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	public static ComResult<MSIme> CreateImeJpNoThrow()
	{
		// 分かりにくい原因なのでアサートを発生させます。
		Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA,
			"シングルスレッドモデルでのみ正常に動作します。");

		// {6a91029e-aa49-471b-aee7-7d332785660d}
		Guid CLSID_VERSION_DEPENDENT_MSIME_JAPANESE = new(0x6a91029e, 0xaa49, 0x471b, 0xae, 0xe7, 0x7d, 0x33, 0x27, 0x85, 0x66, 0x0d);

		return ComHelper.CreateInstanceNoThrow<MSIme, IUnknown>(CLSID_VERSION_DEPENDENT_MSIME_JAPANESE, ComClassContext.InProcServer);
	}

	public static MSIme CreateImeJp()
		=> CreateImeJpNoThrow().Value;

	public ComResult<FECommon> AsFECommonNoThrow
		=> IComUnknownWrapper.Casted<FECommon, IFECommon>(this);

	public FECommon AsFECommon
		=> AsFECommonNoThrow.Value;

	public ComResult<FELanguage> AsFELanguageNoThrow
		=> IComUnknownWrapper.Casted<FELanguage, IFELanguage>(this);

	public FELanguage AsFELanguage
		=> AsFELanguageNoThrow.Value;

	public ComResult<FEDictionary> AsFEDictionaryNoThrow
		=> IComUnknownWrapper.Casted<FEDictionary, IFEDictionary>(this);

	public FEDictionary AsFEDictionary
		=> AsFEDictionaryNoThrow.Value;
}
