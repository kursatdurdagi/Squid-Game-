using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{

    public void Cut(Transform cutter)
    {
        if (cutter.transform.position.x < 0)
        {
            // scale 2 x:1 cutter x -0.5      
            // left
            float y = transform.localScale.y;
            y -= transform.position.x;
            float dist = y + cutter.position.x;
            Debug.Log("dist : " + dist);
            if (dist / 2 > 0)
            {
                // 3 -0.5=2.5
                // 0 + 0.5 = 0.5

                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - dist / 2, transform.localScale.z);
                transform.position = new Vector3(transform.position.x + dist / 2, transform.position.y, transform.position.z);
                // gameObject.SetActive(false);
                GameObject yeni = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

                yeni.transform.localScale = new Vector3(transform.localScale.x, dist / 2, transform.localScale.z);
                yeni.transform.rotation = transform.rotation;
                yeni.transform.position = new Vector3(-(y - yeni.transform.localScale.y), transform.position.y, transform.position.z);

                yeni.AddComponent<Rigidbody>();

            }




            // cutter -1 yeni pos -2 scale 1
            // cutter -0.5 yeni pos -1.75 scale 1.25


        }
        else
        {
            // right
            // scale 3 cutter 1 
            float y = transform.localScale.y;
            y += transform.position.x;
            float dist = y - cutter.position.x;
            Debug.Log("dist : " + dist);

            if (dist / 2 > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - dist / 2, transform.localScale.z);
                transform.position = new Vector3(transform.position.x - dist / 2, transform.position.y, transform.position.z);

                GameObject yeni = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                yeni.transform.localScale = new Vector3(transform.localScale.x, dist / 2, transform.localScale.z);
                yeni.transform.rotation = transform.rotation;
                yeni.transform.position = new Vector3(y - yeni.transform.localScale.y, transform.position.y, transform.position.z);

                yeni.AddComponent<Rigidbody>();
            }





        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Saw")
        {
            Cut(other.gameObject.transform);
        }
    }


}
