using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// シェルアイテムの名前制限情報。
/// </summary>
/// <example>
/// <code>
/// <![CDATA[using Potisan.Windows.Shell;
///
/// using var desktop = ShellItem2.CreateKnownFolderItem(KnownFolderID.Desktop);
/// using var nameLimits = desktop.ItemNameLimits;
///
/// var maxLen = nameLimits.GetMaxLength("");
/// var(validChars, invalidChars) = nameLimits.ValidCharacters;
///
/// Console.WriteLine($"""
/// 	ファイル名：{desktop.NormalDisplayName}
/// 	有効な文字："{validChars}"
/// 	無効な文字："{invalidChars}"
/// 	最大長　　：{maxLen}
/// 	""");]]>
///	</code>
/// </example>
/// <remarks>
/// <c>IItemNameLimits</c> COMインターフェイスのラッパーです。
/// </remarks>
public class ItemNameLimits(object? o) : ComUnknownWrapperBase<IItemNameLimits>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(string ValidChars, string InvalidChars)> ValidCharactersNoThrow
		=> new(_obj.GetValidCharacters(out var x1, out var x2), (x1, x2));

	public (string ValidChars, string InvalidChars) ValidCharacters
		=> ValidCharactersNoThrow.Value;

	public ComResult<int> GetMaxLengthNoThrow(string name)
		=> new(_obj.GetMaxLength(name, out var x), x);

	public int GetMaxLength(string name)
		=> GetMaxLengthNoThrow(name).Value;
}
