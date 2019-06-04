using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(CountDownTimer))]

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    float waitTime = 2.0f;

    [SerializeField]
    Text readyGoText;

    CountDownTimer timer;
    ThirdPersonCharacter character;
    Rigidbody playerRigidbody;
    GameManager gameManager;

    void Start()
    {
        timer = GetComponent<CountDownTimer>();
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>();
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(ReadyGo());
    }

    IEnumerator ReadyGo()
    {
        yield return new WaitForEndOfFrame();
        //プレイヤーを停止させる
        character.enabled = false;
        playerRigidbody.isKinematic = true;
        yield return new WaitForSeconds(waitTime);
        readyGoText.text = "Ready?";
        yield return new WaitForSeconds(waitTime);
        readyGoText.text = "GO!!";

        //プレイヤーを移動可能にさせる
        character.enabled = true;
        playerRigidbody.isKinematic = false;

        //カウントダウン開始
        gameManager.countDown = true;
        yield return new WaitForSeconds(waitTime);
        readyGoText.enabled = false;
    }
}