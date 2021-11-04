using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bullet;
    public GameObject Impact;
    public GameObject MuzullFlash;
    public float Speed;
    public float RotSpeed;

    public Animator SoldierAnimator;
    void Start()
    {
        SoldierAnimator.SetBool("Shoot", true);
        Invoke("Go", 0.5f);
    }


    void FixedUpdate()
    {
        transform.LookAt(Player.transform.position + new Vector3(0,1.4f,0));
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        Bullet.transform.Rotate(0,0,RotSpeed * Time.deltaTime);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(Impact, transform.position, transform.rotation);
            Speed = 0;
            //Destroy(Bullet);
            Bullet.SetActive(false);
        }
    }
    public void Go()
    {
        Speed = 30;
        Bullet.SetActive(true);
        MuzullFlash.SetActive(true);
        
    }
}
