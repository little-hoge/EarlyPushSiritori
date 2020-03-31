using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    // 計測時間
    public TimeDate time;

    public struct TimeDate
    {
        public int min, sec;  //  分, 秒, 
        public float msec;    // ミリ秒
    };

    // 開始されたかどうかのフラグ
    public bool IsStarted { get; set; } = false;


    // 経過時間を表示するテキストオブジェクト
    private GameObject TimerText { get; set; }

    void Start()
    {
        // テキストオブジェクトを取得しておく
        this.TimerText = GameObject.Find("Timer");

        // 計測開始
        this.IsStarted = false;

    }

    void Update()
    {

        // 準備時間、勝負が決まっていない時
        if (!this.IsStarted && !Data.Instance.GameJudge)
        {
            time.msec += Time.deltaTime;
            this.TimerText.GetComponent<Text>().text = "よーい";

            if (time.msec >= Define.GAMESTART_DELAYTIME - 1)
            {
                this.TimerText.GetComponent<Text>().text = "すたーと";
                if (time.msec >= Define.GAMESTART_DELAYTIME)
                {
                    this.time.msec = 0;
                    this.IsStarted = true;
                }

            }
        }
        // タイマー計測開始中の場合
        if (this.IsStarted)
        {
            // タイマー更新
            CalcTime();

            // 経過時間を表示
            this.TimerText.GetComponent<Text>().text = string.Format("Time:{0:00}:{1:00}.{2:00}"+"\n", time.min, time.sec, (time.msec / 10));

        }
    }

    // タイマー更新処理
    void CalcTime()
    {
        // 最大値制御
        if (time.min < 59 || time.sec < 59  || time.msec < 900) {
            // ミリ秒換算
            time.msec += (int)(UnityEngine.Time.deltaTime * 1000.0f);
        }

        // 時間計算
        if (time.sec >= 60)
        {
            time.sec = 0;
            time.min++;
        }
        if (time.msec >= 1000)
        {
            time.msec -= 1000;
            time.sec++;
        }



    }

}