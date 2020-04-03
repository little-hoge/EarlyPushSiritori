# 目次

<!-- TOC -->

- [目次](#%E7%9B%AE%E6%AC%A1)
- [デモ](#%E3%83%87%E3%83%A2)
- [開発環境](#%E9%96%8B%E7%99%BA%E7%92%B0%E5%A2%83)
- [使用アセット](#%E4%BD%BF%E7%94%A8%E3%82%A2%E3%82%BB%E3%83%83%E3%83%88)
  - [ユーザーデータ管理(必須)](#%E3%83%A6%E3%83%BC%E3%82%B6%E3%83%BC%E3%83%87%E3%83%BC%E3%82%BF%E7%AE%A1%E7%90%86%E5%BF%85%E9%A0%88)
  - [通信対戦同期(必須)](#%E9%80%9A%E4%BF%A1%E5%AF%BE%E6%88%A6%E5%90%8C%E6%9C%9F%E5%BF%85%E9%A0%88)
  - [バージョン管理(任意)](#%E3%83%90%E3%83%BC%E3%82%B8%E3%83%A7%E3%83%B3%E7%AE%A1%E7%90%86%E4%BB%BB%E6%84%8F)
  - [WebGL画面サイズ自動調整(任意)](#webgl%E7%94%BB%E9%9D%A2%E3%82%B5%E3%82%A4%E3%82%BA%E8%87%AA%E5%8B%95%E8%AA%BF%E6%95%B4%E4%BB%BB%E6%84%8F)
  - [WebGL日本語入力(任意)](#webgl%E6%97%A5%E6%9C%AC%E8%AA%9E%E5%85%A5%E5%8A%9B%E4%BB%BB%E6%84%8F)
- [参考リンク](#%E5%8F%82%E8%80%83%E3%83%AA%E3%83%B3%E3%82%AF)
  - [環境](#%E7%92%B0%E5%A2%83)
  - [実装](#%E5%AE%9F%E8%A3%85)
  - [素材](#%E7%B4%A0%E6%9D%90)
- [資料](#%E8%B3%87%E6%96%99)
  - [シーケンス図](#%E3%82%B7%E3%83%BC%E3%82%B1%E3%83%B3%E3%82%B9%E5%9B%B3)
  - [画面遷移図](#%E7%94%BB%E9%9D%A2%E9%81%B7%E7%A7%BB%E5%9B%B3)
- [機能](#%E6%A9%9F%E8%83%BD)
  - [オブジェクト操作](#%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E6%93%8D%E4%BD%9C)
  - [釦押下イベント追加](#%E9%87%A6%E6%8A%BC%E4%B8%8B%E3%82%A4%E3%83%99%E3%83%B3%E3%83%88%E8%BF%BD%E5%8A%A0)
  - [部屋情報、プレイヤー情報取得](#%E9%83%A8%E5%B1%8B%E6%83%85%E5%A0%B1%E3%83%97%E3%83%AC%E3%82%A4%E3%83%A4%E3%83%BC%E6%83%85%E5%A0%B1%E5%8F%96%E5%BE%97)
  - [ユーザー名取得](#%E3%83%A6%E3%83%BC%E3%82%B6%E3%83%BC%E5%90%8D%E5%8F%96%E5%BE%97)
- [メモ](#%E3%83%A1%E3%83%A2)
  - [注意](#%E6%B3%A8%E6%84%8F)
  - [課題](#%E8%AA%B2%E9%A1%8C)
  - [未実装](#%E6%9C%AA%E5%AE%9F%E8%A3%85)
- [流用方法](#%E6%B5%81%E7%94%A8%E6%96%B9%E6%B3%95)
  - [必要ファイル](#%E5%BF%85%E8%A6%81%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB)
  - [手順](#%E6%89%8B%E9%A0%86)

<!-- /TOC -->
# デモ
[数字探し](https://little-hoge.github.io/EarlyPush/)  
[![キャプチャ](https://user-images.githubusercontent.com/3638785/77823674-363c7980-7140-11ea-87b7-b7703289c471.PNG)
](https://little-hoge.github.io/EarlyPush/)

# 開発環境
- Windows10 64bit
- unity2019.2.0f1  unity日本語化(https://www.sejuku.net/blog/56333)
- Visual C# 2019
- jdk1.8.0_25
- android-ndk-r13b

# 使用アセット
#### ユーザーデータ管理(必須)
- NCMB(https://github.com/NIFCLOUD-mbaas/ncmb_unity/releases) \
※登録(https://console.mbaas.nifcloud.com/)

#### 通信対戦同期(必須)
- PUN2(https://assetstore.unity.com/packages/tools/network/pun-2-free-119922) \
※登録(https://www.photonengine.com/ja-JP/Photon)

#### バージョン管理(任意)
- Github for Unity(https://miyagame.net/github-for-unity/) \
※登録(https://github.com/) \
※使い方(https://qiita.com/toRisouP/items/97c4cddcb735acde2f03)  

#### WebGL画面サイズ自動調整(任意)
- WebGL responsive template(https://github.com/miguel12345/UnityWebglResponsiveTemplate) \
※WebGLに対応する場合便利  

#### WebGL日本語入力(任意)
- WebGLInput(https://github.com/kou-yeung/WebGLInput) \
※WebGLに対応する場合便利  

# 参考リンク
#### 環境
- インテリセンスが効いていない時の確認  
https://docs.microsoft.com/ja-jp/visualstudio/cross-platform/getting-started-with-visual-studio-tools-for-unity?view=vs-2019#configure-unity-for-use-with-visual-studio

- UnityからAndroid実機ビルド手順(2017.08版)   
https://qiita.com/relzx/items/7f8e7817c9edd11c5023

- 【超初心者】Unityで使えるフォントを増やす方法 Google Font編  
https://yagigame.hatenablog.com/entry/2018/10/25/212020  
※**WebGL**に対応する場合、一部フォントを表示できない為、**必須**


#### 実装
- Unityで数字順押しゲーム  
https://gamegame-game.com/archives/touch_number_1/

- PUN2で始めるオンラインゲーム開発入門【その１】  
https://connect.unity.com/p/pun2deshi-meruonraingemukai-fa-ru-men-sono1

- ロビーに部屋を表示する方法（Unity v2019.1.1f1＆PUN 2）  
https://weeklyhow.com/how-to-display-rooms-in-the-lobby-unity-v2019-1-1f1-pun-2/


#### 素材
- Google Fonts  
https://fonts.google.com/

- OtoLogic  
https://otologic.jp/free/se/quiz01.html

# 資料

#### シーケンス図
![シーケンス](https://user-images.githubusercontent.com/3638785/77221332-36f76d80-6b8c-11ea-949e-8b57c89b72c0.PNG)

#### 画面遷移図
![画面遷移図](https://user-images.githubusercontent.com/3638785/77826004-23ca3c00-7150-11ea-8092-f607e3c227f3.PNG)


# 機能
#### オブジェクト操作  
```C#
using UnityEngine;
using UnityEngine.UI;

// オブジェクト定義
private GameObject GameObject;
private Text TextObj;

// オブジェクト設定
GameObject = GameObject.Find("オブジェクト名");
TextObj = GameObject.Find("オブジェクト名").GetComponent<Text>();

// 更新
TextObj.text = "hogehoge";
```  
#### 釦押下イベント追加  
```C#
using UnityEngine;
using UnityEngine.UI;

// オブジェクト定義
private Button ButtonObj;

// 釦押下時動作設定
ButtonObj = GameObject.Find("オブジェクト名").GetComponent<Button>();
ButtonObj.onClick.AddListener(関数名);
```

#### 部屋情報、プレイヤー情報取得  
```C#
using Photon.Realtime;
 // 部屋情報、プレイヤー情報
 Room myroom = PhotonNetwork.CurrentRoom;
 Player player = PhotonNetwork.LocalPlayer;
```  

#### ユーザー名取得
```C#
using NCMB;
// ユーザー名
NCMBUser.CurrentUser.UserName;
```

# メモ
#### 注意
- PUN2で同期するプレハブは**Resources**配下限定
- [WebGL画面サイズ自動調整(任意)](#webgl%E7%94%BB%E9%9D%A2%E3%82%B5%E3%82%A4%E3%82%BA%E8%87%AA%E5%8B%95%E8%AA%BF%E6%95%B4%E4%BB%BB%E6%84%8F)を使用せずWebGLを使う場合、設定を変える必要あり

#### 課題
- 通信周り
  - 切断側の処理
  - 開始時間同期すべきか否か
- 部屋リスト周り処理
  - 2部屋以上ある状態での入室時のリスト表示
- アニメーション全般  

#### 未実装


# 流用方法
#### 必要ファイル  
- NCMB(https://github.com/NIFCLOUD-mbaas/ncmb_unity/releases)
- PUN2(https://assetstore.unity.com/packages/tools/network/pun-2-free-119922)
- EarlyPush_v1.0.unitypackage(https://github.com/little-hoge/EarlyPush/releases)  

#### 手順
1. アセット＞パッケージをインポートから上記３ファイルをインポート  
※PUN2インポート時に下記が表示されるが閉じて問題ない  
![キャプチャ](https://user-images.githubusercontent.com/3638785/77826110-d9958a80-7150-11ea-8dfa-690f2a5be28f.PNG)
2. プロジェクト内のAsset＞Scenes＞GameLoginをダブルクリックで開く
3. ヒエラルキー内のNCMBSettingsのApplicationKeyとClientKeyを設定する
![キャプチャ](https://user-images.githubusercontent.com/3638785/77194675-bd2b9980-6b23-11ea-8540-a285d5d680a4.PNG)
4. ALT＋PでPUNWizardを表示、LocatePhotonServerSettingsを選択  
※PUN Wizardが表示されない場合、検索ウィンドウにsetと入力後、PhotonServerSettingsを選択　　
5. インスペクターAPPIdRealTimeとAppversionとFixedRegionを入力する
![キャプチャ2](https://user-images.githubusercontent.com/3638785/77139934-35aa4000-6abb-11ea-916f-66a7f8d5aec0.PNG)
![](https://user-images.githubusercontent.com/3638785/77139935-3642d680-6abb-11ea-88a9-bef32395653b.PNG)
6. ファイル＞ビルド設定画面表示した状態で、プロジェクト内の下記Scenesをそれぞれダブルクリックで開き、  
シーンに追加を選択する  
   ※シーンは選択状態時Deleteで削除可能  
   ※GameLobyが開始なので**0**に設定する
    - GameLoby
    - GameLogin
    - GameMain
    - GameRanking
![キャプチャ2](https://user-images.githubusercontent.com/3638785/77194678-be5cc680-6b23-11ea-8f4d-4c463bcaf37c.PNG)
7. GameLoginをダブルクリックで開き、再生を実行する
