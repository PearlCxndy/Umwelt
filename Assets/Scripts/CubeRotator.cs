using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [Header("Manual or Auto Rotation")]
    public bool rotateAutomatically = true;
    public float rotationSpeed = 30f;

    [Header("External Input")]
    public bool useExternalRotation = false;
    public float externalYAngle = 0f;

    private float currentYRotation;

    void Start()
    {
        currentYRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        if (useExternalRotation)
        {
            currentYRotation = externalYAngle;
        }
        else if (rotateAutomatically)
        {
            currentYRotation += rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
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
