using UnityEngine;

public class SurfaceWalker : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float raycastDistance = 2f;

    public Transform orientationReference; // Optional: camera or head
    public LayerMask groundMask;

    private Vector3 currentSurfaceNormal = Vector3.up;

    void Update()
    {
        HandleSurfaceAlignment();
        HandleMovement();
    }

void HandleSurfaceAlignment()
{
    // Step 1: Cast a sphere downward from above
    Vector3 rayOrigin = transform.position + currentSurfaceNormal * 0.3f;
    Vector3 rayDirection = -currentSurfaceNormal;

    RaycastHit hit;
    if (Physics.SphereCast(rayOrigin, 0.4f, rayDirection, out hit, raycastDistance, groundMask))
    {
        Vector3 hitNormal = hit.normal;

        // Step 2: Smoothly blend the surface normal
        currentSurfaceNormal = Vector3.Lerp(currentSurfaceNormal, hitNormal, Time.deltaTime * rotationSpeed);

        // Step 3: Compute target rotation with up aligned to surface normal
        Quaternion surfaceAlignRotation = Quaternion.FromToRotation(transform.up, currentSurfaceNormal) * transform.rotation;

        // Step 4: Apply the new rotation smoothly
        transform.rotation = Quaternion.Slerp(transform.rotation, surfaceAlignRotation, Time.deltaTime * rotationSpeed);

        // Step 5: Stick to the surface with small offset
        transform.position = hit.point + currentSurfaceNormal * 0.05f;
    }
    else
    {
        Debug.DrawRay(rayOrigin, rayDirection * raycastDistance, Color.red);
    }
}

    void HandleMovement()
{
    // Get input
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    // Find tangent direction relative to current surface normal
    Vector3 forward = Vector3.Cross(transform.right, currentSurfaceNormal);
    Vector3 right = Vector3.Cross(currentSurfaceNormal, forward);

    Vector3 moveDir = (right * h + forward * v).normalized;

    // Move in a way that follows the surface curve
    Vector3 newPosition = transform.position + moveDir * moveSpeed * Time.deltaTime;

    // Project new position onto surface
    RaycastHit hit;
    if (Physics.Raycast(newPosition + currentSurfaceNormal * 2f, -currentSurfaceNormal, out hit, 5f, groundMask))
    {
        newPosition = hit.point;
        transform.position = newPosition;
    }
    else
    {
        // fallback movement
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
}