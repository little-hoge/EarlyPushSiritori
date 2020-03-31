using UnityEngine;
using UnityEngine.UI;
using NCMB;
using Photon.Pun;

public class TextController : MonoBehaviour
{
    private GameDirector GameDirector { get; set; }

    private Text Player1;
    private Text Player2;
    private Text HitNumber;
    private Text ResidualNumber;

    // Start is called before the first frame update
    void Start()
    {
        this.GameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();

        // テキストオブジェクト設定
        Player1 = GameObject.Find("Player1").GetComponent<Text>();
        Player2 = GameObject.Find("Player2").GetComponent<Text>();
        HitNumber = GameObject.Find("HitNumber").GetComponent<Text>();
        ResidualNumber = GameObject.Find("ResidualNumber").GetComponent<Text>();

        // 初期設定
        Player1.text = "";
        Player2.text = "";
        HitNumber.text = "";
        ResidualNumber.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        // 複数プレイ
        if (PhotonNetwork.CurrentRoom.MaxPlayers != Define.PLAYR_SINGLE)
        {
            Player2.text = string.Format("{0}\n{1}", Data.Instance.P2Name, Data.Instance.P2Score);
        }

        Player1.text = string.Format("{0}\n{1}", NCMBUser.CurrentUser.UserName, Data.Instance.P1Score);
        HitNumber.text = string.Format("出題番号\n{0}", Data.Instance.NowQuestionNumber);
        ResidualNumber.text = string.Format("残り\n{0}", this.GameDirector.GetCleared());


    }
}
