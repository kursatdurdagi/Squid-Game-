using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject AI;
    public GameObject Player;

    public RobotAI robot;

    public GameObject PlayerProjectile;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        ShootStopped();
    }

    public void ShootStopped()
    {
        //if(robot.isGreenLight == false)
        //{
            transform.LookAt(Player.transform.position);
        //}

        //if(robot.isGreenLight == false)
        //{
        //    if(Player.GetComponent<PlayerTocuhMove>().Stoped == false)
        //    {
        //        PlayerProjectile.SetActive(true);
        //    }
        //}
    }
}
