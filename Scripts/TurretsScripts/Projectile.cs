using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;

    public GameObject Target;

    public GameObject Impact;

    public int ProjectileIndex;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        transform.LookAt(Target.transform.position + new Vector3(0,1,0));
        ProjectileIndex = transform.parent.GetComponent<AIMain>().AI_Index;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.GetComponent<AIMain>().AI_Index == ProjectileIndex)
            {
                Instantiate(Impact, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            
        }
    }
    
}
