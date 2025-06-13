using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The octopus's camera follow target

    public float smoothSpeed = 5f;
    public Vector3 offset;

    public float moveUp = 1.5f;

    void LateUpdate()
    {   
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;
        transform.LookAt(target.position + new Vector3(0, moveUp, 0));
    }
}