# 開発者向け情報

このクラスライブラリでは円滑な開発のために次のクラスを使用しています。追加機能の作成時は利用を検討してください。

- `ComResult`および`ComResult<T>`
- `IComUnknownWrapper`
- `ComUnknownWrapperBase<TIUnknown>`

## `ComResult`および`ComResult<T>`

COM関数の結果を`HRESULT`（`ComResult`）あるいは`HRESULT`と出力の組（`ComResult<T>`）として保持する構造体です。GCの負荷を減らすために構造体を選択しています。

次の機能を持ちます。

- `inplicit operator bool(in ComResult cr)`
  - `hr`が成功なら`true`、それ以外なら`false`
  - `if (cr) <成功時の処理> else <エラー時の処理>`
- `ComResult.HRSuccess(int hr)`
  - `ComResult<bool>`を作成します。値は`hr`が`S_OK`なら`true`、それ以外なら`false`です。
- `T? ComResult<T>.Or(T? defaultValue)`
  - `ComResult<T>`が成功を保持していれば元の値、エラーを保持していれば`defaultValue`を返します。

### パターンマッチングの応用

`ComResult<T>`はパターンマッチングを利用して変換できます。コードは煩雑になりますが、デリゲートよりもGCへの負荷を減らせる可能性があります。

```cs
// 成否に関わらず値をキャストする。
// 列挙型への変換等で使用できます。
cr switch { { HResult: var hr, ValueUnchecked: var x } => new(hr, (OtherType)x) };
```

## `IComUnknownWrapper`

COMインターフェイスのラッパーを表すインターフェイスです。派生クラスはランタイム呼び出し可能ラッパー(RCW)インスタンスを受け取るコンストラクタ、RCWインスタンスの値を返す`WrappedObject`プロパティ、`Dispose`メソッドを実装してください。

`IComUnknownWrapper`は次のメソッドを実装しています。

```cs
Wrap<TWrapper>(int hr, object? o)
    where TWrapper : IComUnknownWrapper
```

- `o`をコンストラクタの引数とした`TWrapper`インスタンスを作成して、結果と`hr`の値から`ComResult<TWrapper>`を作成します。
- `TWrapper`がテンプレート型の場合に使用します。

```cs
Wrap<TWrapper>(ComResult<object> cr)
    where TWrapper : IComUnknownWrapper
```

- `cr`の保持するオブジェクトをコンストラクタの引数とした`TWrapper`インスタンスを作成して、結果と`hr`の値から`ComResult<TWrapper>`を作成します。
- `TWrapper`がテンプレート型の場合に使用します。

```cs
Wrap<TWrapperTo, TWrapperFrom>(ComResult<object> cr)
    where TWrapperTo : IComUnknownWrapper
    where TWrapperFrom : IComUnknownWrapper
```

- `cr`の保持するオブジェクトをコンストラクタの引数とした`TWrapperTo`インスタンスを作成して、結果と`hr`の値から`ComResult<TWrapper>`を作成します。
- `TWrapperTo`がテンプレート型の場合に使用します。

```cs
Casted<TWrapper, TInterface>(object o)
    where TWrapper : IComUnknownWrapper
    where TInterface : class
```

- `o`が`TInterface`にキャストできるか確認して、成否(`S_OK`または`E_FAIL`)とキャスト結果を保持する`ComResult<TWrapper>`を作成します。
- `TWrapper`がテンプレート型の場合に使用します。

## `ComUnknownWrapperBase<TIUnknown>`

COMインターフェイスを公開できる場合に使用できる`IComUnknownWrapper`インターフェイスの抽象クラスラッパーです。次の機能を自動的に実装します。

- `_obj`プロテクト変数の作成と引数`o`が`TIUnknown`にキャスト可能かの確認。`o`が`null`の場合は特別に許容されます。
- `WrappedObject`プロパティ`_obj`プロテクト変数の値を返します。
- `Dispose`メソッド。`Marshal.FinalReleaseComObject`クラスメソッドも適用されます。不要な場合は派生クラスで処理を上書きしてください。

## 構造体に配列を渡す。

ごく少数の構造体、例えば`DISPPARAMS`等は内部にマネージ型や`VARIANT`型の配列を保持しています。この時は次のメソッドが使用できます。

- `ComHelper.SafeStructureArrayToPtr<T>`静的メソッド、`ComHelper.SafeStructureToPtrForClass<T>`静的メソッド
  - 構造体のフィールドに持たせる構造体やクラスのアンマネージド表現配列を作成します。後者は`null`許容版です。
- `NativeVariantArrayOnCoTaskMem`クラス
  - 構造体のフィールドに持たせる`VARIANT`型のアンマネージド表現配列を作成します。クラスは解放時に`VARIANT`配列の各要素も解放します。
