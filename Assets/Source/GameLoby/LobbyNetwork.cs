using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using NCMB;

public class LobbyNetwork : MonoBehaviourPunCallbacks
{
    public static LobbyNetwork Instance;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {

        // PhotonNetwork接続
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("PhotonNetwork接続");
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.GameVersion = "0.0.0";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("PhotonNetwork接続済み");
        }

    }


    // ロビー入室時に呼ばれるコールバック
    public override void OnJoinedLobby()
    {

        if (!PhotonNetwork.InRoom)
        {
            Debug.Log("ロビーに入室");

        }
    }

    // マスターサーバーに接続が確立時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーに接続");

        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.NickName = NCMBUser.CurrentUser.UserName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    // 他プレイヤーが参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player player)
    {
        Debug.Log(player.NickName + "が参加しました");

        // ゲームメイン(マルチプレイ)へ
        SceneManager.LoadScene(Define.GAME_MAIN);

        // マッチング後、自分自身のネットワークオブジェクトを生成する
        PhotonNetwork.Instantiate(Define.PLAYER, Vector3.zero, Quaternion.identity);
        Debug.Log("ホスト" + string.Format("{0}", PhotonNetwork.PhotonViews));

        // 対戦数更新保存
        QuickRanking.Instance.mRankingData.gametotal++;
        QuickRanking.Instance.UserDataUpdate();

    }


}