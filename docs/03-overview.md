# NuGet Package Structure and Overview

KAMISHIBAIはNuGetの3つのパッケージを公開しています。

|Package|役割|
|--|--|
|Kamishibai|コアライブラリ。画面遷移などを利用するためのインターフェイスなどを含む。|
|Kamishibai.View|WPFで利用するUI要素など、View層で利用するクラス群を含む。|
|Kamishibai.Hosting|KAMISHIBAIをGeneric Host上にホストするためのクラスを含む。|

Kamishibai.Hostingは推移的にすべてのパッケージに依存しています。

そのため、もっとも簡単な利用方法はKamishibai.Hostingの参照を追加することです。すべての機能が利用可能になります。

しかし実際のアプリケーションを構築する場合、アプリケーション内の役割に応じて、いくつかのプロジェクトに分割することをKAMISHIBAIでは推奨しています。これは単一のプロジェクトにすべてのコードを配置してしまうと、レイヤー間の依存関係を正しく管理しつづけることが難しいからです。

KAMISHIBAIのサンプル「SampleBrowser」アプリケーションでは、つぎのようにプロジェクトを分割し、それぞれのプロジェクトの依存関係を制限しています。

![](/images/books/kamishibai/components.png)

|Package|役割|
|--|--|
|SampleBrowser.ViewModel|Kamishibaiのみ参照する。WPFのクラスも参照していない。ViewModelはテスタビリティやMVVMを厳密に守るためにはWPFのクラスに依存しないほうが好ましいが、必ずしも厳守する必要もない。|
|SampleBrowser.View|Kamishibai.Viewを参照し、推移的にKamishibaiに依存する。|
|SampleBrowser.Hosting|アプリケーションをGeneric Host上にホスティングするための初期化処理のみを記載する。すべてのプロジェクト・パッケージに直接または推移的に依存する。|

もちろん、これらのプロジェクトだけで実現しろという意味ではありません。たとえばドメインプロジェクトなどを必要に応じて追加する必要がるでしょう。また大きなプロジェクトではViewやViewModelも分割する必要があるかもしれません。

あくまで参考としてサンプルコードをご覧ください。

# 画面遷移概要

KAMISHIBAIの画面遷移において重要な要素は下記に表します。

![](/images/books/kamishibai/architecture.png)

KAMISHIBAIでは画面遷移させたい領域にNavigationFrameを定義します。

NavigationFrameの中に任意のUserControlを表示することで画面遷移を実現します。

NavigationFrameにはFrameNameを定義することができ、1画面内に複数のNavigationFrameを定義することや、ネストすることが可能です。

画面遷移はFrameNameを指定します。

```cs
_presentationService.NavigatePage2Async("FrameA");
```

FrameNameはアプリケーション内でユニークである必要があります。デフォルトのFrameNameは空文字列で、省略可能です。

画面遷移はIPresentationServiceをViewModelに注入して利用します。

IPresentationServiceは、つぎの属性が宣言されたクラスの存在するプロジェクトにコード生成されます。

- NavigateAttribute
- OpenWindowAttribute
- OpenDialogAttribute

これらの属性がひとつも利用されていない場合、IPresentationServiceが生成されないため注意してください。

では、より具体的な利用方法について説明しましょう。

