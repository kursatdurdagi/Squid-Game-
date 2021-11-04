using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickSc : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Transform player;
    Vector3 move;
    public float moveSpeed;
    public bool moving;
    public Animator anim;


    public RectTransform pad;

    public bool Lose;
    public RobotAI robot;
    public GameObject Bullet;

    public float DieTimer;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition =
            Vector2.ClampMagnitude(eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);

        move = new Vector3(transform.localPosition.x, 0, transform.localPosition.y).normalized;
        //if (robot.isGreenLight == false)
        //{
        //    DieTimer += Time.deltaTime;
        //    if (DieTimer >= 1f)
        //    {
        //        Lose = true;
        //        Bullet.SetActive(true);
        //    }

        //}
    }
 
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        move = Vector3.zero;
        moving = false;
        Lose = false;
        DieTimer = 0;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        moving = true;
        pad.transform.position = eventData.position;
        //if (robot.isGreenLight == false)
        //{
        //    DieTimer += Time.deltaTime;
        //    if(DieTimer >= 0.1f)
        //    {
        //        Lose = true;
        //        Bullet.SetActive(true);
        //    }
           
        //}
    }
  

    private void Update()
    {
        if(Lose)
        {
            anim.speed = 1;
        }
        if (moving)
        {
            if (robot.isGreenLight == false)
            {
                DieTimer += Time.deltaTime;
                if(player.GetComponent<PlayerTocuhMove>().CanCloseAnimator == false)
                {
                    Lose = true;
                    Bullet.SetActive(true);
                }
                if (DieTimer >= 0.85f)
                {
                    Lose = true;
                    Bullet.SetActive(true);
                }

            }
            player.Translate(move * moveSpeed * Time.deltaTime, Space.World);
            anim.SetBool("Run", true);
            player.gameObject.GetComponent<PlayerTocuhMove>().Stoped = false;
           
            //anim.enabled = true;
            anim.speed = 1;
            //if (anim.speed <= 1)
            //{
            //    anim.speed += Time.deltaTime;

            //}
            if (move != Vector3.zero)
            {
                player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(move), 5 * Time.deltaTime);


            }
        }
        else
        {
            //anim.SetBool("Run", false);
            if(player.gameObject.GetComponent<PlayerTocuhMove>().GoIdle)
            {
                anim.SetBool("Run", false);
                player.gameObject.GetComponent<PlayerTocuhMove>().GoIdle = false;
            }
            if (player.GetComponent<PlayerTocuhMove>().CanCloseAnimator)
            {
                //anim.enabled = false;

                if (Lose)
                {
                    anim.speed = 1;
                }
                else
                {
                    if (anim.speed >= 0)
                    {
                        anim.speed -= Time.deltaTime;
                    }
                }

               
                player.gameObject.GetComponent<PlayerTocuhMove>().Stoped = true;
            }
             
            }
        }

  
}

