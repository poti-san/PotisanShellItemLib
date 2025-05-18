namespace Potisan.Windows.Com;

public record struct ComDecimal(ushort Reserved, byte Scale, byte Sign, uint Hi32, uint Lo32, uint Mid32)
{
	public readonly decimal ToDecimal()
		=> new((int)Lo32, (int)Mid32, (int)Hi32, Sign == 0x80, Scale);
}
