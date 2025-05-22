using System.Text;

using Potisan.Windows.MSIme.ComTypes;

namespace Potisan.Windows.MSIme;

/// <summary>
/// ユーザー辞書。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// <code>
/// <![CDATA[using Potisan.Windows.MSIme;
/// 
/// Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
/// Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
///
/// var msIme = MSIme.CreateImeJp();
/// var dict = msIme.AsFEDictionary;
///
/// // デスクトップに「test.dic」を作成します。
/// // 存在する場合は上書きされることに注意してください。
/// dict.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.dic"), new (1, "UserDict1", "Test Dictionary", ""));
/// dict.RegisterWord(ImeWordRegistringPlace.Tail, "よみかた", "語句", ImeWordJPos.MeishiFutsu, ImeUserComment.StringUnicode, "Comment");
/// dict.Close();
///
/// Console.WriteLine();]]>
/// </code>
/// </example>
public class FEDictionary(object? o) : ComUnknownWrapperBase<IFEDictionary>(o)
{
	[UnmanagedFunctionPointer(CallingConvention.Winapi)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private delegate bool CreateIFEDictionaryInstanceType([MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	public ComResult<ImeUserDictionaryFileHeader> OpenNoThrow(string dictionaryPath)
		=> new(_obj.Open(dictionaryPath, out var x), x);

	public ImeUserDictionaryFileHeader Open(string dictionaryPath)
		=> OpenNoThrow(dictionaryPath).Value;

	public ComResult CloseNoThrow()
		=> new(_obj.Close());

	public void Close()
		=> CloseNoThrow().ThrowIfError();

	public ComResult<(ImeUserDictionaryFileHeader Header, ImeDictionaryFormat Format, int Type)> GetHeaderNoThrow(string dictionaryPath)
		=> new(_obj.GetHeader(dictionaryPath, out var x1, out var x2, out var x3), (x1, x2, x3));

	public (ImeUserDictionaryFileHeader Header, ImeDictionaryFormat Format, int Type) GetHeader(string dictionaryPath)
		=> GetHeaderNoThrow(dictionaryPath).Value;

	public ComResult DisplayPropertyNoThrow(nint windowHandle = 0)
		=> new(_obj.DisplayProperty(windowHandle));

	public void DisplayProperty(nint windowHandle = 0)
		=> DisplayPropertyNoThrow(windowHandle).ThrowIfError();

	//[PreserveSig]
	//int GetPosTable(
	//	out nint/*POSTBL*/ prgPosTbl,
	//	out int pcPosTbl);

	//[PreserveSig]
	//int GetWords(
	//	[MarshalAs(UnmanagedType.LPWStr)] string pwchFirst,
	//	[MarshalAs(UnmanagedType.LPWStr)] string pwchLast,
	//	[MarshalAs(UnmanagedType.LPWStr)] string pwchDisplay,
	//	uint ulPos,
	//	uint ulSelect,
	//	uint ulWordSrc,
	//	[MarshalAs(UnmanagedType.I2)] ref byte pchBuffer,  // IMEWRD
	//	uint cbBuffer,
	//	out uint pcWrd);

	//[PreserveSig]
	//int NextWords(
	//	ref byte pchBuffer,
	//	uint cbBuffer,
	//	out uint pcWrd);

	public ComResult CreateNoThrow(string dictionaryPath, in ImeUserDictionaryFileHeader header)
		=> new(_obj.Create(dictionaryPath, header));

	public void Create(string dictionaryPath, in ImeUserDictionaryFileHeader header)
		=> CreateNoThrow(dictionaryPath, header).ThrowIfError();

	public ComResult CreateNoThrow(string dictionaryPath, ushort version, string title, string description, string copyright)
		=> CreateNoThrow(dictionaryPath, new(version, title, description, copyright));

	public void Create(string dictionaryPath, ushort version, string title, string description, string copyright)
		=> CreateNoThrow(dictionaryPath, version, title, description, copyright).ThrowIfError();

	public ComResult SetHeaderNoThrow(in ImeUserDictionaryFileHeader header)
		=> new(_obj.SetHeader(header));

	public void SetHeader(in ImeUserDictionaryFileHeader header)
		=> SetHeaderNoThrow(header).ThrowIfError();

	public ComResult SetHeaderNoThrow(ushort version, string title, string description, string copyright)
		=> SetHeaderNoThrow(new(version, title, description, copyright));

	public void SetHeader(ushort version, string title, string description, string copyright)
		=> SetHeaderNoThrow(version, title, description, copyright).ThrowIfError();

	//[PreserveSig]
	//int ExistWord(
	//	IMEWRD pwrd);

	//[PreserveSig]
	//int ExistDependency(
	//	IMEDP pdp);

	/// <summary>
	/// 単語を登録します。
	/// </summary>
	public ComResult RegisterWordNoThrow(ImeWordRegistringPlace reg, ImeWord word)
		=> new(_obj.RegisterWord(reg, word));

	/// <inheritdoc cref="RegisterWordNoThrow(ImeWordRegistringPlace, ImeWord)"/>
	public void RegisterWord(ImeWordRegistringPlace reg, ImeWord word)
		=> RegisterWordNoThrow(reg, word).ThrowIfError();

	/// <summary>
	/// 単語を登録します。
	/// </summary>
	/// <param name="reg">追加位置または削除。</param>
	/// <param name="reading">読み。</param>
	/// <param name="display">単語。</param>
	/// <param name="jpos">品詞。</param>
	/// <param name="commentType">コメントの種類。</param>
	/// <param name="comment">コメント。</param>
	public ComResult RegisterWordNoThrow(ImeWordRegistringPlace reg, string reading, string display, ImeWordJPos jpos, ImeUserComment commentType, object? comment)
	{
		using var wrd = new ImeWord(reading, display, jpos, commentType, comment);
		return new(_obj.RegisterWord(reg, wrd));
	}

	/// <inheritdoc cref="RegisterWordNoThrow(ImeWordRegistringPlace, string, string, ImeWordJPos, ImeUserComment, object?)"/>
	public void RegisterWord(ImeWordRegistringPlace reg, string reading, string display, ImeWordJPos jpos, ImeUserComment commentType, object? comment)
		=> RegisterWordNoThrow(reg, reading, display, jpos, commentType, comment).ThrowIfError();

	//[PreserveSig]
	//int RegisterDependency(
	//	ImeWordRegistringPlace reg,
	//	in IMEDP pdp);

	//[PreserveSig]
	//int GetDependencies(
	//	[MarshalAs(UnmanagedType.LPWStr)] string pwchKakariReading,
	//	[MarshalAs(UnmanagedType.LPWStr)] string pwchKakariDisplay,
	//	uint ulKakariPos,
	//	[MarshalAs(UnmanagedType.LPWStr)] string pwchUkeReading,
	//	[MarshalAs(UnmanagedType.LPWStr)] string pwchUkeDisplay,
	//	uint ulUkePos,
	//	ImeRelationType jrel,
	//	uint ulWordSrc,
	//	//(in/out) buffer for storing array of IMEDP
	//	ref byte pchBuffer,
	//	uint cbBuffer,
	//	//(out) count of IMEDP's returned);
	//	out uint pcdp);

	//[PreserveSig]
	//int NextDependencies(
	//	//(in/out) buffer for storing array of IMEDP
	//	ref byte pchBuffer,
	//	//(in) size of buffer in bytes
	//	uint cbBuffer,
	//	//(out) count of IMEDP's retrieved
	//	out uint pcDp);

	//[PreserveSig]
	//int ConvertFromOldMSIME(
	//	//(in) old ime user dictionary path
	//	[MarshalAs(UnmanagedType.LPStr)] string pchDic,
	//	//(in) pointer to log function
	//	ImeLogFunc pfnLog,
	//	//(in) word registration info
	//	ImeWordRegistringPlace reg);

	public ComResult ConvertFromUserToSystemNoThrow()
		=> new(_obj.ConvertFromUserToSys());

	public void ConvertFromUserToSystem()
		=> ConvertFromUserToSystemNoThrow().ThrowIfError();
}

/// <summary>
/// IMEREG
/// </summary>
public enum ImeWordRegistringPlace
{
	Head,
	Tail,
	Del,
}

/// <summary>
/// IMEFMT
/// </summary>
public enum ImeDictionaryFormat
{
	Unknown,
	MSIme2BinSystem,
	MSIme2BinUser,
	MSIme2TextUser,
	MSIme95BinSystem,
	MSIme95BinUser,
	MSIme95TextUser,
	MSIme97BinSystem,
	MSIme97BinUser,
	MSIme97TextUser,
	MSIme98BinSystem,
	MSIme98BinUser,
	MSIme98TextUser,
	ActiveDictionary,
	Atok9,
	Atok10,
	NecAI,
	WXII,
	WXIII,
	Vje20,
	MSIme98SystemCE,
	MSImeBinSystem,
	MSImeBinUser,
	MSImeTextUser,
	PIme2BinUser,
	PIme2BinSystem,
	PIme2BinStandardSystem,
}

/// <summary>
/// IMEUCT
/// </summary>
public enum ImeUserComment
{
	None,
	StringSJis,
	StringUnicode,
	UserDefined,
}

/// <summary>
/// IMEREL
/// </summary>
public enum ImeRelationType
{
	None,
	No,
	Ga,
	Wo,
	Ni,
	De,
	Yori,
	Kara,
	Made,
	He,
	To,
	Ideom,
	FukuYougen,
	KeiyouYougen,
	Keidou1Yougen,
	Keidou2Yougen,
	Taigen,
	Yougen,
	TentaiMei,
	Rensou,
	KeiyouToYougen,
	KeiyouTaruYougen,
	Unknown1,
	Unknown2,
	All,
}

/// <summary>
/// IMEの共有辞書ファイルヘッダー。
/// </summary>
/// <remarks>
/// <c>IMESHF</c>構造体。
/// </remarks>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public readonly struct ImeUserDictionaryFileHeader(ushort version, string title, string description, string copyright)
{
	private readonly ushort cbShf = (ushort)Marshal.SizeOf<ImeUserDictionaryFileHeader>();
	public readonly ushort Version = version;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
	public readonly string Title = title;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
	public readonly string Description = description;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
	public readonly string Copyright = copyright;
}

// ImeDependencyInfo
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct IMEDP : IDisposable
{
	public ImeWord Modifier; // kakari-go
	public ImeWord Modifiee; // uke-go
	public ImeRelationType relID;

	public void Dispose()
	{
		Modifier.Dispose();
		Modifiee.Dispose();
	}
}

/// <summary>
/// 単語の品詞。
/// </summary>
/// <remarks>
/// <c>JPOS_*</c>
/// </remarks>
public enum ImeWordJPos : uint
{
	Undefined = 0,
	MeishiFutsu = 100,
	MeishiSahen = 101,
	MeishiZahen = 102,
	MeishiKeiyoudoushi = 103,
	HukusiMeishi = 104,
	MeisaKeidou = 105,
	Jinmei = 106,
	JinmeiSei = 107,
	JinmeiMei = 108,
	Chimei = 109,
	ChimeiKuni = 110,
	ChimeiKen = 111,
	ChimeiGun = 112,
	ChimeiKu = 113,
	ChimeiShi = 114,
	ChimeiMachi = 115,
	ChimeiMura = 116,
	ChimeiEki = 117,
	Sonota = 118,
	Shamei = 119,
	Soshiki = 120,
	Kenchiku = 121,
	Buppin = 122,
	Daimeishi = 123,
	DaimeishiNinshou = 124,
	DaimeishiShiji = 125,
	Kazu = 126,
	KazuSuryou = 127,
	KazuSushi = 128,
	GodanAWa = 200,
	GodanKa = 201,
	GodanGa = 202,
	GodanSa = 203,
	GodanTa = 204,
	GodanNa = 205,
	GodanBa = 206,
	GodanMa = 207,
	GodanRa = 208,
	GodanAWaUon = 209,
	GodanKaSoKuon = 210,
	GodanRahen = 211,
	YondanHa = 212,
	Ichidan = 213,
	TokushuKahen = 214,
	TokushuSahensuru = 215,
	TokushuSahen = 216,
	TokushuZahen = 217,
	TokushuNahen = 218,
	KuruKi = 219,
	KuruKita = 220,
	KuruKitara = 221,
	KuruKitari = 222,
	KuruKitarou = 223,
	KuruKite = 224,
	KuruKureba = 225,
	KuruKo = 226,
	KuruKoi = 227,
	KuruKoyou = 228,
	SuruSa = 229,
	SuruSi = 230,
	SuruSita = 231,
	SuruSitara = 232,
	SuruSiatri = 233,
	SuruSitarou = 234,
	SuruSite = 235,
	SuruSiyou = 236,
	SuruSureba = 237,
	SuruSE = 238,
	SuruSEYO = 239,
	Keiyou = 300,
	KeiyouGaru = 301,
	KeiyouGe = 302,
	KeiyouMe = 303,
	KeiyouYuu = 304,
	KeiyouU = 305,
	Keidou = 400,
	KeidouNo = 401,
	KeidouTaru = 402,
	KeidouGaru = 403,
	Fukushi = 500,
	FukushiSahen = 501,
	FukushiNi = 502,
	FukushiNano = 503,
	FukushiDa = 504,
	FukushiTo = 505,
	FukushiTosuru = 506,
	Rentaishi = 600,
	RentaishiShiji = 601,
	Setsuzokushi = 650,
	Kandoushi = 670,
	Settou = 700,
	SettouKaku = 701,
	SettouSai = 702,
	SettouFuku = 703,
	SettouMi = 704,
	SettouDaishou = 705,
	SettouKoutei = 706,
	SettouChoutan = 707,
	SettouShinkyu = 708,
	SettouJinmei = 709,
	SettouChimei = 710,
	SettouSonota = 711,
	SettouJosushi = 712,
	SettouTeineiO = 713,
	SettouTeineiGo = 714,
	SettouTeineiOn = 715,
	Setsubi = 800,
	SetsubiTeki = 801,
	SetsubiSei = 802,
	SetsubiKa = 803,
	SetsubiChu = 804,
	SetsubiFu = 805,
	SetsubiRyu = 806,
	SetsubiYoy = 807,
	SetsubiKata = 808,
	SetsubiMeishiRendaku = 809,
	SetsubiJinmei = 810,
	SetsubiChimei = 811,
	SetsubiKuni = 812,
	SetsubiKen = 813,
	SetsubiGun = 814,
	SetsubiKu = 815,
	SetsubiShi = 816,
	SetsubiMachi = 817,
	SetsubiChou = 818,
	SetsubiMura = 819,
	SetsubiSon = 820,
	SetsubiEki = 821,
	SetsubiSonota = 822,
	SetsubiShamei = 823,
	SetsubiSoshiki = 824,
	SetsubiKenchiku = 825,
	RenyouSetsubi = 826,
	SetsubiJosushi = 827,
	SetsubiJpsushiPlus = 828,
	SetsubiJikan = 829,
	SetsubiJikanPlus = 830,
	SetsubiTeinei = 831,
	SetsubiSna = 832,
	SetsubiKun = 833,
	SetsubiSama = 834,
	SetsubiDono = 835,
	SetsubiFukusu = 836,
	SetsubiTachi = 837,
	SetsubiRa = 838,
	Tankanji = 900,
	TankanjiKao = 901,
	Kanyouku = 902,
	Dokuristugo = 903,
	Futeigo = 904,
	Kigou = 905,
	Eiji = 906,
	Kuten = 907,
	Touten = 908,
	Kanji = 909,
	OpenBrace = 910,
	CloseBrace = 911,
	Yokusei = 912,
	Tanshuku = 913,
}

/// <summary>
/// 
/// </summary>
/// <remarks>
/// <c>IMEWRD</c>
/// </remarks>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ImeWord : IDisposable
{
	[MarshalAs(UnmanagedType.LPWStr)]
	private readonly string pwchReading;
	[MarshalAs(UnmanagedType.LPWStr)]
	private readonly string pwchDisplay;
	private readonly ImeWordJPos JPos;
	private readonly uint rgulAttrs1;
	private readonly uint rgulAttrs2;
	private int cbComment;
	private readonly ImeUserComment uct;
	private nint pvComment;

	public ImeWord(string reading, string display, ImeWordJPos jpos, ImeUserComment type, object? data)
	{
		pwchReading = reading;
		pwchDisplay = display;
		JPos = jpos;
		uct = type;
		switch (type)
		{
		case ImeUserComment.None:
			cbComment = 0;
			pvComment = 0;
			break;
		case ImeUserComment.StringSJis:
			if (data is string sjis)
			{
				var bytes = Encoding.ASCII.GetBytes(sjis);
				cbComment = bytes.Length;
				pvComment = ComHelper.ByteArrayToPtr(bytes);
			}
			else
			{
				throw new InvalidDataException("コメントには文字列が指定されています。");
			}
			break;
		case ImeUserComment.StringUnicode:
			if (data is string uni)
			{
				var bytes = Encoding.Unicode.GetBytes(uni);
				cbComment = bytes.Length;
				pvComment = ComHelper.ByteArrayToPtr(bytes);
			}
			else
			{
				throw new InvalidDataException("コメントには文字列が指定されています。");
			}
			break;
		case ImeUserComment.UserDefined:
			if (data is byte[] userDefined)
			{
				cbComment = userDefined.Length;
				pvComment = ComHelper.ByteArrayToPtr(userDefined);
			}
			else
			{
				throw new InvalidDataException("コメントにはバイト配列が指定されています。");
			}
			break;
		}
	}

	public void Dispose()
	{
		if (pvComment == 0) return;
		Marshal.FreeCoTaskMem(pvComment);
		cbComment = 0;
		pvComment = 0;
	}
}