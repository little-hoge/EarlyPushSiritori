using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NumberButtonController : MonoBehaviour
{
    public GameDirector GameDirector { get; set; }
    public TimerController TimerController { get; set; }


    // ボタンに表示されるテキストと数値
    public string Text { get; set; }
    public int Number { get; set; }


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
    public void SetButtonInfos(int number, string text)
    {
        this.Number = number; // 押下時の番号
        this.Text = text;

        // 表示設定
        this.GetComponentsInChildren<Text>()[0].text = text;

        // 文字サイズ自動調整
        this.GetComponentsInChildren<Text>()[0].resizeTextForBestFit = true;

    }

    // クリック時の処理
    private void OnClick()
    {
        // 待ち時間
        if (this.TimerController.IsStarted)
        {
            // 出題番号とボタン番号を比較
            if (this.GameDirector.CheckNumber(this.Number))
            {
                // 正解音を鳴らす
                SoundManager.Instance.PlaySeByName("Buzzer0");

                // 正解エフェクト表示
                StartCoroutine("DrawJudgeCorrect");

                // スコア加算
                Data.Instance.P1Score += 1;


            }
            else
            {
                // 不正解音を鳴らす
                SoundManager.Instance.PlaySeByName("Buzzer1");

                // 不正解エフェクト表示
                StartCoroutine("DrawJudgeIncorrec");
            }
        }


    }

    // 正解表示
    IEnumerator DrawJudgeCorrect()
    {
        // 不正解の子オブジェクト検索
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