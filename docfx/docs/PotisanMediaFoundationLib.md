# PotisanMediaFoundationLib

## サンプルコード

### カメラの映像をキャプチャする。

CameraCaptureプロジェクトのコードを参照してください。

### 動画のサムネイルを作成する。

VideoFileThumbnailプロジェクトのコードを参照してください。
このプロジェクトはマイドキュメントに「test.mp4」があると仮定しています。

### MP4ファイルの音声を再生する。

マイドキュメントに「test.mp4」があると仮定しています。
開始30秒間を演奏しますが、コンソールを閉じると中断できます。

```cs
using Potisan.Windows.MediaFoundation;
using Potisan.Windows.MediaFoundation.MediaEngine;

MF.Startup();

var mediaEndineFactory = MFMediaEngineClassFactory.Create();

var mediaEndineAttrs = MFAttributes.Create();
mediaEndineAttrs.ForMediaEngine.AudioEndpointRole = MFRole.Multimedia;
mediaEndineAttrs.ForMediaEngine.AudioCategory = MFAudioStreamCategory.Media;

var mediaEndine = mediaEndineFactory.CreateInstance(
	MFMediaEngineNotify.CreateConsoleWriter(),
	MFMediaEngineCreateFlag.AudioOnly,
	mediaEndineAttrs);

var mp4Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.mp4");

mediaEndine.Source = mp4Path;
mediaEndine.Load();
mediaEndine.Play();

// 最初30秒（30000 ms）だけ演奏
Thread.Sleep(30000);

Console.WriteLine();
```

### ローカルファイルのMFByteStreamを作成する。

マイドキュメントに「test.mp4」があると仮定しています。
未確認ですがインターネット上の動画も取得できるはずです。

```cs
using Potisan.Windows.MediaFoundation;

var mp4Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.mp4");

MF.Startup();

var sourceResolver = MFSourceResolver.Create();

var byteStream = sourceResolver.CreateByteStreamFromUrl(mp4Path, MFResolutionFlag.Read);
var buffer = new byte[100];
byteStream.Read(buffer);

Console.WriteLine();
```
