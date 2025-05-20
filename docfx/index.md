# PotisanComLibs

WindowsのCOM機能ラッパークラスライブラリをまとめたC#ソリューションです。開発方針は『COMに不慣れな人でも使いやすく、少しのコードでCOMを使えて、多少動作が遅くても開発に負担の少ないこと』です。作りはじめなので常に修正中です。

|クラスライブラリ|概要|依存関係|
|:--|:--|:--|
|PotisanComCoreLib|最低限のCOM機能。Direct3Dのような最低限のCOM機能を使用するクラスライブラリから参照されます。||
|PotisanComLib|基本的なCOM機能。ほとんどのクラスライブラリから参照されます。|PotisanComCoreLib|
|PotisanPropertySystemLib|プロパティシステムのラッパーライブラリ。|PotisanComLib|
|PotisanShellItemLib|シェルアイテムのラッパーライブラリ。|PotisanPropertySystemLib|
|PotisanAutomationLib|オートメーション機能（IDispatchやITypeLib）のラッパーライブラリ。|PotisanComLib|
|PotisanMediaFoundationLib|Microsoft Media Foundationのラッパーライブラリ。|System.Drawing.Common、PotisanPropertySystemLib|
|PotisanDxgiLib|DXGIのラッパーライブラリ|PotisanComCoreLib|
|PotisanDXCoreLib|DXCoreのラッパーライブラリ|PotisanComCoreLib|
|PotisanShellWindowLib|シェルウィンドウのラッパーライブラリ。|PotisanAutomationLib、PotisanShellItemLib|
|PotisanMSImeLib|Microsoft IME COM機能のラッパーライブラリ。**作成初期です。**|PotisanComLib|
|PotisanTextServicesFrameworkLib|TSFのラッパーライブラリ。**作成初期です。**|PotisanComLib|
|PotisanWindowsUpdateAgentLib|Windows Update Agentのラッパーライブラリ。**作成初期です。**|PotisanAutomationLib|

## 使用上の注意

- GitHubにもクラスライブラリ開発にも不慣れです。学びながら開発、公開しているため、コードにもリポジトリにも不備が残っているはずです。
- 使用時のファイルサイズやメモリサイズを節約するため、個々のプロジェクトはある程度の機能単位で独立させています。この単位はWindowsにおけるDLLの管轄とは一致しません。
- 現時点では組み込みCOMラッパーRCWを使用しており、Native AOT非対応です。WinFormsがNative AOT対応した場合に対応を予定しています。
- 引数を取らない処理は速度に関わらずプロパティとして実装しています。特にIEnumerable実装クラスを返すメソッドはXXXEnumerableとして実装しています。処理速度は将来的にドキュメントへ記載します。
- このライブラリでは`IEnum～`COMインターフェイスのラッパークラスを`～Enumerable`と命名しています。列挙回数が1回のみのクラスも含まれますが、`yield`機能を使用しており、またCOMインターフェイスがコレクションやリストの機能を持つ場合もあることから、Enumerable、Collection、Listとなるように統一しています。
- プロジェクト数が増えたら他のリポジトリへ分割する可能性があります。
- WinForms水準のオブジェクト指向は目指しません。Registryクラスのようにコレクションの要素は個別のメソッドで扱います。

## COMインターフェイスラッパー

COMインターフェイスをラップするクラスです。次の特徴を持ちます。

- 作成時にRCWインスタンスを受け取ります。RCWインスタンスが対応する型にキャストできない場合は例外を発生します。`null`は特別扱いしてメソッド実行時に`null`参照例外を発生させます。
- メソッドのNoThrow版と無印版を持ちます。

## メソッドのNoThrow版と無印版

COMメソッド・関数に対応するほとんどのメソッド・プロパティ・関数にはNoThrow版と無印版があります。NoThrow版はCOMメソッド・関数の返すエラーを`ComResult`または`ComResult<T>`でラップして返します。無印版はエラーに対して例外を送出します。基本的に無印版はNoThrow版のラッパーです。

NoThrow版が存在しない場合はNoThrow版の処理が複雑で無印版のみの方が開発・使用上のコストが低い場合か、元となるCOM機能がエラーコードを返さない場合です。

NoThrow版はエラー時に保持する値を保証しません。使用前にエラーの有無を確認するか、`ComResult<T>`の機能で値を適切に無視してください。

> [!WARNING]
> NoThrow版もRCWインスタンスが`null`の場合やCOM処理以外のエラーでは例外を発生します。

## 謝辞

プログラミングに関心のある日本の子供達、そして大人に素晴らしいOS、サービス、IDE、SDK、MSDNから続く膨大な情報、そしてGitHubを提供して下さっているMicrosoft社、実際にGitHubを運営されているGitHub社、有益な情報を惜しみ無く公開してくださる多くのプログラマーに感謝申し上げます。

このリポジトリが少しでも誰かの役に立つことを願います。
