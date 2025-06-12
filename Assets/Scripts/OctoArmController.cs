using UnityEngine;

public class ArmTargetMouseController : MonoBehaviour
{
    public Transform octopusBody; // Reference to the octopus
    public float distance = 3f;
    public float sensitivity = 0.01f;
    public float scrollSpeed = 1f;
    public float minDistance = 1f;
    public float maxDistance = 5f;

    private Vector2 screenCenter;

    void Start()
    {
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        // Read mouse offset from screen center
        Vector2 mouseOffset = (Vector2)Input.mousePosition - screenCenter;
        Vector3 offsetDir = new Vector3(mouseOffset.x * sensitivity, mouseOffset.y * sensitivity, 1).normalized;

        // Calculate the new position in front of the octopus
        Vector3 targetPos = octopusBody.position + octopusBody.TransformDirection(offsetDir * distance);
        transform.position = targetPos;

        // Distance control with Q/E
        if (Input.GetKey(KeyCode.Q)) distance -= scrollSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) distance += scrollSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }
}