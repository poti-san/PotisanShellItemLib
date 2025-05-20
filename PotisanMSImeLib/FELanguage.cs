using Potisan.Windows.MSIme.ComTypes;

namespace Potisan.Windows.MSIme;

public class FELanguage(object? o) : ComUnknownWrapperBase<IFELanguage>(o)
{
	public ComResult OpenNoThrow()
		=> new(_obj.Open());

	public void Open()
		=> OpenNoThrow().ThrowIfError();

	public ComResult CloseNoThrow()
		=> new(_obj.Close());

	public void Close()
		=> CloseNoThrow().ThrowIfError();

	/// <summary>
	/// 日本語形態素分析を実行して結果を返します。
	/// </summary>
	/// <param name="request"></param>
	/// <param name="mode"></param>
	/// <param name="input"></param>
	/// <param name="infos"></param>
	/// <returns></returns>
	/// <example>
	/// <code>
	///<![CDATA[using Potisan.Windows.MSIme;
	///
	///Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
	///Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
	///
	///var msIme = MSIme.CreateImeJp();
	///var feLang = msIme.AsFELanguage;
	///
	///feLang.Open();
	///
	///var jmorfRes = feLang.GetJMorphResult(FELanguageConversionRequest.Rev, FELanguageConversionMode.MonoRuby, "漢字からひらがなへ！１２３");
	///
	///foreach (var wordDesc in jmorfRes.WordDescriptors)
	///{
	///	Console.WriteLine($"{wordDesc.InputString}: {wordDesc.OutputString}");
	///}
	///
	///feLang.Close();]]>
	/// </code>
	/// </example>
	public ComResult<JMorphResult> GetJMorphResultNoThrow(
		FELanguageConversionRequest request,
		FELanguageConversionMode mode,
		ReadOnlySpan<char> input,
		FELanguageMorphologyInfo[]? infos = null)
	{
		return new(_obj.GetJMorphResult((uint)request, (uint)mode, input.Length,
			MemoryMarshal.GetReference(input), infos, out var x), new(x));
	}

	/// <inheritdoc cref="GetJMorphResultNoThrow(FELanguageConversionRequest, FELanguageConversionMode, ReadOnlySpan{char}, FELanguageMorphologyInfo[]?)"/>
	public JMorphResult GetJMorphResult(
		FELanguageConversionRequest request,
		FELanguageConversionMode mode,
		ReadOnlySpan<char> input,
		FELanguageMorphologyInfo[]? infos = null)
		=> GetJMorphResultNoThrow(request, mode, input, infos).Value;

	public ComResult<FELanguageConversionMode> ConversionModeCapsNoThrow
		=> new(_obj.GetConversionModeCaps(out var x), (FELanguageConversionMode)x);

	public FELanguageConversionMode ConversionModeCaps
		=> ConversionModeCapsNoThrow.Value;

	public ComResult<string> GetPhoneticNoThrow(string s, int start = 1, int length = -1)
		// ReadOnlySpan<char>にしたいですが、BSTRなのでstringのまま扱います。
		=> new(_obj.GetPhonetic(s, start, length, out var x), x);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="s"></param>
	/// <param name="start"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	/// <example>
	/// <code>
	///<![CDATA[using Potisan.Windows.MSIme;
	///
	///Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
	///Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
	/// 
	///var msIme = MSIme.CreateImeJp();
	///var feLang = msIme.AsFELanguage;
	/// 
	///feLang.Open();
	/// 
	///var s = feLang.GetPhonetic("漢字から平仮名へ");
	///Console.WriteLine(s);
	/// // "かんじからひらがなへ"
	/// 
	///feLang.Close();]]></code>
	///</example>
	public string GetPhonetic(string s, int start = 1, int length = -1)
		=> GetPhoneticNoThrow(s, start, length).Value;

	/// <summary>
	/// 入力文字列の変換後の文字列を返します。
	/// </summary>
	/// <param name="s"></param>
	/// <param name="start"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	/// <example>
	/// <code>
	///<![CDATA[using Potisan.Windows.MSIme;
	///
	///Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
	///Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
	///
	///var msIme = MSIme.CreateImeJp();
	///var feLang = msIme.AsFELanguage;
	///
	///feLang.Open();
	///
	///var s = feLang.GetConversion("かんじからひらがなへ");
	///Console.WriteLine(s);
	/// // "漢字から平仮名へ"
	///
	///feLang.Close();]]>
	/// </code>
	/// </example>
	public ComResult<string> GetConversionNoThrow(string s, int start = 1, int length = -1)
		=> new(_obj.GetConversion(s, start, length, out var x), x);

	/// <inheritdoc cref="GetConversionNoThrow(string, int, int)"/>
	public string GetConversion(string s, int start = 1, int length = -1)
		=> GetConversionNoThrow(s, start, length).Value;
}

[Flags]
public enum FELanguageConversionRequest : uint
{
	Conv = 0x00010000,
	Reconv = 0x00020000,
	Rev = 0x00030000,
}

[Flags]
public enum FELanguageConversionMode : uint
{
	MonoRuby = 0x00000002,
	NoPruning = 0x00000004,
	KatakanaOut = 0x00000008,
	HiraganaOut = 0x00000000,
	HalfWidthOut = 0x00000010,
	FullWidthOut = 0x00000020,
	BopoMofo = 0x00000040,
	Hangul = 0x00000080,
	Pinyin = 0x00000100,
	Preponv = 0x00000200,
	Radical = 0x00000400,
	UnknownReading = 0x00000800,
	MergeCand = 0x00001000,
	Roman = 0x00002000,
	BestFirst = 0x00004000,
	UseNoRevWords = 0x00008000,
	None = 0x01000000,
	PlauralClause = 0x02000000,
	SingleConvert = 0x04000000,
	Automatic = 0x08000000,
	PhrasePredict = 0x10000000,
	Conversation = 0x20000000,
	Name = PhrasePredict,
	NoInvisibleChar = 0x40000000,
}

public enum FELanguageMorphologyInfo : uint
{
	WBreak = 0x01,
	NoWBreak = 0x02,
	PBreak = 0x04,
	NoPBreak = 0x08,
	FixR = 0x10,
	FixDisplay = 0x20,
}