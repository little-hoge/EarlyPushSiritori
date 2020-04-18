using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using NCMB;
using System.Linq;

public class GameDirector : MonoBehaviour
{
    // 結果表示用UIのPreafab
    private GameObject ResultTextPrefab;

    void Start()
    {
        // プレハブ設定
        ResultTextPrefab = GameObject.Find("ResultText");
    }


    void Update()
    {
        // クリア判定
        ClearGameIf();

        // クリア時にクリックされたら
        if (Data.Instance.GameJudge == true && Input.GetMouseButtonDown(0))
        {
            // 部屋を退出
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(Define.GAME_LOBY);

        }

    }

    // クリア処理
    public void ClearGameIf()
    {
        // 得点を一定数取った場合
        if (this.GetCleared())
        {
            // タイマーを止める
            var timerController = GameObject.Find("Timer").GetComponent<TimerController>();
            timerController.IsStarted = false;

            // 未決着
            if (!Data.Instance.GameJudge)
            {
                // 結果を表示 
                var time = timerController.time;
                var resultTextObject = Instantiate(this.ResultTextPrefab) as GameObject;

                // 一人プレイ
                if (PhotonNetwork.CurrentRoom.MaxPlayers == Define.PLAYR_SINGLE)
                {
                    var resultText = string.Format("{0:00}:{1:00}.{2:00}", time.min, time.sec, (time.msec * 0.1));
                    resultTextObject.GetComponent<Text>().text = "Time：" + resultText;

                    // 記録を更新した場合保存
                    if (NewRecordJudge(resultText))
                    {
                        QuickRanking.Instance.mRankingData.time = resultText;
                        QuickRanking.Instance.UserDataUpdate();
                    }
                }
                // マルチプレイ
                else
                {
                    // 勝敗判定
                    if (Data.Instance.P1Score > Data.Instance.P2Score)
                    {
                        // 不戦勝
                        if (Data.Instance.Disconnect)
                        {
                            resultTextObject.GetComponent<Text>().text += string.Format("切断されました。\n", Data.Instance.P2Name);
                        }

                        // 結果表示後、データ更新
                        resultTextObject.GetComponent<Text>().text += string.Format("{0}の勝ち", NCMBUser.CurrentUser.UserName);
                        QuickRanking.Instance.mRankingData.gamewin++;
                        QuickRanking.Instance.UserDataUpdate();

                    }
                    // 敗
                    else
                    {
                        // 結果表示後、データ更新
                        resultTextObject.GetComponent<Text>().text += string.Format("{0}の勝ち", Data.Instance.P2Name);
                        QuickRanking.Instance.mRankingData.gamelose++;
                        QuickRanking.Instance.UserDataUpdate();

                    }
                    // ランキングデータ更新
                    QuickRanking.Instance.DBRankingLoad();

                }

                // キャンバスの子として表示
                resultTextObject.GetComponent<Transform>().SetParent(
                    GameObject.Find("Canvas").GetComponent<Transform>()
                    );
                resultTextObject.transform.localPosition = new Vector3(0, 0, 0);

                // 決着
                Data.Instance.GameJudge = true;
            }
        }
    }

    // 時間データ更新判定
    public static bool NewRecordJudge(string NowTimeStr)
    {
        var RecordTime = int.Parse(QuickRanking.Instance.mRankingData.time.Replace(":", "").Replace(".", ""));
        var NowTime = int.Parse(NowTimeStr.Replace(":", "").Replace(".", ""));

        // 新しい時間が早い時
        if (RecordTime > NowTime)
        {
            return true;
        }
        else
        {
            return false;
        }

    }



    // クリアの判定
    public bool GetCleared()
    {
        return (Data.Instance.P1Score >= Define.SCORE_MAX) || (Data.Instance.P2Score >= Define.SCORE_MAX);
    }

}