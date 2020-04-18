using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberButtonController : MonoBehaviour
{
    public GameDirector GameDirector { get; set; }
    public TimerController TimerController { get; set; }


    // ボタン数値
    public short Number { get; private set; }


    void Start()
    {
        //
        this.GameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        this.TimerController = GameObject.Find("Timer").GetComponent<TimerController>();

        // ボタンクリック時の処理を追加
        this.GetComponent<Button>().onClick.AddListener(OnClick);

        // ボタンのサイズを設定
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(Define.BUTTONSIZE_X, Define.BUTTONSIZE_Y);

    }

    // ボタンの情報を設定する
    public void SetButtonInfos(short number)
    {
        this.Number = number; // 押下時の番号

        /*
        // 画像無し時用
        // 表示設定
        this.GetComponentsInChildren<Text>()[0].text = number.ToString();

        // 文字サイズ自動調整
        this.GetComponentsInChildren<Text>()[0].resizeTextForBestFit = true;
        */

        // 画像オブジェクト名設定
        List<string> ObjectName = new List<string>() { "Text" };

        for (int index = 1; index < Define.EWORD_NUM; index++)
        {
            ObjectName.Add(index.ToString());
        }

        // 該当画像アクティブ化
        foreach (var item in ObjectName)
        {

            // 釦番号と画像番号が一致した時
            if (number.ToString() == item)
            {
                // アクティブ
                transform.Find(item).gameObject.SetActive(true);
            }
            else
            {
                // 非アクティブ
                transform.Find(item).gameObject.SetActive(false);
            }
       
        }

}

    // クリック時の処理
    private void OnClick()
    {
        // 待ち時間
        if (this.TimerController.IsStarted)
        {
            // 出題単語と押下した釦の単語
            var WordType = Function.CheckWordNumber(this.Number);

            // 不一致以外
            if (WordType != ENUM.eWordType.None)
            {
                // 押下した釦の単語
                string NumberWordStr;

                if (WordType == ENUM.eWordType.Normal)
                {
                    NumberWordStr = ((ENUM.eWord)Number).ToString();
                }
                else //if (WordType == ENUM.eWordType.Sub)
                {
                    NumberWordStr = ((ENUM.eSubWord)Number).ToString();
                }

                // 正解音を鳴らす
                SoundManager.Instance.PlaySeByName("Buzzer0");

                
                // スコア加算
                // 末端が"ん"の時
                if (NumberWordStr.Substring(NumberWordStr.Length - 1) == "ん")
                {
                    Data.Instance.P1Score += 5;
                }
                // サブ読み
                else if (WordType== ENUM.eWordType.Sub)
                {
                    Data.Instance.P1Score += 3;
                }
                // 通常読み
                else
                {
                    Data.Instance.P1Score += 1;

                }

                // 正解単語番号、単語タイプを保存
                Data.Instance.AnserWordNumber = this.Number;
                Data.Instance.AnserWordType = WordType;


                // 正解エフェクト表示
                StartCoroutine(DrawJudgeCorrect());


            }
            else
            {
                // 不正解音を鳴らす
                SoundManager.Instance.PlaySeByName("Buzzer1");

                // 経過時間加算
                var timerController = GameObject.Find("Timer").GetComponent<TimerController>();
                timerController.time.msec += timerController.time.sec+Define.PENALTY_TIME;

                // 不正解エフェクト表示
                StartCoroutine(DrawJudgeIncorrec());
            }
        }


    }

    // 正解表示
    IEnumerator DrawJudgeCorrect()
    {
        // 正解の子オブジェクト検索
        var CorrectObj = transform.Find("correct").gameObject;

        // 表示
        CorrectObj.SetActive(true);

        // 一定秒待ち
        yield return new WaitForSeconds(0.5f);

        // 非表示
        CorrectObj.SetActive(false);

        // 正しい番号の場合、ボタンを非アクティブにする
        gameObject.SetActive(false);


    }

    // 不正解表示
    IEnumerator DrawJudgeIncorrec()
    {
        // 不正解の子オブジェクト検索
        var IncorrecObj = transform.Find("incorrec").gameObject;

        // 表示
        IncorrecObj.SetActive(true);

        // 一定秒待ち
        yield return new WaitForSeconds(1.0f);

        // 非表示
        IncorrecObj.SetActive(false);
    }

}