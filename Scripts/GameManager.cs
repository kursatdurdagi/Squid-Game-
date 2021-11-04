using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject inGameMenu;
    public GameObject RestartMenu;
    public GameObject WinMenu;

    public bool isStart;
    public bool isLose;
    public bool isWin;


    public Animator CamAnimator;

    public TMP_Text CounterText;

    public int AIs;
    public int TotalAIs;
    void Start()
    {
        TotalAIs = AIs;
    }


    void Update()
    {
        UIManagment();
        PlayerCounter();
    }
    public void UIManagment()
    {
        if(isWin)
        {
            inGameMenu.SetActive(false);
            WinMenu.SetActive(true);
        }
        if(isLose)
        {
            inGameMenu.SetActive(false);
            RestartMenu.SetActive(true);
        }
    }
    public void PlayerCounter()
    {
        CounterText.text = AIs.ToString() + "/" + TotalAIs.ToString();
    }

    public void StartGame()
    {
        isStart = true;
        CamAnimator.SetTrigger("Start");
        //CamAnimator.SetBool("StartGame",true);
        MainMenu.SetActive(false);
        inGameMenu.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
