using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [Header("Manual or Auto Rotation")]
    public bool rotateAutomatically = true;
    public float rotationSpeed = 30f; // degrees/second

    [Header("Use External Rotation Input (e.g. from Arduino)")]
    public bool useExternalRotation = false;
    public float externalYAngle = 0f;

    void Update()
    {
        if (useExternalRotation)
        {
            // Rotate around the Y axis using external value
            transform.rotation = Quaternion.Euler(0f, externalYAngle, 0f);
        }
        else if (rotateAutomatically)
        {
            // Rotate around Y axis continuously
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    // Call this from another script to update Y angle
    public void SetYRotation(float angleY)
    {
        externalYAngle = angleY;
    }
}
