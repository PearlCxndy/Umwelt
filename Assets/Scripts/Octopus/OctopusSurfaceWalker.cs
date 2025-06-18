using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OctopusController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 50f;
    public float jumpForce = 5f;
    public LayerMask groundMask;
    public float groundCheckDistance = 0.6f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleRotation();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
        CheckGrounded();
    }

    void HandleRotation()
    {
        float rotateInput = Input.GetAxis("Horizontal"); // A/D
        if (Mathf.Abs(rotateInput) > 0.01f)
        {
            Quaternion rotation = Quaternion.AngleAxis(rotateInput * rotationSpeed * Time.deltaTime, transform.up);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }

void HandleMovement()
{
    float moveInput = Input.GetAxis("Vertical");

    if (Mathf.Abs(moveInput) > 0.01f)
    {
        // Cast a ray to find the surface normal below the octopus
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.2f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance + 0.3f, groundMask))
        {
            Vector3 surfaceNormal = hit.normal;

            // Calculate slope-aware forward direction
            Vector3 slopeForward = Vector3.ProjectOnPlane(transform.forward, surfaceNormal).normalized;

            // Move along slope
            Vector3 move = slopeForward * moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            // Optional: push down slightly to prevent floating
            rb.AddForce(-surfaceNormal * 5f, ForceMode.Acceleration);
        }
        else
        {
            // Fallback move (air)
            Vector3 move = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
    }
}

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void CheckGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        isGrounded = Physics.Raycast(ray, groundCheckDistance, groundMask);
    }
}