using UnityEngine;

[ExecuteAlways]
public class FitToCameraView : MonoBehaviour
{
    public Camera targetCamera;
    public float distanceFromCamera = 5f;
    public float oversize = 0f; // Add margin if needed

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        AdjustToCamera();
    }

    void Update()
    {
#if UNITY_EDITOR
        AdjustToCamera(); // Keep updating in editor view
#endif
    }

    void AdjustToCamera()
    {
        if (targetCamera == null) return;

        float height = 2f * distanceFromCamera * Mathf.Tan(targetCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * targetCamera.aspect;

        transform.localScale = new Vector3(width + oversize, height + oversize, 1f);
        transform.position = targetCamera.transform.position + targetCamera.transform.forward * distanceFromCamera;
        transform.rotation = targetCamera.transform.rotation;
    }
}
