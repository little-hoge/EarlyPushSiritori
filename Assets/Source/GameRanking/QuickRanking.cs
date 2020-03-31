using NCMB;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System.Threading.Tasks;
using Photon.Pun;

public class QuickRanking : MonoBehaviour
{
    private List<RankingData> rankingDataList = new List<RankingData>();// 取得したランキングデータ
    private bool IsRankingDataValid; // ランキングデータの取得に成功しているか

    public static QuickRanking Instance;
    public RankingData mRankingData = new RankingData();


    public void Awake()
    {

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // シーン切り替え後、オブジェクト引き継ぎ
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            // 接続確認
            CheckNCMBValid();
        }
    }
    void Start()
    {
        DataLoadorCreate();
        DBRankingLoad();
    }


    // データ読み込みまたは作成
    public void DataLoadorCreate()
    {

        // データストアの名前をキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(Define.DATASTORENAME);
        query.WhereEqualTo("Name", NCMBUser.CurrentUser.UserName);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {

            // 検索成功したら
            if (e == null)
            {
                // 未登録の時、データ新規作成
                if (objList.Count == 0)
                {
                    NCMBObject obj = new NCMBObject(Define.DATASTORENAME);
                    obj["Name"] = NCMBUser.CurrentUser.UserName;
                    obj["Time"] = "59:59.99";
                    obj["Gametotal"] = 0;
                    obj["Gamewin"] = 0;
                    obj["Gamelose"] = 0;
                    obj.SaveAsync();

                    // データ設定
                    mRankingData.id = NCMBUser.CurrentUser.ObjectId;
                    mRankingData.name = NCMBUser.CurrentUser.UserName;
                    mRankingData.time = "59:59.99";
                }
                // データ登録済みの時
                else
                {
                    mRankingData.id = System.Convert.ToString(objList[0].ObjectId);// ★
                    mRankingData.name = System.Convert.ToString(objList[0]["Name"]);
                    mRankingData.time = System.Convert.ToString(objList[0]["Time"]);
                    mRankingData.gametotal = System.Convert.ToInt32(objList[0]["Gametotal"]);
                    mRankingData.gamewin = System.Convert.ToInt32(objList[0]["Gamewin"]);
                    mRankingData.gamelose = System.Convert.ToInt32(objList[0]["Gamelose"]);
                }
            }


        });



    }

    // ランキングデータ読み込み
    public void DBRankingLoad()
    {

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(Define.DATASTORENAME);

        // Scoreの値で降順にソート
        query.OrderByDescending("Gametotal");

        // 取得数の設定
        query.Limit = Define.DRAWLIST_MAX;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                // 検索失敗時の処理
                IsRankingDataValid = false;
            }
            else
            {
                rankingDataList.Clear();

                foreach (NCMBObject obj in objList)
                {
                    rankingDataList.Add(new RankingData(
                         name: obj["Name"] as string,
                         time: obj["Time"] as string,
                         gametotal: Convert.ToInt32(obj["Gametotal"]),
                         gamewin: Convert.ToInt32(obj["Gamewin"]),
                         gamelose: Convert.ToInt32(obj["Gamelose"])

                        ));
                }

                IsRankingDataValid = true;
            }

        });

    }


    // データ更新
    public void UserDataUpdate()
    {
        // データストアの名前をキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(Define.DATASTORENAME);
        query.WhereEqualTo("Name", NCMBUser.CurrentUser.UserName);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            // 検索成功したら
            if (e == null)
            {
                // 名前と一致したユーザー更新
                foreach (NCMBObject obj in objList)
                {
                    obj["Time"] = mRankingData.time;
                    obj["Gametotal"] = mRankingData.gametotal;
                    obj["Gamewin"] = mRankingData.gamewin;
                    obj["Gamelose"] = mRankingData.gamelose;
                    obj.SaveAsync();
                }
            }
        });

    }


    // ランキング表示文字列取得
    public string GetRankingText(RANKING_TEXT_TYPE texttype)
    {
        // データ取得有無
        if (IsRankingDataValid)
        {
            int count = 1;
            string text = string.Empty;


            foreach (RankingData rankingData in rankingDataList)
            {
                // 書式設定
                string name = string.Format("{0, 0}", rankingData.name);
                string time = string.Format("{0, 0}", rankingData.time);
                string gametotal = string.Format("{0, 4}", rankingData.gametotal);
                string gamewin = string.Format("{0, 4}", rankingData.gamewin);
                string gamelose = string.Format("{0, 4}", rankingData.gamelose);


                // ユーザー名情報
                if (texttype == RANKING_TEXT_TYPE.NAME)
                {
                    // 自分のデータを赤にする
                    if (rankingData.name == mRankingData.name)
                    {
                        text += "<color=red>" + count + "." + name + "</color>" + "\n";
                        text += "<color=red>" + "Time：" + time + "</color>";
                    }
                    else
                    {
                        text += count + "." + name + "\n";
                        text += "Time：" + time;
                    }
                    // 最終行手前まで改行追加
                    if (rankingDataList.Count > count++)
                    {
                        text += "\n\n";
                    }
                }
                // スコア情報
                else if (texttype == RANKING_TEXT_TYPE.SCORE)
                {

                    // 自分のデータを赤にする
                    if (rankingData.name == mRankingData.name)
                    {
                        text += "<color=red>" + gametotal + "\t" + gamewin + "\t" + gamelose + "</color>";
                    }
                    else
                    {
                        text += gametotal + "\t" + gamewin + "\t" + gamelose;
                    }
                    // 最終行手前まで改行追加
                    if (rankingDataList.Count > count++)
                    {
                        text += "\n\n\n";
                    }
                }
            }
            return text;

        }
        else
        {
            return "データがありません";
        }

    }

    // ゲーム負数降順
    public void GametotalSort()
    {
        rankingDataList.Sort((data1, data2) => data2.gametotal - data1.gametotal);
    }

    // ゲーム勝数降順
    public void GamewinSort()
    {
        rankingDataList.Sort((data1, data2) => data2.gamewin - data1.gamewin);
    }

    // ゲームプレイ数降順
    public void GameloseSort()
    {
        rankingDataList.Sort((data1, data2) => data2.gamelose - data1.gamelose);
    }

    // クリア時間昇順
    public void GameTimeSort()
    {
        rankingDataList.Sort((data1, data2) =>
        int.Parse(data1.time.Replace(":", "").Replace(".", ""))
        - int.Parse(data2.time.Replace(":", "").Replace(".", "")));
    }

    // プレイヤー数を返す
    public int GetPlayerCount()
    {
        return rankingDataList.Count;
    }



    private bool CheckNCMBValid()
    {
#if UNITY_WEBGL

            Debug.LogWarning("NCMB SDK はWebGLに対応していません。");
            return false;
#else
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogError("ネットワーク接続がありません。");
            return false;
        }
        else
        {
            return true;
        }
#endif
    }


}

// ランキングデータ
public class RankingData
{
    public string id;// ID
    public string name;// プレイヤー名
    public string time;// クリア時間
    public int gametotal;// 試合数
    public int gamewin;// 勝ち数
    public int gamelose;// 負け数

    public RankingData()
    {
    }

    public RankingData(string name, string time, int gametotal, int gamewin, int gamelose)
    {
        this.name = name;
        this.time = time;
        this.gametotal = gametotal;
        this.gamewin = gamewin;
        this.gamelose = gamelose;
    }
}

