namespace Potisan.Windows.Com;

/// <summary>
/// COMの10進数表現小数。
/// </summary>
/// <param name="Reserved"></param>
/// <param name="Scale"></param>
/// <param name="Sign"></param>
/// <param name="Hi32"></param>
/// <param name="Lo32"></param>
/// <param name="Mid32"></param>
/// <remarks><c>DECIMAL</c>ネイティブ型のラッパーです。</remarks>
public record struct ComDecimal(ushort Reserved, byte Scale, byte Sign, uint Hi32, uint Lo32, uint Mid32)
{
	public readonly decimal ToDecimal()
		=> new((int)Lo32, (int)Mid32, (int)Hi32, Sign == 0x80, Scale);
}
