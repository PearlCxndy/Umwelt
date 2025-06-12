using UnityEngine;

public class OctopusSurfaceWalker : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotateSpeed = 5f;
    public float raycastDistance = 2f;
    public LayerMask surfaceLayer;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Octopus walks on all surfaces, so no gravity
    }

    void FixedUpdate()
    {
        // Get surface normal
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance, surfaceLayer))
        {
            // Align "up" direction to surface normal
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // Move along local forward direction
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = (transform.forward * v + transform.right * h).normalized;
            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
        }
    }
}