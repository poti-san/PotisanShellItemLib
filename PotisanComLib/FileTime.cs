namespace Potisan.Windows.Com;

/// <summary>
/// Windowsのファイル日時表現。
/// </summary>
public readonly record struct FileTime(uint LowDateTime, uint HighDateTime)
{
	public DateTime ToDateTime() => DateTime.FromFileTime(HighDateTime << 16 | LowDateTime);
	public DateTime ToDateTimeUtc() => DateTime.FromFileTimeUtc(HighDateTime << 16 | LowDateTime);

	public FileTime(in DateTime dt, bool utc)
		: this(0, 0)
	{
		var ul = unchecked((ulong)(utc ? dt.ToFileTimeUtc() : dt.ToFileTime()));
		LowDateTime = unchecked((uint)(ul & 0x00000000ffffffff));
		HighDateTime = unchecked((uint)((ul & 0xffffffff00000000) >> 32));
	}
}
