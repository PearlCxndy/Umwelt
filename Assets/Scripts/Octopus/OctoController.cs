using UnityEngine;

public class SquidMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // Left/Right
        float v = Input.GetAxis("Vertical");   // Up/Down

        // Rotate around Y (left/right)
        transform.Rotate(Vector3.up, h * rotateSpeed * Time.deltaTime);

        // Move forward in local Z direction
        transform.position += transform.forward * v * moveSpeed * Time.deltaTime;
    }
}