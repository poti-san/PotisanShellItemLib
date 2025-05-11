namespace Potisan.Windows.Com;

/// <summary>
/// システム定義のクリップボードフォーマット。
/// </summary>
/// <remarks>
/// 文字列として定義されたフォーマットは含みません。
/// </remarks>
public enum SystemClipboardFormat : ushort
{
#pragma warning disable CA1707 // 識別子はアンダースコアを含むことはできません
	CF_TEXT = 1,
	CF_BITMAP = 2,
	CF_METAFILEPICT = 3,
	CF_SYLK = 4,
	CF_DIF = 5,
	CF_TIFF = 6,
	CF_OEMTEXT = 7,
	CF_DIB = 8,
	CF_PALETTE = 9,
	CF_PENDATA = 10,
	CF_RIFF = 11,
	CF_WAVE = 12,
	CF_UNICODETEXT = 13,
	CF_ENHMETAFILE = 14,
	CF_HDROP = 15,
	CF_LOCALE = 16,
	CF_DIBV5 = 17,
	CF_OWNERDISPLAY = 0x0080,
	CF_DSPTEXT = 0x0081,
	CF_DSPBITMAP = 0x0082,
	CF_DSPMETAFILEPICT = 0x0083,
	CF_DSPENHMETAFILE = 0x008E,
#pragma warning restore CA1707
}

/// <summary>
/// クリップボードフォーマット。
/// </summary>
public record struct ClipboardFormat(ushort Value)
{
	public const ushort PrivateFirst = 0x0200;
	public const ushort PrivateLast = 0x02FF;
	public const ushort GdiObjectFirst = 0x0300;
	public const ushort GdiObjectLast = 0x03FF;
	public const ushort RegisteredFirst = 0xC000;
	public const ushort RegisteredLast = 0xFFFF;

	// BOX化は許容します。
	public readonly bool IsSystem => Enum.IsDefined(typeof(SystemClipboardFormat), Value);

	public readonly SystemClipboardFormat? SystemFormat => IsSystem ? (SystemClipboardFormat)Value : null;

	public readonly bool IsPrivate => PrivateFirst <= Value && Value <= PrivateLast;
	public readonly bool IsGdiObject => GdiObjectFirst <= Value && Value <= GdiObjectLast;
	public readonly bool IsRegistered => RegisteredFirst <= Value && Value <= RegisteredLast;

	public readonly string? RegisteredName
	{
		get
		{
			[DllImport("user32.dll", CharSet = CharSet.Unicode)]
			static extern int GetClipboardFormatNameW(uint format, ref char lpszFormatName, int cchMaxCount);

			for (uint i = 0; i < int.MaxValue; i++)
			{
				var buffer = GC.AllocateUninitializedArray<char>(unchecked((int)i));
				var copied = GetClipboardFormatNameW(Value, ref MemoryMarshal.GetArrayDataReference(buffer), checked((int)i));

				var err = Marshal.GetLastPInvokeError();
				if (err != 0)
					return null;
				if (copied + 1 < i)
					return new string(buffer, 0, copied);
			}
			throw new InvalidDataException();
		}
	}

	public static ClipboardFormat? Register(string name)
	{
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		static extern uint RegisterClipboardFormatW(string lpszFormat);

		var x = RegisterClipboardFormatW(name);
		if (x == 0)
			return null;
		return new(checked((ushort)x));
	}

	public readonly string ToHashNoString()
		=> $"#{Value}";

	public readonly string? SystemName
		=> IsSystem ? ((SystemClipboardFormat)Value).ToString() : null;

	public override readonly string ToString()
		=> SystemName ?? RegisteredName ?? ToHashNoString();
}