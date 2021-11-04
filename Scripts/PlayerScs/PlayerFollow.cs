using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {

    public GameObject PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

	// Use this for initialization
	void Start () {
        _cameraOffset = transform.position - PlayerTransform.transform.position;
        //PlayerTransform = GameObject.FindGameObjectWithTag("Player");
    }
	
	// LateUpdate is called after Update
	void Update () {
      

        Vector3 newPos = PlayerTransform.transform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer)
            transform.LookAt(PlayerTransform.transform);

	}
}
