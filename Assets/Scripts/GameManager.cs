﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    public GameObject endUI;
    public Text endMessage;

    public static GameManager Instance;
    private EnemySpawner enemySpawner;
    bool isPaused = false;
    bool isEnd = false;


    void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void Win()
    {
        GameObject.Find("Canvas/Wave").GetComponent<Text>().text = "";
        endUI.SetActive(true);
        endMessage.text = "胜 利";
        isEnd = true;
    }
    public void Failed()
    {
        GameObject.Find("Canvas/Wave").GetComponent<Text>().text = "";
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMessage.text = "失 败";
        isEnd = true;
    }

    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }


    //---------------------------------------------------------曾颉 add   PauseGame-----------------------------------------------------------------

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isEnd==false)
        {
            isPaused = !isPaused;
           
            //打印isPaused
            Debug.Log(isPaused);
            
            if (isPaused)
            {
                // Pause the game
                endUI.SetActive(true);
                endMessage.text = "暂 停";
                Time.timeScale = 0;
                endUI.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
            }
            else
            {
                // Unpause the game
                Time.timeScale = 1;
                endUI.SetActive(false);
                endUI.GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;
            }
        }
    }


    //---------------------------------------------------------曾颉 add   PauseGame-----------------------------------------------------------------

    private bool canUseProp = true;
    IEnumerator CannotUseProp(float sec)
    {
        yield return new WaitForSeconds(sec);
        canUseProp = true;
    }
    IEnumerator ReturnNormal(float sec)
    {
        yield return new WaitForSeconds(sec);
        GlobalRate.speedRate = 1.0f;
        GlobalRate.turrentRate = 1.0f;
    }
    IEnumerator OnlySlowSpeed(float sec)
    {
        yield return new WaitForSeconds(sec);
        GlobalRate.speedRate = 0.5f;
    }
    IEnumerator WaitandGetMoney(float sec)
    {
        yield return new WaitForSeconds(sec);
        GlobalRate.gainmoney = -200;
        Debug.Log("gainmoney");
    }
    public void ButtonSlowDown()
    {
        if (canUseProp)
        {
            canUseProp = false;
            GlobalRate.speedRate = 0f;
            StartCoroutine(OnlySlowSpeed(3.0f));
            StartCoroutine(ReturnNormal(6.0f));
            StartCoroutine(CannotUseProp(20.0f));
        }

    }
    public void ButtonFrenzy()
    {
        if (canUseProp)
        {
            canUseProp = false;
            GlobalRate.turrentRate = 1.5f;
            StartCoroutine(ReturnNormal(8.0f));
            StartCoroutine(CannotUseProp(20.0f));
        }
        
    }
    public void GetMoney()
    {
        if (canUseProp)
        {
            canUseProp = false;
            StartCoroutine(WaitandGetMoney(10.0f));
            StartCoroutine(CannotUseProp(20.0f));
        }
    }


}
