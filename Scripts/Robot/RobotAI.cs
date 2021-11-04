using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    GameManager gm;
    public Animator CamAnimator;
    public PlayerTocuhMove Player;

    [Header ("Verables")]
    public float Timer;
    public float GetTurn;
    public int TourIndex;

    [Header("Head")]
    public Transform Head;
    public float HeadTurnAngel;

    public bool isGreenLight;
    public bool PlayFOV;
    public bool Detected;

    public ParticleSystem StopEffect;
    public ParticleSystem RunEffect;

    void Start()
    {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        isGreenLight = true;

    }


    void Update()
    {
        if(gm.isStart)
        {
            StartTimer();
            CamFOV();
            Head.rotation = Quaternion.Lerp(Head.rotation, Quaternion.Euler(0, HeadTurnAngel, 0), 10 * Time.deltaTime);
            EffectController();
        }
        else
        {
            CamAnimator.SetBool("GoUp", false);
        }
    }
    public void CamFOV()
    {
        if(PlayFOV)
        {
            CamAnimator.SetBool("GoUp", true);
        }
        else
        {

            CamAnimator.SetBool("GoUp", false);

        }
    }

    public void EffectController()
    {
        if(Timer >= 4.5f && isGreenLight)
        {
            StopEffect.Play();
        }
        if (Timer >= 2.5f && !isGreenLight)
        {
            RunEffect.Play();
        }
    }
    public void StartTimer()
    {
        if(Detected == false)
        {
            Timer += Time.deltaTime;
        }
        
        if(Timer >= GetTurn)
        {
            isGreenLight = !isGreenLight;
            PlayFOV = !PlayFOV;
            Timer = 0;
            
            if(!isGreenLight)
            {
                TourIndex += 1;
                HeadTurnAngel = 180;
                GetTurn = 3;
            }
            else
            {
                
                HeadTurnAngel = 0;
                GetTurn = 5;
            }
         
        }
    }
}
