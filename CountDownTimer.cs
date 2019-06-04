using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDownTimer : MonoBehaviour
{
    //　制限時間（分）
    [SerializeField]
    private int minute;

    //　制限時間（秒）
    [SerializeField]
    private float seconds;

    [SerializeField]
    private Text timerText;

    //　トータル制限時間
    [System.NonSerialized]
    public float totalTime;

    //　前回Update時の秒数
    private float oldSeconds;

    GameManager gameManager;


    void Start()
    {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        //　制限時間が0秒以下、countDownがfalseなら何もしない
        if (totalTime <= 0f || !gameManager.countDown)
        {
            return;
        }

        gameManager.time = totalTime;

        //　一旦トータルの制限時間を計測；
        totalTime = minute * 60 + seconds;
        totalTime -= Time.deltaTime;

        //　再設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        //　タイマー表示用UIテキストに時間を表示する
        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }

        oldSeconds = seconds;

        //　制限時間終了
        if (totalTime <= 0f)
        {
            gameManager.GameOver();
        }
    }
}