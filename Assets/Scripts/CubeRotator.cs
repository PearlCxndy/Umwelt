using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [Header("Rotation Modes")]
    public bool rotateAutomatically = true;
    public float rotationSpeed = 30f;

    [Header("External Rotation Input")]
    public bool useExternalRotation = false;
    public float externalYAngle = 0f;
    public CubeEncoder encoderReader;

    [Header("Manual Key Control")]
    public bool allowKeyControl = true;
    public float keyRotationSpeed = 90f; // degrees per second

    private float currentYRotation;
    private Vector3 centerOffset;

    void Start()
    {
        currentYRotation = transform.eulerAngles.y;
        CalculateCenterOffset();
    }

    void Update()
    {
        bool isManuallyControlling = false;

        if (allowKeyControl)
        {
            float input = Input.GetAxis("Horizontal"); // A/D or Left/Right
            if (Mathf.Abs(input) > 0.01f)
            {
                currentYRotation += input * keyRotationSpeed * Time.deltaTime;
                isManuallyControlling = true;
            }
        }

        if (!isManuallyControlling)
        {
            if (useExternalRotation && encoderReader != null)
            {
                externalYAngle = encoderReader.encoderAngle;
                currentYRotation = externalYAngle;
            }
            else if (rotateAutomatically)
            {
                currentYRotation += rotationSpeed * Time.deltaTime;
            }
        }

        ApplyRotationAroundCenter();
    }

    void ApplyRotationAroundCenter()
    {
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
