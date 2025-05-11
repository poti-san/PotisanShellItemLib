# PotisanComLib

## サンプルコード

### Running Object Table (ROT)の項目を取得する。

```cs
using Potisan.Windows.Com;

var rot = RunningObjectTable.CreateLocal();

foreach (var item in rot)
{
	Console.WriteLine(item);
}
```

### クリップボード上のフォーマットとデータ形式を取得する。

```cs
using Potisan.Windows.Com;

var dataobj = ComDataObject.GetClipboard();
foreach (var fmtetc in dataobj.FormatEtcGetterEnumerator)
{
	var cf = fmtetc.ClipboardFormat;
	var data = dataobj.GetData(fmtetc);
	Console.WriteLine($"{cf}: {data.Tymed}形式、{data.GetByteLengthNoThrow().ValueUnchecked}バイト");
}
```

### COMカテゴリと所属クラスCLSIDを取得する。

```cs
using Potisan.Windows.Com;

var categoryManager = ComCategoryManager.Create();
foreach (var (catId, lcid, desc) in categoryManager.Categories)
{
	Console.WriteLine($"{catId:B},{lcid.Value},{desc}");
	foreach (var clsid in categoryManager.GetCategoryClassEnumerable([catId], []))
	{
		Console.WriteLine($"  {clsid:B},{ComGuidHelper.ClsidToProgIDNoThrow(clsid).Or("")}");
	}
}
```
