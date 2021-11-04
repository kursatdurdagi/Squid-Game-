using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSc : MonoBehaviour
{

    [SerializeField] GameObject Saw;


    [SerializeField] float RotSpeed;



    [SerializeField] float SpeedX;
    [SerializeField] float SpeedY;
    [SerializeField] float SpeedZ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Saw.transform.Rotate(0, 0, RotSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * SpeedX * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Left")
        {
            SpeedX = 5;
        }
        if (other.gameObject.name == "Right")
        {
            SpeedX = -5;
        }
    }
}
