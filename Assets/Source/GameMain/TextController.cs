using UnityEngine;
using UnityEngine.UI;
using NCMB;
using Photon.Pun;
using System.Text.RegularExpressions;

public class TextController : MonoBehaviour
{
    private GameDirector GameDirector { get; set; }

    private Text Player1;
    private Text Player2;
    private Text QuestionWord;

    // Start is called before the first frame update
    void Start()
    {
        this.GameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();

        // テキストオブジェクト設定
        Player1 = GameObject.Find("Player1").GetComponent<Text>();
        Player2 = GameObject.Find("Player2").GetComponent<Text>();
        QuestionWord = GameObject.Find("QuestionWord").GetComponent<Text>();

        // 初期設定
        Player1.text = "";
        Player2.text = "";
        QuestionWord.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        var NowQuestionWord = ((ENUM.eWord)Data.Instance.NowQuestionWordNumber).ToString();

        // お題が単語(サブ読み)だった時
        if (Data.Instance.NowQuestionWordType == ENUM.eWordType.Sub)
        {
            NowQuestionWord = ((ENUM.eSubWord)Data.Instance.NowQuestionWordNumber).ToString();
        }

        // お題(終端文字)が長音の場合無視する
        var NowQuestionWordEnd = Regex.Replace(NowQuestionWord, "ー", "");
        NowQuestionWordEnd = NowQuestionWordEnd.Substring(NowQuestionWordEnd.Length - 1);

        // 複数プレイ
        if (PhotonNetwork.CurrentRoom.MaxPlayers != Define.PLAYR_SINGLE)
        {
            Player2.text = string.Format("{0}\n{1}/{2}", Data.Instance.P2Name, Data.Instance.P2Score, Define.SCORE_MAX);
        }

        Player1.text = string.Format("{0}\n{1}/{2}", NCMBUser.CurrentUser.UserName, Data.Instance.P1Score, Define.SCORE_MAX);
        QuestionWord.text = string.Format("お題\n{0}→「{1}」", NowQuestionWord, NowQuestionWordEnd);

    }
}
