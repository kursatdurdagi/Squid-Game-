using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTocuhMove : MonoBehaviour
{
    Animator anim;
    public Animator CamAnimator;

    public RobotAI robot;

    Touch touch;
    public float speedModifier;
    public float Speed;


    public float MinX;
    public float MaxX;
    public float MinZ;
    public float MaxZ;

    public bool CanCloseAnimator;
    public bool GoIdle;
    public bool Stoped;
    public bool Lose;

    public GameObject Impact;
    public GameObject Bullet;

    public Transform Pointer;
    public ParticleSystem WinEffect;
    GameManager Gm;

    void Start()
    {
        Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        
    }


    void FixedUpdate()
    {
       
        if (Gm.isStart)
        {
            Pointer.gameObject.SetActive(true);
            Pointer.Rotate(0, 100 * Time.deltaTime, 0);
            //Movement();
            Bounders();
           
            
        }
        if(Gm.isLose)
        {
            Pointer.gameObject.SetActive(false);
        }
    }
    
    //public void Movement()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    //        anim.SetBool("Run", true);

    //        anim.enabled = true;

    //        touch = Input.GetTouch(0);
    //        if (touch.phase == TouchPhase.Moved)
    //        {
    //            transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier, transform.position.y, transform.position.z);
                
    //        }
    //    }
    //    else
    //    {
    //        anim.SetBool("Run", false);
    //        if(CanCloseAnimator)
    //        {
    //            anim.enabled = false;
    //        }
    //    }
    //}
    public void Bounders()
    {
        Vector3 boundry = transform.position;
        boundry.x = Mathf.Clamp(boundry.x, MinX, MaxX);
        boundry.z = Mathf.Clamp(boundry.z, MinZ, MaxZ);
        transform.position = boundry;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SmallObstcales")
        {
            anim.SetTrigger("Stagger");
            GoIdle = true;
            Instantiate(Impact, transform.position, transform.rotation);
        }
        if(other.gameObject.tag == "BigObstcales")
        {
            anim.SetTrigger("Fall");
            GoIdle = true;
            CanCloseAnimator = false;
            Invoke("ClosableAgain", 2.5f);
            Instantiate(Impact, transform.position, transform.rotation);
        }
        if(other.gameObject.tag == "Finish")
        {
            anim.SetTrigger("WinJump");
            CamAnimator.SetBool("Finish", true);
            Gm.isStart = false;
            anim.applyRootMotion = true;
            Gm.isWin = true;
            transform.rotation = Quaternion.identity;
            Pointer.gameObject.SetActive(false);
            WinEffect.Play();

        }
        if(other.gameObject.tag == "Bullet")
        {
            anim.SetBool("Die", true);
            Gm.isWin = true;
            anim.speed = 1;
            Gm.isLose = true;
            Pointer.gameObject.SetActive(false);
        }
    }

    public void ClosableAgain()
    {
        CanCloseAnimator = true;
    }
}
