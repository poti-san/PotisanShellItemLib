# PotisanShellItemLib

デスクトップやエクスプローラーに表示される項目の操作機能を提供します。

## サンプルコード

### デスクトップ上のアイテムの表示名を列挙する。

```cs
using Potisan.Windows.Shell;

var desktop = ShellItem.CreateKnownFolderItem(KnownFolderID.Desktop);
foreach (var item in desktop.Items)
{
	Console.WriteLine(item.NormalDisplayName);
}
```

### ファイル拡張子に関連付けられた実行方法を取得する。

```cs
using Potisan.Windows.Shell;

Console.WriteLine($"UI表示名, 名前, アイコンの場所");
foreach (var assoc in ShellAssocHandlerEnumerator.Create(".txt", ShellAssocFilter.None))
{
	Console.WriteLine($"{assoc.UIName}, {assoc.Name}, {assoc.IconLocation}");
}
```
