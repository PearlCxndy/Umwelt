using UnityEngine;

public class ArmTargetMouseController : MonoBehaviour
{
    public float distance = 3f;
    public float sensitivity = 0.01f;
    public float scrollSpeed = 1f;
    public float minDistance = 1f;
    public float maxDistance = 5f;
    public float surfaceOffset = 0.05f; // how far to keep above surface

    private Vector2 screenCenter;
    public float smoothTime = 0.2f;


    public Transform octopusBody;
    public float maxRaycastDistance = 5f;
    public float moveSpeed = 10f;
    public LayerMask surfaceMask; // Assign to the sphere layer

    private Vector3 velocity;

    void Start()
    {
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }


    void Update()
    {
        // 1. Ray direction from octopus forward (or mouse direction)
        Vector3 rayDir = octopusBody.forward;
        Ray ray = new Ray(octopusBody.position, rayDir);

        if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance, surfaceMask))
        {
            // 2. Position slightly above the surface
            Vector3 targetPos = hit.point + hit.normal * 0.05f;

            // 3. Smooth follow
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.05f);
        }
        else
        {
            // 4. Fallback position if nothing hit
            transform.position = octopusBody.position + rayDir * 2f;
            // Scroll to control distance
            if (Input.GetKey(KeyCode.Q)) distance -= scrollSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.E)) distance += scrollSpeed * Time.deltaTime;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }
    }
}