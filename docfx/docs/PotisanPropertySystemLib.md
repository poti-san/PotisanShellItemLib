# PotisanPropertySystem

## サンプルコード

### フォントアイテムのシステムキーでのみ取得できるプロパティを取得する。

```cs
using Potisan.Windows.PropertySystem;
using Potisan.Windows.Shell;

var propsys = PropertySystem.Create();
var syskeys = propsys.AllPropertyKeys.ToArray();

var desktop = ShellItem2.CreateKnownFolderItem(KnownFolderID.Fonts);
var item = desktop.Items.First();
var ps = item.GetPropertyStore(GetPropertyStoreFlag.Default);
var keys = ps.Keys.ToArray();

var keysOnlyInSystem = new HashSet<PropertyKey>(ps.WhereValidKeys(syskeys));
keysOnlyInSystem.ExceptWith(keys);
var itemsOnlyInSystem = ps.GetItemsForKeysIgnoreMissingKeys(keysOnlyInSystem);
foreach (var (key, value) in itemsOnlyInSystem)
{
	Console.WriteLine($"{key}: {value}");
}
```
