using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_FollowPlayer : MonoBehaviour {

    private Transform playerTransform;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        follow();
	}

    void follow()
    {
        transform.position = Vector3.forward * playerTransform.position.z;
    }
}
