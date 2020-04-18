using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using NCMB;
using System.Collections.Generic;
using System;

public class GamePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    // 釦プレハブ
    public NumberButtonGenerator NumberButtonGeneratorPrefab;

    // 前回値
    private short Last_AnserWordNumber; 

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

        // 開始単語設定
        Last_AnserWordNumber = Data.Instance.AnserWordNumber;

        // 自分
        if (photonView.IsMine)
        {
            // プレイヤー1(ホスト)から
            if (photonView.OwnerActorNr == Define.ROOM_OWNER)
            {
                // 釦シャッフル設定
                NumberButtonGeneratorPrefab.NumberButtonsWordReset();
                Debug.Log("釦番号" + string.Join(", ", Data.Instance.ButtonNumber));

            }

        }

    }

    private void Update()
    {
        // 部屋情報
        Room myroom = PhotonNetwork.CurrentRoom;

        // プレイヤー1(ホスト)から
        if (photonView.OwnerActorNr == Define.ROOM_OWNER)
        {
            Data.Instance.NowQuestionWordNumber = Data.Instance.AnserWordNumber;
            Data.Instance.NowQuestionWordType = Data.Instance.AnserWordType;

            Debug.Log("お題：" + Data.Instance.NowQuestionWordNumber);



        }


        // 対戦相手が決着前にいなくなった時
        if ((myroom.PlayerCount != myroom.MaxPlayers) && !Data.Instance.GameJudge)
        {
            // 不戦勝にする
            Data.Instance.P1Score = Define.SCORE_MAX;
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
                // 釦表示順を送信
                stream.SendNext(Data.Instance.ButtonNumber.ToArray());
                // Debug.Log("送信" + string.Join(", ", Data.Instance.ButtonNumber));

            }
            // 自分のスコア/名前を送信/正解単語番号/単語タイプ
            stream.SendNext(Data.Instance.P1Score);
            stream.SendNext(NCMBUser.CurrentUser.UserName);


            // 正解した時のみ送信
            if (Last_AnserWordNumber != Data.Instance.AnserWordNumber)
            {
                stream.SendNext(Data.Instance.AnserWordNumber);
                stream.SendNext(Data.Instance.AnserWordType);
                Last_AnserWordNumber = Data.Instance.AnserWordNumber;
              //  Debug.Log("送信答えられるか：" + Data.Instance.P2AnserNone);

            }


        }
        // 受信
        else
        {

            // プレイヤー1(ホスト)から
            if (photonView.OwnerActorNr == Define.ROOM_OWNER)
            {
                // 釦表示順
                Data.Instance.ButtonNumber.Clear();
                Data.Instance.ButtonNumber.AddRange((short[])stream.ReceiveNext());
                //Debug.Log("受信釦" + string.Join(", ", Data.Instance.ButtonNumber));

            }

            // スコア名前を受信
            Data.Instance.P2Score = (short)stream.ReceiveNext();
            Data.Instance.P2Name = (string)stream.ReceiveNext();

            // 正解があった時
            if (stream.Count > Define.NOUPDATE_DATANUM)
            {
                Data.Instance.AnserWordNumber = (short)stream.ReceiveNext();
                Data.Instance.AnserWordType = ((ENUM.eWordType)stream.ReceiveNext());
               // Debug.Log("受信答えられるか：" + Data.Instance.P2AnserNone);
            }

            
        }
    }

}

