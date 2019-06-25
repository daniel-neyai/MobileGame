using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotating : MonoBehaviour {

    public Transform m_Object = null;
    public Transform m_target = null;

    public Vector3 m_Speed = new Vector3(4.0f, 2.0f, 1.0f);

    public Vector3 nextPosition = Vector3.zero;

    void LateUpdate()
    {
        nextPosition.x = Mathf.Lerp(this.transform.position.x, m_target.position.x, m_Speed.x * Time.deltaTime);
        nextPosition.y = Mathf.Lerp(this.transform.position.y, m_target.position.y, m_Speed.y * Time.deltaTime);
        nextPosition.z = Mathf.Lerp(this.transform.position.z, m_target.position.z, m_Speed.z * Time.deltaTime);

        this.transform.position = nextPosition;

        this.transform.LookAt(m_Object.position);  
    }

}
