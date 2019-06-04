using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    //ステージの数
    int n = 2;

    //表示するランキングの数
    int m = 5;
 
    //スコアを表示するText
    public Text[] scoreText1;
    public Text[] scoreText2;

    //スコアを格納
    int[,] score = new int[2,6];
    string[,] scoreString = new string[2,6];
    string[] separateScore = new string[2];

    string[] sps;

    [System.NonSerialized]
    public int[] newScore = new int[3];

    //スコアをカンマ区切りで保存
    string allScore;

    //スコアの保存先キー
    private string key = "Score";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //スコア更新
    public void ScoreUpdate()
    {
        ScoreReroad();
        ScoreSort();
        ScorePrint();
        ScoreSave();
        //初期化
        newScore = new int[3];
    }

    //スコア読み取り
    void ScoreReroad()
    {
        //スコアを読み取る
        allScore = PlayerPrefs.GetString(key,"0,0,0,0,0:0,0,0,0,0");
        separateScore = allScore.Split(':');

        for (int i = 0; i < n; i++)
        {
            string str = separateScore[i];
            sps = str.Split(',');

            for (int j = 0; j < m; j++)
            {
                scoreString[i, j] = sps[j];
            }
        }

        //文字列を配列に
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                score[i,j] = int.Parse(scoreString[i,j]);
            }
        }

    }

    //スコアを並び替える
    void ScoreSort()
    {
        //それぞれの最後に新しいスコアを代入
        score[0,5] = newScore[0];
        score[1,5] = newScore[1];

        for (int i = 0; i < n; i++)
        {
            //ソート
            for (int j = m; j > 0; j--)
            {
                //一つ前と比較
                if (score[i, j] > score[i, j - 1])
                {
                    //スコア入れ替え
                    int tmp = score[i, j - 1];
                    score[i, j - 1] = score[i, j];
                    score[i, j] = tmp;
                }
            }
        }
    }

    //スコアを保存
    void ScoreSave()
    {
        //配列を文字列に
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                scoreString[i,j] = score[i,j].ToString();
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                sps[j] = scoreString[i, j];
            }

            separateScore[i] = string.Join(",", sps);
        }

        allScore = string.Join(":", separateScore);

        //スコア保存
        PlayerPrefs.SetString(key, allScore);
        PlayerPrefs.Save();
    }

    //スコアを表示
    void ScorePrint()
    {
        for (int j = 0; j < m; j++)
        {
            int second = (score[0, j] % 60);
            string sec;

            //一桁なら0を追加
            if ((second / 10) != 0) {
                sec = second.ToString();
            }
            else
            {
                sec = "0" + second.ToString();
            }

            scoreText1[j].text = "NO." + (j + 1) + " - " + (score[0, j] / 60).ToString() + " : " + sec;



            second = (score[1, j] % 60);

            //一桁なら0を追加
            if ((second / 10) != 0)
            {
                sec = second.ToString();
            }
            else
            {
                sec = "0" + second.ToString();
            }

            scoreText2[j].text = "NO." + (j + 1) + " - " + (score[1, j] / 60).ToString() + " : " + sec;
        }
    }

}
