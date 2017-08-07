[[Home]](../README-ja.md) > 詳細仕様

# 詳細仕様

1. [画面遷移イベント基本仕様](#基本仕様)  
    1. [イベント種別](#イベント種別)
    2. [MainPage設定時のイベント発行順序](#MainPage設定時のイベント発行順序)  
    3. [次画面遷移のイベント発行順序](#次画面遷移のイベント発行順序)
    4. [戻る遷移のイベント発行順序](#戻る遷移のイベント発行順序)  
2. [遷移イベント個別仕様](#遷移イベント個別仕様)
    1. [OnInitialize](#oninitialize)  
    2. [OnLoaded](#onloaded)  
    3. [OnUnloaded](#onunloaded)  
3. [画面遷移時パラメーター仕様](#画面遷移時パラメーター仕様)  
4. [Applicationライフサイクルイベント仕様](#applicationライフサイクルイベント仕様)  
    1. [OnSleep](#onsleep)  
    2. [OnResume](#onresume)

# [画面遷移イベント基本仕様](#詳細仕様)  

KAMISHIBAIでは、画面遷移時に複数のイベント通知に対応しています。  
イベントの通知は対応するインターフェース（IPageInitializeAwareなど）を実装したViewまたはViewModelに対して行います。ViewModelだけではなく、View（Page）に対しても通知可能です。  
またイベントの通知は、イベントの種別・遷移の内容（前へ進むか戻るか）と言った内容によって、具体的な処理が異なります。  
本章ではイベントの種別と、遷移内容による異なる処理の概要について解説します。  

## [イベント種別](#詳細仕様)  

KAMISHIBAIでは画面遷移のイベントとして、つぎをサポートしています。  

|No.|イベント種別|概要|
|:--|:--|:--|
|1|OnInitialize|画面遷移時に新しく作成されたViewおよびViewModelの初期化時に一度だけ通知されます。<br>遷移要求時に画面遷移パラメーターが指定されていた場合、OnInitializeイベントにてパラメーターを受け取ることが可能です。パラメーターを受け取ることが可能なのはOnInitializeイベントのみです。<br>画面遷移時において、遷移処理の実行前（例えばINavigation#PusyAsync()メソッドの呼び出し前）に呼び出されます。|
|2|OnLoaded|画面遷移が完了し、画面が表示された時に呼び出されます。<br>一般的なINavigationを利用した画面遷移だけではなく、TabbedPageやCarouselPageでCurrentページが変更された際にも呼び出されます。|
|3|OnUnloaded|画面遷移時に、遷移元の画面が非表示になったタイミングで呼び出されます。<br>OnLoadedと同様にTabbedPageやCarouselPageでCurrentページが変更された際にも呼び出されます。|

## [MainPage設定時のイベント発行順序](#詳細仕様)  

アプリケーションの起動時や、実行中にMainPageを置き換える場合のイベント実行順序について説明します。  
イベントは次の順番で実行されます。  

1. 設定対象のView及びViewModelのOnInitilalizeイベントの通知  
2. Application.Current.MainPageへPageの設定  
3. 設定対象のView及びViewModelのOnLoadedイベントの通知  
4. 

## [次画面遷移のイベント発行順序](#詳細仕様)  



## [戻る遷移のイベント発行順序](#詳細仕様)  
# [遷移イベント個別仕様](#詳細仕様)  
## [OnInitialize](#詳細仕様)  
## [OnLoaded](#詳細仕様)  
## [OnUnloaded](#詳細仕様)  
# [画面遷移時パラメーター仕様](#詳細仕様)  
# [Applicationライフサイクルイベント仕様](#詳細仕様)  
## [OnSleep](#詳細仕様)  
## [OnResume](#詳細仕様)  