using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Common : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


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
    public const int PLAYR_SINGLE = (1); // 一人プレイ
    public const int PLAYR_MULTI = (2); // 複数プレイ
    public const int ROOM_OWNER =(1);   // 部屋主
    public const int QUESTION_MAX = (7); // 問題出題数
    public const double GAMESTART_DELAYTIME = (3.0); // ゲーム開始待ち時間
    public const int SCREEN_X = (480), SCREEN_Y = (864), FPS = (30);  // 画面設定 
    public const int CREATE_X = (7), CREATE_Y = (5);  // 釦数
    public const int BUTTONSIZE_X = (100), BUTTONSIZE_Y = (100);// ボタンの大きさ
    public const float DRAWCORRECT_COUNT = (1.0f);// 不正解表示時間
    public const float DRAWJUDGECORRECT_COUNT = (0.5f);// 正解表示時間

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
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }
    // ボタン総数
    public static int GetMaxButton()
    {
        return Define.CREATE_X * Define.CREATE_Y;
    }

}


// 共有データ
public class Data
{
    public readonly static Data Instance = new Data();

    // 変数
    public int P1Score, P2Score;    // スコア
    public string P2Name;           // 相手の名前
    public bool GameJudge;          // 決着判定
    public bool Disconnect;          // 切断判定
    public int NowQuestionNumber;   // 出題番号

    // ボタン番号に1～ボタン数設定
    public List<int> ButtonNumber = new List<int>((Enumerable.Range(1, Function.GetMaxButton()).ToArray()));


}

