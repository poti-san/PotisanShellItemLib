namespace Potisan.Windows.Com.ClrExtensions;

public static class CharArrayExtensions
{
	public static string ToStringAsNullTerminated(this char[] value)
		=> ToStringAsNullTerminated(value.AsSpan());

	public static string ToStringAsNullTerminated(this ReadOnlySpan<char> value)
	{
		var i = value.IndexOf('\0');
		return new string(i != -1 ? value[..i] : value);
	}
}