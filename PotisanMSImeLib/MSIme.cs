using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.MSIme.ComTypes;

namespace Potisan.Windows.MSIme;

/// <summary>
/// Microsoft IMEのクラスインスタンス。
/// ここから他のCOMインターフェイスを取得できます。
/// </summary>
public sealed class MSIme(object? o) : ComUnknownWrapperBase<IUnknown>(o)
{
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

	private static ComResult<MSIme> CreateLangNoThrow(string progId)
	{
		// 分かりにくい原因なのでアサートを発生させます。
		Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA,
			"シングルスレッドモデルでのみ正常に動作します。");

		var clsid = ComGuidHelper.ProgIDToClsid(progId);
		return ComHelper.CreateInstanceNoThrow<MSIme, IUnknown>(clsid, ComClassContext.InProcServer);
	}

	public static ComResult<MSIme> CreateImeJapanNoThrow()
		=> CreateLangNoThrow("MSIME.Japan");
	public static ComResult<MSIme> CreateImeKoreaNoThrow()
		=> CreateLangNoThrow("MSIME.Korea");
	public static ComResult<MSIme> CreateImeChinaNoThrow()
		=> CreateLangNoThrow("MSIME.China");
	public static ComResult<MSIme> CreateImeTaiwanNoThrow()
		=> CreateLangNoThrow("MSIME.Taiwan");

	public static MSIme CreateImeJapan()
		=> CreateLangNoThrow("MSIME.Japan").Value;
	public static MSIme CreateImeKorea()
		=> CreateLangNoThrow("MSIME.Korea").Value;
	public static MSIme CreateImeChina()
		=> CreateLangNoThrow("MSIME.China").Value;
	public static MSIme CreateImeTaiwan()
		=> CreateLangNoThrow("MSIME.Taiwan").Value;

	public FECommon? AsFECommon
		=> this.As<FECommon, IFECommon>();

	public FELanguage? AsFELanguage
		=> this.As<FELanguage, IFELanguage>();

	public FEDictionary? AsFEDictionary
		=> this.As<FEDictionary, IFEDictionary>();
}
