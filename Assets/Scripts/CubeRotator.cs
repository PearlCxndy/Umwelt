using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [Header("Manual or Auto Rotation")]
    public bool rotateAutomatically = true;
    public float rotationSpeed = 30f;

    [Header("External Input")]
    public bool useExternalRotation = false;
    public float externalYAngle = 0f;

    [Header("Mouse Drag Control")]
    public bool allowMouseDrag = true;
    public float mouseSensitivity = 0.2f;

    private float currentYRotation;
    private bool isDragging = false;
    private Vector3 lastMousePosition;

    private Vector3 centerOffset;  // center offset

    void Start()
    {
        currentYRotation = transform.eulerAngles.y;
        CalculateCenterOffset();
    }

    void Update()
    {
        if (allowMouseDrag)
        {
            HandleMouseDrag();
        }

        if (useExternalRotation)
        {
            currentYRotation = externalYAngle;
        }
        else if (rotateAutomatically && !isDragging)
        {
            currentYRotation += rotationSpeed * Time.deltaTime;
        }

        ApplyRotationAroundCenter();
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float deltaY = (delta.x / Screen.width) * 360f;  // much smoother drag

            currentYRotation += deltaY;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void ApplyRotationAroundCenter()
    {
        // Move object to center, rotate, then move back
        transform.position -= centerOffset;
        transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
        transform.position += centerOffset;
    }

    void CalculateCenterOffset()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
        {
            centerOffset = Vector3.zero;
            return;
        }

        Bounds combinedBounds = renderers[0].bounds;

        foreach (Renderer rend in renderers)
        {
            combinedBounds.Encapsulate(rend.bounds);
        }

        Vector3 centerWorld = combinedBounds.center;
        centerOffset = centerWorld - transform.position;
    }

    public float GetCurrentYRotation()
    {
        return currentYRotation;
    }

    public void SetYRotation(float angleY)
    {
        externalYAngle = angleY;
    }
}
