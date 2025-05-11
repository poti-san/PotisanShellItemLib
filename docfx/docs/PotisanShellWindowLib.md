# PotisanShellWindowLib

シェルウィンドウ機能のラッパーライブラリです。エクスプローラーの提供するGUI機能を操作できます。

## サンプルコード

### 最初に見つかったエクスプローラーウィンドウの項目名を取得する。

```cs
using Potisan.Windows.Shell;

var shellWindows = ShellWindows.Create();
var sbrowser = shellWindows.ShellBrowserEnumerable.FirstOrDefault();
if (sbrowser == null)
{
	Console.WriteLine("シェルウィンドウが開かれていません。");
	return;
}

var sview = sbrowser.ActiveShellView;

foreach (var item in sview.AllItems.ShellItem2Enumerable)
{
	Console.WriteLine(item.NormalDisplayName);
}
```

### シェルウィンドウの公開インターフェイス名を取得する。

```cs
var shellWindows = ShellWindows.Create();

foreach (var item in shellWindows.ItemEnumerable)
{
	Console.WriteLine(item.HasTypeInfo ? item.TypeInfo.Documentation.Name : "<インターフェイス名不明>");
}
```

### シェルウィンドウのフォルダ名を取得する。

```cs
using Potisan.Windows.Shell;

var shellWindows = ShellWindows.Create();

foreach (var sbrowser in shellWindows.ShellBrowserEnumerable)
{
	var fview = sbrowser.ActiveShellViewAsFolderView;

	Console.WriteLine(fview.FolderAsShellItem2.NormalDisplayName);
}
```
