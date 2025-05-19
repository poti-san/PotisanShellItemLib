using Potisan.Windows.Com.SafeHandles;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// シェルフォルダと要素IDリストのペア。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// <code>
///<![CDATA[using Potisan.Windows.Shell;
///
///var documentsFolder = ShellItem.CreateKnownFolderItem(KnownFolderID.Documents);
///
///var itemAndParent = documentsFolder.AsParentAndItem;]]>
/// </code>
/// </example>
public class ParentAndItem(object? o) : ComUnknownWrapperBase<IParentAndItem>(o)
{
	public ComResult SetParentAndItemAsRcwNoThrow(SafeHandle pidlParent, object shellFolder, SafeHandle pidlChild)
		=> new(_obj.SetParentAndItem(pidlParent.DangerousGetHandle(), shellFolder, pidlChild.DangerousGetHandle()));

	public ComResult<(SafeHandle pidlParent, object shellFolder, SafeHandle pidlChild)> GetParentAndItemAsRcwNoThrow()
		=> new(_obj.GetParentAndItem(out var pidlParent, out var sf, out var pidlChild),
			(new SafeCoTaskMemHandle(pidlParent, true), sf, new SafeCoTaskMemHandle(pidlChild, true)));

	public (SafeHandle pidlParent, object shellFolder, SafeHandle pidlChild) ParentAndItemAsRcw
	{
		get => GetParentAndItemAsRcwNoThrow().Value;
		set => SetParentAndItemAsRcwNoThrow(value.pidlParent, value.shellFolder, value.pidlChild).ThrowIfError();
	}
}
