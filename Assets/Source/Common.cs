using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ENUM;
using System.Text.RegularExpressions;

public class Common : MonoBehaviour
{
   

}


// PC画面サイズ設定
public class GameInitial
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(Define.SCREEN_X, Define.SCREEN_Y, false, Define.FPS);

    }

}


public static class Define
{
    // 定数
    public const string GAME_LOGIN = ("GameLogin");
    public const string GAME_LOBY = ("GameLoby");
    public const string GAME_MAIN = ("GameMain");
    public const string GAME_RANKING = ("GameRanking");
    public const string PLAYER = ("GamePlayer");

    // 
    public const int PLAYR_SINGLE = (1);    // 一人プレイ
    public const int PLAYR_MULTI = (2);     // 複数プレイ
    public const int ROOM_OWNER =(1);       // 部屋主
    public const int SCORE_MAX = (20);   // 問題出題数
    public const int NOUPDATE_DATANUM = (6); // データ数
    public const double GAMESTART_DELAYTIME = (3.0); // ゲーム開始待ち時間
    public const int PENALTY_TIME = (3000);     // 不正解加算時間
    public const int SCREEN_X = (480), SCREEN_Y = (864), FPS = (30);// 画面設定 
    public const int CREATE_X = (5), CREATE_Y = (6);                // 釦数
    public const int BUTTONSIZE_X = (100), BUTTONSIZE_Y = (100);    // ボタンの大きさ
    public const float DRAWCORRECT_COUNT = (1.0f);// 不正解表示時間
    public const float DRAWJUDGECORRECT_COUNT = (0.5f);// 正解表示時間
    public static int EWORD_NUM = Enum.GetNames(typeof(ENUM.eWord)).Length;// 単語数
    public static int EWORDTYPE_NUM = Enum.GetNames(typeof(eWordType)).Length;// 単語タイプ数

    // NCMB
    public const string DATASTORENAME = ("Ranking");// ランキングクラス名
    public const int DRAWLIST_MAX = (20);// 最大いくつまでランキングデータを取得するか



}

public static class Function
{
    // シャッフル
    public static List<T> Shuffle<T>(this List<T> list)
    {

        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }

    // ボタン総数
    public static short GetMaxButton()
    {
        return Define.CREATE_X * Define.CREATE_Y;
    }


    // クリックした値の正誤判定
    // 0:不一致　1:単語一致　2:サブ単語一致　
    public static eWordType CheckWordNumber(int number)
    {
        // 出題単語と押下した釦の単語
        var NowQuestionWord = ((eWord)Data.Instance.NowQuestionWordNumber).ToString();
        var ButtonWord = ((eWord)number).ToString();
        var ButtonSubWord = ((eSubWord)number).ToString();

        // お題が単語(サブ読み)だった時
        if (Data.Instance.NowQuestionWordType == eWordType.Sub) {
            NowQuestionWord = ((eSubWord)Data.Instance.NowQuestionWordNumber).ToString();
        }

        // お題(終端文字)が長音の場合無視する
        NowQuestionWord = Regex.Replace(NowQuestionWord, "ー", "");

        // お題(終端文字)と釦の単語(先頭文字)を比較
        if (NowQuestionWord.Substring(NowQuestionWord.Length - 1) == ButtonWord.Substring(0, 1))
        {
            return eWordType.Normal;
        }
        else if (NowQuestionWord.Substring(NowQuestionWord.Length - 1) == ButtonSubWord.Substring(0, 1))
        {
            return eWordType.Sub;
        }
        else 
        {
            return eWordType.None;

        }
    

    }

}


// 共有データ
public class Data
{
    public readonly static Data Instance = new Data();

    // 変数
    public short P1Score, P2Score;       // スコア
    public string P2Name;                // 相手の名前
    public bool GameJudge;               // 決着判定
    public bool Disconnect;              // 切断判定
    public short NowQuestionWordNumber, AnserWordNumber;  // お題単語番号、答えた単語番号
    public ENUM.eWordType NowQuestionWordType, AnserWordType; // お題単語タイプ、答えた単語タイプ


    // 登録されている単語を設定
    public List<short> ButtonNumber = new List<short>(
        Enumerable.Range(1, Function.GetMaxButton())
        .Select(num => (short)num).ToArray());


}

namespace ENUM
{
    // 単語読み方タイプ
    public enum eWordType
    {
        None,
        Normal,
        Sub,
    };

    // 単語(通常読み)

    public enum eWord
    {
        しりとり,
        りんご,
        ごみばこ,
        こいん,
        こま,
        まいく,
        くりすます,
        くれじっとかーど,
        どんぶり,
        どらごんぼーる,
        りんぐ,
        ぐらふ,
        ふぁっくす,
        すーつ,
        まうす,
        つき,
        めだまやき,
        にんじん,
        すいか,
        かば,
        かるて,
        ばんそうこう,
        うぃんなー,
        ほうちょう,
        かちんこ,
        がいこつ,
        ゆきだるま,
        ききゅう,
        たんてい,
        よっと,
        とびばこ,
        こーひーかっぷ,
        ぷりんたー,
        わいふぁい,
        なまはむ,
        のーとぱそこん,



    };

    // 単語(サブ読み)

    public enum eSubWord
    {
        しりとり,
        あっぷる,
        ばけつ,
        かじの,
        えんぎもの,
        こんでんさまいく,
        もみのき,
        かーど,
        おわん,
        いーしんちゅう,
        ゆびわ,
        ぼうぐらふ,
        でんわ,
        れいふく,
        にゅうりょくきき,
        むーん,
        ふらいぱん,
        きゃろっと,
        うぉーたーめろん,
        どうぶつ,
        しりょう,
        さびお,
        そーせーじ,
        ないふ,
        えいが,
        ほらー,
        ふゆ,
        ばるーん,
        ぐらさん,
        でぃんぎー,
        たいいく,
        まぐかっぷ,
        いんさつき,
        むせんらん,
        はむ,
        ぱそこん,

    };


}