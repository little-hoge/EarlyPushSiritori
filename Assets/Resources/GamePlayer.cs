using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using NCMB;
using System.Collections.Generic;

public class GamePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    // パネル情報
    public NumberButtonGenerator NumberButtonGeneratorPrefab;

    // 出題番号に1～ボタン数設定
    private int QuestionIndex = Data.Instance.P1Score + Data.Instance.P2Score;
    private List<int> QuestionNumber = new List<int>(Enumerable.Range(1, Function.GetMaxButton()).ToArray());

    private void Start()
    {
        Debug.Log("GamePlayer生成");

        // シーン切り替え後、オブジェクト引き継ぎ
        DontDestroyOnLoad(gameObject);

        // パラメータ初期化
        Data.Instance.Disconnect = false;
        Data.Instance.GameJudge = false;
        Data.Instance.P1Score = 0;
        Data.Instance.P2Score = 0;
        Data.Instance.P2Name = "";

        // 自分
        if (photonView.IsMine)
        {
            // プレイヤー1(ホスト)から
            if (photonView.OwnerActorNr == Define.ROOM_OWNER)
            {
                // 出題番号シャッフル
                Function.Shuffle(QuestionNumber);
                Debug.Log("出題番号" + string.Join(", ", QuestionNumber));

                // パネル番号シャッフル
                Function.Shuffle(Data.Instance.ButtonNumber);
                Debug.Log("パネル番号" + string.Join(", ", Data.Instance.ButtonNumber));

            }

        }

    }

    private void Update()
    {
        // 部屋情報
        Room myroom = PhotonNetwork.CurrentRoom;

        // 出題番号
        QuestionIndex = Data.Instance.P1Score + Data.Instance.P2Score;

        // プレイヤー1(ホスト)から
        if (photonView.OwnerActorNr == Define.ROOM_OWNER)
        {
            // の出題番号に同期
            Data.Instance.NowQuestionNumber = QuestionNumber[QuestionIndex];
            //DebugLogger.Log("出題番号更新" + string.Join(", ", QuestionNumber));

        }

        // 対戦相手が決着前にいなくなった時
        if ((myroom.PlayerCount != myroom.MaxPlayers) && !Data.Instance.GameJudge)
        {
            // 不戦勝にする
            Data.Instance.P1Score = Define.QUESTION_MAX;
            Data.Instance.P2Score = 0;
            Data.Instance.Disconnect = true;
        }

    }



    // データを送受信するメソッド
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        // 送信
        if (photonView.IsMine)
        {
            // プレイヤー1(ホスト)から
            if (photonView.OwnerActorNr == Define.ROOM_OWNER)
            {
                //出題番号順、パネル表示順を送信
                stream.SendNext(QuestionNumber.ToArray());
                stream.SendNext(Data.Instance.ButtonNumber.ToArray());
                //  DebugLogger.Log("送信" + string.Join(", ", Data.Instance.ButtonNumber));


            }

            // 自分のスコア名前を送信
            stream.SendNext(Data.Instance.P1Score);
            stream.SendNext(NCMBUser.CurrentUser.UserName);


        }
        // 受信
        else
        {

            // プレイヤー1(ホスト)から
            if (photonView.OwnerActorNr == Define.ROOM_OWNER)
            {
                // 出題番号順
                QuestionNumber.Clear();
                QuestionNumber.AddRange((int[])stream.ReceiveNext());
                //DebugLogger.Log("受信出題番号" + string.Join(", ", QuestionNumber));

                // パネル表示順
                Data.Instance.ButtonNumber.Clear();
                Data.Instance.ButtonNumber.AddRange((int[])stream.ReceiveNext());
                //DebugLogger.Log("受信パネル" + string.Join(", ", Data.Instance.ButtonNumber));
            }
            // スコア名前を受信
            Data.Instance.P2Score = (int)stream.ReceiveNext();
            Data.Instance.P2Name = (string)stream.ReceiveNext();

        }
    }

}

