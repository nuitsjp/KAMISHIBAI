[for Xamarin](Xamarin/README-ja.md)

[![KAMISHIBAI](https://raw.githubusercontent.com/nuitsjp/KAMISHIBAI/master/Images/KAMISHIBAI.png)](https://github.com/nuitsjp/KAMISHIBAI/blob/master/README-ja.md)

# KAMISHIBAI

KAMISHIBAIは、Generic Host上でMVVMパターンをサポートするWPF用の画面遷移ライブラリです。

ViewModelのコンストラクターに引数を宣言することで、専用の画面遷移メソッドが自動的に生成されます。

画面遷移時にstringをわたす場合、つぎのようにViewModelを定義します。

```cs
[Navigate]
public class FirstViewModel
{
    public FirstViewModel(string message)
    {
        Message = message;
    }

    public string Message { get; }
}
```

すると専用の画面遷移メソッドが自動生成され、つぎのように呼び出すことができます。

```cs
await _presentationService.NavigateToFirstAsync("Hello, KAMISHIBAI!");
```

またはDIを併用することも可能です。

```cs
public FirstViewModel(
    string message, 
    [Inject] ILogger<FirstViewModel> logger)
```

コンストラクターの引数にInjectAttributeを宣言することで、messageとはことなり、DIコンテナーから依存性を注入することもできます。先の画面遷移とまったく同じように画面遷移を呼び出すことができます。

KAMISHIBAIでは画面遷移時に型安全が保障され、nullableを最大限に活用した安全な実装が実現できます。

KAMISHIBAIは、WPFの機能を一切制限せず下記を実現します。

- Generic Hostのサポート
- MVVMパターンを適用したViewModel起点の画面遷移
- 型安全性の保証された画面遷移時パラメーター
- 画面遷移にともなう一貫性あるイベント通知
- nullableを最大限活用するためのサポート

Generic Hostをサポートすることで、ほとんどの .NETの最新テクノロジーがWPFで活用できます。

KAMISHIBAIは下記の機能を提供します。
- 画面遷移
- 新しい画面・ダイアログの表示
- メッセージダイアログ・ファイル選択・保存ダイアログの表示

WPFにおけるMVVMパターンを採用した画面遷移は、KAMISHIBAIを利用することで、より簡単に実現する事ができます。

そして既存のあらゆるMVVMフレームワークと同居が可能です。ただしひとつだけ制約が発生します。

画面遷移にはKAMISHIBAIを利用してください。

