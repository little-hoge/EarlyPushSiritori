using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using NCMB;
public class LobbyFunction : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListDisplay roomListDisplay = default;

    private RoomListDisplay RoomListDisplay
    {
        get { return roomListDisplay; }
    }

    // 他プレイヤーが参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player player)
    {
        Debug.Log("参加");

        // 満員になった時、リスト非表示途中参加不可
        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("部屋満員");
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

    }

    //　部屋に入室した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // プレイヤー数が揃った場合
        if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            // 複数人プレイの場合
            if (PhotonNetwork.CurrentRoom.MaxPlayers > Define.PLAYR_MULTI)
            {
                // 対戦数更新保存
                QuickRanking.Instance.mRankingData.gametotal++;
                QuickRanking.Instance.UserDataUpdate();
            }

            // ゲームメインへ
            SceneManager.LoadScene(Define.GAME_MAIN);

            // ★
            // 自身のネットワークオブジェクトを生成する
            PhotonNetwork.Instantiate(Define.PLAYER, Vector3.zero, Quaternion.identity);

        }
    }
    // 部屋参加時に呼び出し
    public void OnClickRoom(string roomName)
    {

        if (PhotonNetwork.JoinRoom(roomName))
        {
            Debug.Log("部屋に参加");

        }
        else
        {
            Debug.LogWarning("ルームへの参加に失敗しました");
        }
    }


}