# 目次

<!-- TOC -->

- [目次](#%E7%9B%AE%E6%AC%A1)
- [ルール](#%E3%83%AB%E3%83%BC%E3%83%AB)
- [デモ](#%E3%83%87%E3%83%A2)
- [開発環境](#%E9%96%8B%E7%99%BA%E7%92%B0%E5%A2%83)
- [使用アセット](#%E4%BD%BF%E7%94%A8%E3%82%A2%E3%82%BB%E3%83%83%E3%83%88)
  - [ユーザーデータ管理(必須)](#%E3%83%A6%E3%83%BC%E3%82%B6%E3%83%BC%E3%83%87%E3%83%BC%E3%82%BF%E7%AE%A1%E7%90%86%E5%BF%85%E9%A0%88)
  - [通信対戦同期(必須)](#%E9%80%9A%E4%BF%A1%E5%AF%BE%E6%88%A6%E5%90%8C%E6%9C%9F%E5%BF%85%E9%A0%88)
  - [バージョン管理(任意)](#%E3%83%90%E3%83%BC%E3%82%B8%E3%83%A7%E3%83%B3%E7%AE%A1%E7%90%86%E4%BB%BB%E6%84%8F)
  - [WebGL画面サイズ自動調整(任意)](#webgl%E7%94%BB%E9%9D%A2%E3%82%B5%E3%82%A4%E3%82%BA%E8%87%AA%E5%8B%95%E8%AA%BF%E6%95%B4%E4%BB%BB%E6%84%8F)
  - [WebGL日本語入力(任意)](#webgl%E6%97%A5%E6%9C%AC%E8%AA%9E%E5%85%A5%E5%8A%9B%E4%BB%BB%E6%84%8F)
- [参考リンク](#%E5%8F%82%E8%80%83%E3%83%AA%E3%83%B3%E3%82%AF)
  - [流用](#%E6%B5%81%E7%94%A8)
  - [実装](#%E5%AE%9F%E8%A3%85)
  - [素材](#%E7%B4%A0%E6%9D%90)

<!-- /TOC -->

# ルール
- 読み方は複数種類  
例 りんご、アップル等

- 得点  
  基本読み +1点  
  応用読み +3点  
  「ん」で終わる読み +5点  
  不正解 +３秒  

- 語尾の長音は無視  
例 りーだーの場合、「だ」から



# デモ
[早押ししりとり](https://little-hoge.github.io/EarlyPushSiritori/)  
[![キャプチャ](https://user-images.githubusercontent.com/3638785/79637831-32d76380-81bd-11ea-8e99-8e20fce42f1e.PNG)](https://little-hoge.github.io/EarlyPushSiritori/)


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
#### 流用
- EarlyPush  
https://github.com/little-hoge/EarlyPush

#### 実装
- スーパーワギャンランド2  
※早押ししりとり

#### 素材
- Googleフォント  
https://fonts.google.com/

- OtoLogic  
https://otologic.jp/free/se/quiz01.html

- SoundManagerのC＃スクリプト  
https://00m.in/Lp0Up

- FLAT ICON DESIGN  
http://flat-icon-design.com/
