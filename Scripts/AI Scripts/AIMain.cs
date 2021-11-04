using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMain : MonoBehaviour
{
    GameManager gm;
    Animator anim;
    RobotAI robot;

    public string States;
    /// <summary>
    /// Run
    /// Turn Right,Left
    /// Die
    /// Stagger Small
    /// Stagger Big
    /// Escape
    /// Stop
    /// </summary>

    public int AI_Index;

    [Header("Verables")]
    public float ForwardSpeed;
    public float TurnSpeed;
    public float TurnAngel;
    public float TurnTimer;
    public float GetTurn;

    [Header("Booliens")]
    public bool CanCloseAnimator;
    public bool Lose;
    public bool Win;

    [Header("Game Objects")]
    public GameObject ProjectTile;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        robot = GameObject.Find("MainGirl").GetComponent<RobotAI>();
        States = "Run";
    }


    void Update()
    {
        StateMachine();


    }

    public void StateMachine()
    {
        if(gm.isStart)
        {
            if(!Win)
            {
                transform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, TurnAngel, 0), TurnSpeed * Time.deltaTime);
                KillAI();
                if (States == "Run")
                {
                    ForwardSpeed = 5;
                    anim.SetBool("Run", true);
                    TurnAI();
                }
                if (robot.isGreenLight == false)
                {
                    States = "Stop";
                    if (States == "Stop")
                    {
                        if (CanCloseAnimator || !Lose)
                        {
                            //anim.enabled = false;
                            if (anim.speed >= 0)
                            {
                                anim.speed -= Time.deltaTime;
                                ForwardSpeed = 0;
                            }

                        }
                    }

                }
                else
                {
                    //anim.enabled = true;
                    if (anim.speed <= 1)
                    {
                        anim.speed += Time.deltaTime;
                        ForwardSpeed = 6.5f;
                    }

                    States = "Run";
                }
            }
            
         
        }
       
    }

    public void TurnAI()
    {
        TurnTimer += Time.deltaTime;
        if(TurnTimer >= GetTurn)
        {
            TurnAngel = Random.Range(-60, 60);
            TurnTimer = 0;
        }
    }
    public void KillAI()
    {
        if(AI_Index == robot.TourIndex)
        {
            Lose = true;
            if(ProjectTile != null)
            {
                ProjectTile.SetActive(true);
                robot.Detected = true;
                CanCloseAnimator = false;
                ForwardSpeed = 2;
            }
           
        }
        else
        {
            Lose = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SmallObstcales")
        {
            anim.SetTrigger("Stagger");
        }
        if(other.gameObject.tag == "BigObstcales")
        {
            anim.SetTrigger("Fall");
            CanCloseAnimator = false;
            Invoke("ClosableAnimator", 2f);
            Lose = true;
        }
        if(other.gameObject.name == "LeftBounder")
        {
            TurnAngel = 50;
        }
        if(other.gameObject.name == "RightBounder")
        {
            TurnAngel = -50;
        }
        if (other.gameObject.name == "Projectile")
        {
            anim.enabled = false;
            GetComponent<Collider>().enabled = false;
            ForwardSpeed = 0;
            Destroy(this.gameObject, 2.3f);
            robot.Detected = false;
            gm.AIs -= 1;
        }
        if(other.gameObject.tag == "Finish")
        {
            anim.SetTrigger("WinJump");
            transform.rotation = Quaternion.identity;
            Win = true;
            anim.applyRootMotion = true;
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Projectile")
    //    {
    //        anim.enabled = false;
    //        GetComponent<Collider>().enabled = false;
    //        ForwardSpeed = 0;
    //        Destroy(this.gameObject, 2f);
    //    }
    //}
    public void ClosableAnimator()
    {
        CanCloseAnimator = true;
    }
}
