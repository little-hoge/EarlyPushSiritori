using NCMB;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumberButtonGenerator : MonoBehaviour
{
    // ボタンのプレハブ
    public GameObject NumberButtonPrefab;
    private List<GameObject> ButtonList = new List<GameObject>();

    // 前回値
    private int Last_P2Score;

    void Start()
    {
        Debug.Log("釦生成");

        // ボタンを生成
        this.GenerateNumberButtons(Define.CREATE_Y, Define.CREATE_X);

    }

    private void Update()
    {
        // 答えられる釦がない時
        if (CheckAnser() == ENUM.eWordType.None)
        {

            // 一定時間後、釦再取得
            StartCoroutine(ButtonsReload());


        }



        // 釦情報更新
        NumberButtonsUpdate();

        // 釦削除(非アクティブ化)
        // 通信時、相手情報同期
        NumberButtonsInActive();
    }

    // 釦生成
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
                var number = Data.Instance.ButtonNumber[index];

                // ボタンを生成
                ButtonList.Add(Instantiate(this.NumberButtonPrefab) as GameObject);

                // ボタンの情報を設定
                var controller = ButtonList[ButtonList.Count - 1].GetComponent<NumberButtonController>();
                controller.SetButtonInfos(number);

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

    // 釦更新
    public void NumberButtonsUpdate()
    {
        for (int index = 0; index < Function.GetMaxButton(); index++)
        {
            // ボタンに設定される番号
            var number = Data.Instance.ButtonNumber[index];

            // ボタンの情報を更新
            ButtonList[index].GetComponent<NumberButtonController>()
                .SetButtonInfos(number);

        }

    }

    // 釦削除(非アクティブ化)
    // 通信時、相手情報同期
    public void NumberButtonsInActive()
    {
        // 相手スコアに変動があった時
        if (Last_P2Score != Data.Instance.P2Score)
        {
            for (int index = 0; index < Function.GetMaxButton(); index++)
            {
                // 押された釦を非アクティブにする
                if (Data.Instance.AnserWordNumber == ButtonList[index].GetComponent<NumberButtonController>().Number)
                {
                    ButtonList[index].SetActive(false);
                }
            }
            // 前回値更新
            Last_P2Score = Data.Instance.P2Score;
        }
    }



    // 釦アクティブ番号取得
    public List<int> GetButtonsActiveNumber()
    {
        List<int> ActiveButtonList = new List<int>();

        for (int index = 0; index < Function.GetMaxButton(); index++)
        {
            // アクティブ番号の時
            if (ButtonList[index].activeSelf == true)
            {
                ActiveButtonList.Add(ButtonList[index].GetComponent<NumberButtonController>().Number);
            }
        }

        return ActiveButtonList;
    }


    // 答えるブロックがない場合
    public ENUM.eWordType CheckAnser()
    {
        ENUM.eWordType result = ENUM.eWordType.None;

        // 釦アクティブ番号取得
        var ActiveButtonList = GetButtonsActiveNumber();

        Debug.Log("アクティブ釦" + string.Join(", ", ActiveButtonList));

        // 存在する釦
        foreach (var val in ActiveButtonList)
        {
            // 答えられる釦か判定
            result = Function.CheckWordNumber(val);

            // 答えられる
            if (ENUM.eWordType.None != result)
            {
                return result;
            }
        }

        // 答えられない
        return result;
    }


    // 全釦表示(アクティブ化)
    public void NumberButtonsActive()
    {
        for (int index = 0; index < Function.GetMaxButton(); index++)
        {
            ButtonList[index].SetActive(true);
        }
    }

    // 釦単語再設定
    public void NumberButtonsWordReset()
    {
        // ランダムにお題取得
        var Random = new System.Random();
        Data.Instance.AnserWordNumber = (short)Random.Next(1, Define.EWORD_NUM - 1);
        Data.Instance.AnserWordType = ENUM.eWordType.Normal;

        // 全単語取得
        List<short> eWordNum = new List<short>(
       Enumerable.Range(1, Define.EWORD_NUM-1)
       .Select(num => (short)num).ToArray());
        
        // シャッフル
        Function.Shuffle(eWordNum);

        // 登録単語が釦数以下の時
        for (; eWordNum.Count < Function.GetMaxButton(); )
        {
            // 重複単語をランダムに割り当てる
            eWordNum.Add((short)Random.Next(1, Define.EWORD_NUM - 1));

        }
        
        // 釦数分単語取得
        Data.Instance.ButtonNumber.Clear();
        Data.Instance.ButtonNumber.AddRange(eWordNum.GetRange(0, Function.GetMaxButton()));
    }


    // 不正解表示
    IEnumerator ButtonsReload()
    {
        
        // 一定秒待ち
        yield return new WaitForSeconds(0.5f);

        // アクティブ化
        NumberButtonsActive();

        // 釦単語再設定
        NumberButtonsWordReset();

    }
}