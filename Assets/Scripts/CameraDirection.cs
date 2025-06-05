using UnityEngine;

public class CameraLookAtTarget : MonoBehaviour
{
    public Transform target; // Drag your object here (e.g., squid)

    void Update()
    {
        transform.LookAt(target);
    }
}