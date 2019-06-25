using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_RoamingBoost : MonoBehaviour {

    public Transform targetTransform;
    public float distanceFromPlayer = 1.5f;
    public float speed = 7;

    void Update()
    {
        Vector3 displacementFromTarget = targetTransform.position - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;

        float distanceToTarget = displacementFromTarget.magnitude;

        if(distanceToTarget > distanceFromPlayer)
        {
            transform.Translate(velocity * Time.deltaTime);
        }

    }
}
