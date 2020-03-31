using System.Collections.Generic;
using UnityEngine;

public class NumberButtonGenerator : MonoBehaviour
{
    // ボタンのプレハブ
    public GameObject NumberButtonPrefab;
    public GamePlayer GamePlayerPrefab;
    private List<GameObject> ButtonList = new List<GameObject>();

    // 前回値
    private int Last_NowQuestionNumber;
    private int Last_P2Score;

    void Start()
    {
        Debug.Log("パネル生成");

        // ボタンを生成
        this.GenerateNumberButtons(Define.CREATE_X, Define.CREATE_Y);

        // 初期値設定
        Last_NowQuestionNumber = Data.Instance.NowQuestionNumber;
        Last_P2Score = 0;
    }

    private void Update()
    {
        NumberButtonsUpdate();
        NumberButtonsDelete();
    }


    public void GenerateNumberButtons(int rowCount, int colCount)
    {
        var canvas = GameObject.Find("Canvas");

        // 一度ボタン等のオブジェクト全削除
        var buttons = GameObject.FindGameObjectsWithTag("NumberButton");
        foreach (var item in buttons) Destroy(item);

        // ボタンの位置調整用
        float offsetCountX = (colCount - 1) / 2.0f;
        float offsetCountY = (rowCount - 1) / 2.0f;
        int index = 0;
        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < colCount; x++)
            {
                // ボタンに設定される番号
                int number = Data.Instance.ButtonNumber[index];

                // ボタンを生成
                ButtonList.Add(Instantiate(this.NumberButtonPrefab) as GameObject);

                // ボタンの情報を設定
                var controller = ButtonList[ButtonList.Count - 1].GetComponent<NumberButtonController>();
                controller.SetButtonInfos(number, number.ToString());

                // 画面中央に配置されるようにする
                ButtonList[ButtonList.Count - 1].transform.SetParent(canvas.GetComponent<RectTransform>());

                // 表示する行数列数から中央に来るように調整した位置とする
                ButtonList[ButtonList.Count - 1].transform.localPosition = new Vector3(
                    Define.BUTTONSIZE_X * x - Define.BUTTONSIZE_X * offsetCountX,
                    Define.BUTTONSIZE_Y * y - Define.BUTTONSIZE_Y * offsetCountY,
                    0);
            }
        }
    }

    public void NumberButtonsUpdate()
    {
        for (int index = 0; index < Function.GetMaxButton(); index++)
        {
            // ボタンに設定される番号
            int number = Data.Instance.ButtonNumber[index];

            // ボタンの情報を更新
            ButtonList[index].GetComponent<NumberButtonController>()
                .SetButtonInfos( number, number.ToString());

        }

    }

    public void NumberButtonsDelete()
    {
        
        // 相手スコアに変動があった時
        if (Last_P2Score != Data.Instance.P2Score)
        {
            for (int index = 0; index < Function.GetMaxButton(); index++)
            {
                // 出題されたボタンを非アクティブにする
                if (Last_NowQuestionNumber == ButtonList[index].GetComponent<NumberButtonController>().Number)
                {

                    ButtonList[index].SetActive(false);
                }
            }
        }
        // 前回値更新
        Last_P2Score = Data.Instance.P2Score;
        Last_NowQuestionNumber = Data.Instance.NowQuestionNumber;
    }
}