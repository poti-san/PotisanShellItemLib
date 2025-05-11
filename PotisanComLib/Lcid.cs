namespace Potisan.Windows.Com;

/// <summary>
/// 言語コードID。
/// </summary>
/// <param name="Value"></param>
public record struct Lcid(uint Value)
{
	public readonly ushort Reserved => unchecked((ushort)((Value & 0b111111111111_0000_0000000000000000) >> 21));
	public readonly ushort SortID => unchecked((ushort)((Value & 0b000000000000_1111_0000000000000000) >> 16));
	public readonly ushort LanguageID => unchecked((ushort)(Value & 0b000000000000_0000_1111111111111111));

	public Lcid(ushort sortId, ushort languageId, ushort reserved = 0)
		: this(0)
	{
		ArgumentOutOfRangeException.ThrowIfNotEqual((ushort)(reserved & ~0b111111111111_0000_0000000000000000), 0, nameof(reserved));
		ArgumentOutOfRangeException.ThrowIfNotEqual((ushort)(sortId & ~0b000000000000_1111_0000000000000000), 0, nameof(sortId));
		ArgumentOutOfRangeException.ThrowIfNotEqual((ushort)(languageId & ~0b000000000000_0000_1111111111111111), 0, nameof(languageId));

		Value = unchecked((uint)((sortId << 16) | languageId | (reserved << 21)));
	}

	/// <summary>
	/// ユーザーの既定言語コードID。
	/// </summary>
	public static Lcid UserDefault
	{
		get
		{
			[DllImport("kernel32.dll")]
			static extern uint GetUserDefaultLCID();
			return new(GetUserDefaultLCID());
		}
	}

	/// <summary>
	/// システムの既定言語コードID。
	/// </summary>
	public static Lcid SystemDefault
	{
		get
		{
			[DllImport("kernel32.dll")]
			static extern uint GetSystemDefaultLCID();
			return new(GetSystemDefaultLCID());
		}
	}
}
