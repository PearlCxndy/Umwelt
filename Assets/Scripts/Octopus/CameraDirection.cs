using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // Usually the OctopusRoot
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 5f;
    public float moveUp = 1.5f;

    [Tooltip("Whether to align camera tilt to surface climb rotation")]
    public bool matchTargetUp = true;

    void LateUpdate()
    {
        if (!target) return;

        // Determine the rotation-aware offset (important for climbing)
        Vector3 rotatedOffset = matchTargetUp 
            ? target.rotation * offset        // Rotate offset with octopus orientation
            : offset;                         // Just keep fixed world offset

        Vector3 desiredPosition = target.position + rotatedOffset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;

        // Look at the octopus's head area (with upward tweak)
        Vector3 lookTarget = target.position + (matchTargetUp ? target.up : Vector3.up) * moveUp;
        transform.LookAt(lookTarget);
    }
}