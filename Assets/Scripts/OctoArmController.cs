using UnityEngine;

public class ArmTargetMouseController : MonoBehaviour
{
    public Transform octopusBody;
    public float distance = 3f;
    public float sensitivity = 0.01f;
    public float scrollSpeed = 1f;
    public float minDistance = 1f;
    public float maxDistance = 5f;

    private Vector2 screenCenter;
    private Vector3 velocity; // for SmoothDamp
    public float smoothTime = 0.2f;

    void Start()
    {
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        Vector2 mouseOffset = (Vector2)Input.mousePosition - screenCenter;
        Vector3 offsetDir = new Vector3(mouseOffset.x * sensitivity, mouseOffset.y * sensitivity, 1).normalized;

        Vector3 desiredPosition = octopusBody.position + octopusBody.TransformDirection(offsetDir * distance);

        // Smooth movement (springy effect)
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Distance control
        if (Input.GetKey(KeyCode.Q)) distance -= scrollSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) distance += scrollSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }
}