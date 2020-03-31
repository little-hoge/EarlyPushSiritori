using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using NCMB;

public class LobyButtonController : MonoBehaviourPun
{
    [SerializeField]
    private Text roomName = default;
    private Text RoomName
    {
        get { return roomName; }
    }

    // 表示非/表示変更ボタン
    private GameObject RoomOutButton;
    private GameObject CreateRoomButton;
    private GameObject RandomJoinButton;


    private void Start()
    {
        RoomOutButton = GameObject.Find("Button_RoomOut");
        CreateRoomButton = GameObject.Find("Button_CreateRoom");
        RandomJoinButton = GameObject.Find("Button_RandomJoin");
        // 非表示
        RoomOutButton.SetActive(false);

    }


    // 新規部屋作成入室
    public void OnClick_CreateRoom()
    {
        Debug.Log("部屋作成");

        string roomNameStr;

        // ロビー情報設定(ロビーにリスト表示、部屋に参加許可、人数制限)
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };

        // 部屋書式設定
        if (string.IsNullOrEmpty(roomName.text))
        {
            roomNameStr = string.Format("部屋名：{0}\nユーザー名：{1}", "NoName", NCMBUser.CurrentUser.UserName);
        }
        else
        {
            roomNameStr = string.Format("部屋名：{0}\nユーザー名：{1}", roomName.text, NCMBUser.CurrentUser.UserName);
        }

        // 部屋作成済みの場合
        if (PhotonNetwork.CurrentRoom == null)
        {
            // 新規部屋作成
            if (PhotonNetwork.CreateRoom(roomNameStr, roomOptions, TypedLobby.Default))
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = "新規部屋作成に成功しました。\n対戦相手参加待ち";

                // 釦状態更新
                RoomOutButton.SetActive(true);
                CreateRoomButton.SetActive(false);
                RandomJoinButton.SetActive(false);
            }
            else
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = "新規部屋作成に失敗しました。";

            }
        }

    }

    // ランダムな部屋に入室
    public void OnClick_JoinRandomRoom()
    {
        if (PhotonNetwork.JoinRandomRoom() == false)
        {
            GameObject.Find("Text_Log").GetComponent<Text>().text = "ランダム入室に成功しました。";
        }
        else
        {
            GameObject.Find("Text_Log").GetComponent<Text>().text = "ランダム入室に失敗しました。";
        }

    }

    // 部屋を退出
    public void OnClick_RoomOut()
    {
        // 部屋作成済みの場合
        if (PhotonNetwork.CurrentRoom != null)
        {
            if (PhotonNetwork.LeaveRoom())
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = "部屋を退出しました。";
                // 釦状態更新
                RoomOutButton.SetActive(false);
                CreateRoomButton.SetActive(true);
                RandomJoinButton.SetActive(true);

            }
            else
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = "退出に失敗しました。";
            }
        }

    }

    // ログアウト
    public void OnClick_Logout()
    {
        // 部屋作成済みの場合退出
        OnClick_RoomOut();

        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogWarning("ログアウトに失敗: " + e.ErrorMessage);
            }
            else
            {
                Debug.Log("ログアウトに成功");
                SceneManager.LoadScene(Define.GAME_LOGIN);
            }
        });

        // ユーザー情報破棄
        if (GameObject.Find("QuickRanking") != null)
        {
            Destroy(GameObject.Find("QuickRanking"));
        }

    }

    // ランキング
    public void OnClick_Ranking()
    {
        // 部屋作成済みの場合退出
        OnClick_RoomOut();

        // ランキングへ
        SceneManager.LoadScene(Define.GAME_RANKING);

    }

    // 一人プレイ
    public void OnClick_SinglePlay()
    {
        // 部屋作成済みの場合退出
        OnClick_RoomOut();

        // ロビー情報設定(ロビーにリスト表示、部屋に参加許可、人数制限)
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, IsOpen = false, MaxPlayers = 1 };

        // 新規部屋作成 ※一意の部屋名にするためユーザー名で部屋を建てる
        if (PhotonNetwork.CreateRoom(NCMBUser.CurrentUser.UserName, roomOptions, TypedLobby.Default))
        {
            GameObject.Find("Text_Log").GetComponent<Text>().text = "一人で遊ぶに成功しました。";
        }
        else
        {
            GameObject.Find("Text_Log").GetComponent<Text>().text = "一人で遊ぶに失敗しました。";
        }


    }

}