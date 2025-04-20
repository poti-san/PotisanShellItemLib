// デスクトップ上のアイテムの表示名を列挙する。

using PotisanShellItemLib.Shell;

var desktop = ShellItem.CreateKnownFolderItem(KnownFolderID.Desktop);
foreach (var item in desktop.Items)
{
	Console.WriteLine(item.NormalDisplayName);
}