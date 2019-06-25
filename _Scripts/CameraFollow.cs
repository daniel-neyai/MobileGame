using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public bool IsMoving { set; get; }

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (!IsMoving)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
       
        transform.LookAt(target);
    }

}
